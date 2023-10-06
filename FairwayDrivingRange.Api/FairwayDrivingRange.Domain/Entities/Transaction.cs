namespace FairwayDrivingRange.Domain.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }

        //public CustomerInformation CustomerInformation { get; set; }

        //public int CustomerId { get; set; }

        public Booking Booking { get; set; }

        public int BookingId { get; set; }

        public double? ClubPrice { get; set; }

        public double BookingPrice { get; set; }

        public double Total { get; set; }
    }
}
