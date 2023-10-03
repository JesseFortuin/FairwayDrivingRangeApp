namespace FairwayDrivingRange.Domain.Entities
{
    public class Booking : IEntity
    {
        public int Id { get; set; }

        public DateTime DateBooked { get; set; }

        public DateTime End {  get; set; }

        public CustomerInformation Customer { get; set; }

        public int CustomerId { get; set; }

        public List<GolfClub> Clubs { get; set; } = new List<GolfClub>();
    }
}
