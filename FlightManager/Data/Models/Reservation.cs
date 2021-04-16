using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightsManager.Data.Models
{
    public class Reservation
    {
        public Reservation()
        {
            this.Passengers = new List<PassengerReservation>();
        }
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        [Required]
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        public List<PassengerReservation> Passengers { get; set; }
    }
}
