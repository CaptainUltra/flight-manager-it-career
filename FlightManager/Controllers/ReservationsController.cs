using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightsManager.Data.Models;
using FlightManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using System.Text.Encodings.Web;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IEmailSender emailSender;

        public ReservationsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            this.emailSender = emailSender;
        }

        // GET: Reservations
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string emailSearch)
        {
            ViewData["EmailSearch"] = emailSearch;

            var reservations = from r in _context.Reservations
                           select r;
            if (!String.IsNullOrEmpty(emailSearch))
            {
                reservations = reservations.Where(r => r.Email.Contains(emailSearch));
            }
            return View(await reservations.Include(r => r.Flight).ToListAsync());
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Admin,Employee")]
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
                var callbackUrl = Url.Action(
                        "Confirm",
                        "Reservations",
                        values: new { id = reservation.Id },
                        protocol: Request.Scheme);

                reservation = await _context.Reservations
                    .Include(r => r.Flight)
                    .Include(r => r.Passengers)
                    .ThenInclude(p => p.Passenger)
                    .FirstOrDefaultAsync(m => m.Id == model.ReservationId);

                var message = new StringBuilder();
                message.Append($"Моля, потвърдете резервацията си като <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>натиснете тук</a>.");
                message.Append("<br />");
                message.Append($"{reservation.Passengers.Count} общо пътници за полет {reservation.Flight.FlightNumber}. Списък:");
                message.Append("<br />");
                foreach (var passengerReservation in reservation.Passengers)
                {
                    var passenger = passengerReservation.Passenger;
                    message.Append($"{passenger.PersonalNo} {passenger.FirstName} {passenger.MiddleName} {passenger.LastName} {passengerReservation.TicketType}");
                    message.Append("<br />");
                }
                await this.emailSender.SendEmailAsync(reservation.Email, "Потвърждение на резервация", message.ToString());
                return View("ConfirmationNeeded");
            }

            return View(model);
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.IsConfirmed = true;
            try
            {
                _context.Update(reservation);
                var result = await _context.SaveChangesAsync();
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
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
