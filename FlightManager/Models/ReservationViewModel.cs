using FlightsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public List<PassengerReservation> Passengers { get; set; }
        public int PassengerCount { get; set; }
    }
}
