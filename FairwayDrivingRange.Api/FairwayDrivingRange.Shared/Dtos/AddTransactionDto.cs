namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddTransactionDto
    {
        public int BookingId { get; set; }

        public double? ClubPrice { get; set; }

        public double BookingPrice { get; set; }

        public double Total { get; set; }
    }
}
