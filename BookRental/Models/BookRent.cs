using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Models
{
	public class BookRent
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ScheduledStarDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public double RentalPrice { get; set; }
        public string RentalDuration { get; set; }
        public StatusEnum Status { get; set; }

        public enum StatusEnum
        {
            Requested,
            Approved,
            Rejected,
            Rented,
            Closed
        }
    }
}