namespace FairwayDrivingRange.Domain.Entities
{
    public class Booking : IEntity
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End {  get; set; }

        public CustomerInformation Customer { get; set; }

        public int? CustomerId { get; set; }

        public Transaction Transaction { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCancelled { get; set; }

        public List<GolfClub> Clubs { get; set; } = new List<GolfClub>();
    }
}
