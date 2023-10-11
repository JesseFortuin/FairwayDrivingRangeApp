namespace FairwayDrivingRange.Domain.Entities
{
    public class GolfClub : IEntity
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public int? BookingId { get; set; }

        public Booking Booking { get; set; }

        public bool IsAvailable { get; set; }
    }
}
