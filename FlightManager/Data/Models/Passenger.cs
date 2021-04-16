using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightsManager.Data.Models
{
    public class Passenger
    {
        public Passenger()
        {
            this.Reservations = new List<PassengerReservation>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string PersonalNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string Telephone { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]

        public List<PassengerReservation> Reservations { get; set; }
    }
}
