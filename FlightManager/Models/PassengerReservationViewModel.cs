using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
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
            CurrentCount = Int32.Parse(form.FirstOrDefault(x => x.Key == "CurrentCount").Value);
            PassengerCount = Int32.Parse(form.FirstOrDefault(x => x.Key == "PassengerCount").Value);
            ReservationId = Int32.Parse(form.FirstOrDefault(x => x.Key == "ReservationId").Value);
        }
        public int ReservationId { get; set; }
        public int PassengerCount { get; set; }
        public int CurrentCount { get; set; }
        public string PersonalNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Nationality { get; set; }
    }
}
