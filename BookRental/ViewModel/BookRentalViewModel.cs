﻿using BookRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookRental.Utility;

namespace BookRental.ViewModel
{
	public class BookRentalViewModel
	{
		public int Id { get; set; }

		//Book Model
		public int BookId { get; set; }
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }

		[DataType(DataType.ImageUrl)]
		public string ImageUrl { get; set; }

		[Range(0, 1000)]
		public int Availability { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public double Price { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
		public DateTime DateAdded { get; set; }
		public int GenreId { get; set; }
		public Genre Genre { get; set; }
		public string Publisher { get; set; }

		[Display(Name = "Publication Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0: MMM dd yyyy}")]
		public DateTime PublicationDate { get; set; }
		public int Pages { get; set; }

		[DisplayName("Product Dimensions")]
		public string ProductDimensions { get; set; }

		//BookRent

		[DataType(DataType.Date)]
		[DisplayName("Start Date")]
		[DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
		public DateTime? StartDate { get; set; }
		[DisplayName("Scheduled End Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
		public DateTime? ScheduledEndDate { get; set; }
		[DisplayName("Additional End Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
		public DateTime? ActualEndDate { get; set; }
		[DisplayName("Additional Charge")]
		public double? AdditionalCharge { get; set; }
		[DisplayName("Rental Price")]
		public double RentalPrice { get; set; }
		public string RentalDuration { get; set; }
		public String Status { get; set; }

		[DisplayName("Rental Price")]
		public double RentalPriceOneMonth { get; set; }
		[DisplayName("Rental Price")]
		public double RentalPriceSixMonth { get; set; }

		//Users 
		public string UserId { get; set; }
		public string Email { get; set; }
		[DisplayName("First Name")]
		public string FirstName { get; set; }
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		public string Name { get { return FirstName + " " + LastName; } }
		[DisplayName("Birth Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
		public DateTime? BirthDate
		{
			get; set;
		}

		public string actionName
		{
			get 
			{
				if(Status.ToLower().Contains(SD.RequestedLower))
				{
					return "Approve";
				}
				if(Status.ToLower().Contains(SD.ApprovedLower))
				{
					return "Pickup";
				}
				if(Status.ToLower().Contains(SD.RentedLower))
				{
					return "Return";
				}
				return null;
			}
		}
	}
}