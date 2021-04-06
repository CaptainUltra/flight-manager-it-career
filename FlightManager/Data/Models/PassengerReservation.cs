using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsManager.Data.Models
{
    public enum TicketTypes
    {
        Regular,
        Business
    }
    public class PassengerReservation
    {
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public TicketTypes TicketType { get; set; }
    }
}
