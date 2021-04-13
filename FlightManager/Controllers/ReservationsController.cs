using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightsManager.Data.Models;
using FlightManager.Models;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Flight);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .ThenInclude(p => p.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "FlightNumber");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,IsConfirmed,FlightId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                var passengerCount = Int32.Parse(this.Request.Form.FirstOrDefault(x => x.Key == "PassengerCount").Value);
                return RedirectToAction(nameof(AddPassengers), new { count = passengerCount, reservation = reservation.Id });
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "FlightNumber", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/AddPassengers
        public async Task<IActionResult> AddPassengers()
        {
            int count = Int32.Parse(this.Request.Query["count"]);
            int reservationId = Int32.Parse(this.Request.Query["reservation"]);

            var reservation = await _context.Reservations.FindAsync(reservationId);
            var flight = await _context.Flights.FindAsync(reservation.FlightId);
            ViewData.Add("BusinessSeats", flight.BusinessCapacity);
            ViewData.Add("RegularSeats", flight.PassengerCapacity);

            var passengerReservationViewModel = new PassengerReservationViewModel() { PassengerCount = count, CurrentCount = count, ReservationId = reservationId };
            return View(passengerReservationViewModel);
        }

        // POST: Reservations/AddPassengers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPassengers(int? rnd = null)
        {
            var model = new PassengerReservationViewModel(this.Request.Form);
            var reservation = await _context.Reservations.FindAsync(model.ReservationId);
            var flight = await _context.Flights.FindAsync(reservation.FlightId);
            ViewData.Add("BusinessSeats", flight.BusinessCapacity);
            ViewData.Add("RegularSeats", flight.PassengerCapacity);

            if (ModelState.IsValid)
            {
                var passenger = await _context.Passengers.FirstOrDefaultAsync(x => x.PersonalNo == model.PersonalNo);
                if (passenger == null)
                {
                    var newPassenger = new Passenger()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        Nationality = model.Nationality,
                        PersonalNo = model.PersonalNo,
                        Telephone = model.Telephone
                    };
                    _context.Passengers.Add(newPassenger);
                    await _context.SaveChangesAsync();
                    passenger = await _context.Passengers.FindAsync(newPassenger.Id);
                }

                int capacity = 0;
                TicketTypes? ticketType = null;
                switch (model.TicketType)
                {
                    case 1:
                        {
                            capacity = reservation.Flight.PassengerCapacity;
                            ticketType = TicketTypes.Regular;
                            break;
                        }
                    case 2:
                        {
                            capacity = reservation.Flight.BusinessCapacity;
                            ticketType = TicketTypes.Business;
                            break;
                        }
                }

                if (capacity > 0)
                {
                    var relation = new PassengerReservation() 
                    { 
                        Passenger = passenger,
                        PassengerId = passenger.Id,
                        ReservationId = reservation.Id,
                        Reservation = reservation,
                        TicketType = (TicketTypes)ticketType 
                    };

                    reservation.Passengers.Add(relation);
                    passenger.Reservations.Add(relation);
                    await _context.SaveChangesAsync();
                }                

                model = new PassengerReservationViewModel()
                {
                    ReservationId = model.ReservationId,
                    CurrentCount = model.CurrentCount - 1,
                    PassengerCount = model.PassengerCount
                };
            }

            if (model.CurrentCount == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "FlightNumber", reservation.FlightId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,IsConfirmed,FlightId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "FlightNumber", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
