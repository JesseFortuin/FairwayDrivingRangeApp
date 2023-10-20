namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddBookingDto
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int[] GolfClubIds { get; set; }
    }
}
