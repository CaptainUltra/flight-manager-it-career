using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels
{
    public class PassengerReservationViewModel
    {
        public PassengerReservationViewModel()
        { }
        public PassengerReservationViewModel(IFormCollection form)
        {
            FirstName = form.FirstOrDefault(x => x.Key == "FirstName").Value;
            LastName = form.FirstOrDefault(x => x.Key == "LastName").Value;
            MiddleName = form.FirstOrDefault(x => x.Key == "MiddleName").Value;
            Nationality = form.FirstOrDefault(x => x.Key == "Nationality").Value;
            PersonalNo = form.FirstOrDefault(x => x.Key == "PersonalNo").Value;
            Telephone = form.FirstOrDefault(x => x.Key == "Telephone").Value;
            TicketType = int.Parse(form.FirstOrDefault(x => x.Key == "TicketType").Value);
            CurrentCount = int.Parse(form.FirstOrDefault(x => x.Key == "CurrentCount").Value);
            PassengerCount = int.Parse(form.FirstOrDefault(x => x.Key == "PassengerCount").Value);
            ReservationId = int.Parse(form.FirstOrDefault(x => x.Key == "ReservationId").Value);
        }
        public int ReservationId { get; set; }
        public int PassengerCount { get; set; }
        public int CurrentCount { get; set; }
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
        public int TicketType { get; set; }
    }
}
