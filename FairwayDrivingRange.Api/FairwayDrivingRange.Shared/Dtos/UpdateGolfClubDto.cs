namespace FairwayDrivingRange.Shared.Dtos
{
    public class UpdateGolfClubDto
    {
        public string SerialNumber { get; set; }

        public string ClubType { get; set; }

        public int? BookingId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
