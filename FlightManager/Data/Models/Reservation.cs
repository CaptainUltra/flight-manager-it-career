using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsManager.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        //public List<PassengerReservation> Passengers { get; set; }
    }
}
