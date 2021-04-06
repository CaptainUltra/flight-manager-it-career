namespace FlightsManager.Data.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string PersonalNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Nationality { get; set; }

        //public List<PassengerReservation> Reservations { get; set; }
    }
}
