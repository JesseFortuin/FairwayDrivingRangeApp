namespace FairwayDrivingRange.Shared.Dtos
{
    public class TransactionDto
    {
        public int id { get; set; }

        public int bookingId { get; set; }

        public double? clubPrice { get; set; }

        public double bookingPrice { get; set; }

        public double total { get; set; }
    }
}
