﻿namespace FairwayDrivingRange.Shared.Dtos
{
    public class AddBookingDto
    {
        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public int customerId { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public long phone { get; set; }
    }
}
