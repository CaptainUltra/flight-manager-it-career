using System;
using System.ComponentModel.DataAnnotations;

namespace FlightsManager.Data.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DepartureDateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ArrivalDateTime { get; set; }
        public string AircraftType { get; set; }
        public string FlightNumber { get; set; }
        public string PilotName { get; set; }
        public int PassengerCapacity { get; set; }
        public int BusinessCapacity { get; set; }
    }
}
