﻿namespace FairwayDrivingRange.Shared.Dtos
{
    public class UpdateBookingDto
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }
    }
}
