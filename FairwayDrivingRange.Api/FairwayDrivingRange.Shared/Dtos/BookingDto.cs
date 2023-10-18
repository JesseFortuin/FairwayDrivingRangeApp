namespace FairwayDrivingRange.Shared.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsCancelled { get; set; }
    }
}
