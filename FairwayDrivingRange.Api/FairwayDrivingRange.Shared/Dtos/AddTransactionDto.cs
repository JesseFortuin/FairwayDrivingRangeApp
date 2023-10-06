namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddTransactionDto
    {
        public int bookingId { get; set; }

        public double? clubPrice { get; set; }

        public double bookingPrice { get; set; }

        public double total { get; set; }
    }
}
