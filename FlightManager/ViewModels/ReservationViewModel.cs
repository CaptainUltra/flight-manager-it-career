using FlightsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public List<PassengerReservation> Passengers { get; set; }
        public int PassengerCount { get; set; }
    }
}
