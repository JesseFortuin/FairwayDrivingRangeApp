namespace FairwayDrivingRange.Shared.Dtos
{
    public class GolfClubDto
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public int? BookingId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
