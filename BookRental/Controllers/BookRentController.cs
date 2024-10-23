using BookRental.Models;
using BookRental.Utility;
using BookRental.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
	public class BookRentController : Controller
	{
		private readonly ApplicationDbContext _context;
		public BookRentController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: BookRent
		public ActionResult Create(string title = null, string ISBN = null)
		{
			if (title != null && ISBN != null)
			{
				var model = new BookRentalViewModel
				{
					Title = title,
					ISBN = ISBN
				};
				return View(model);
			}
			return View(new BookRentalViewModel());
		}


		[HttpPost]
		public ActionResult Create(BookRentalViewModel bookRentVM)
		{
			if (ModelState.IsValid)
			{
				var email = bookRentVM.Email;
				
				var userDetails = from u in _context.Users
								  where u.Email.Equals(email)
								  select new { u.Id, u.FirstName, u.LastName, u.BirthDate };


				var ISBN = bookRentVM.ISBN;

				var bookSelected = _context.Books.Where(x => x.ISBN.Equals(ISBN)).FirstOrDefault();


				var rentalDuration = bookRentVM.RentalDuration;

				var chargeRate = from u in _context.Users
								 join m in _context.membershipTypes
								 on u.MembershipId equals m.Id
								 where u.Email==email
								 select new { m.ChargeRateOneMonth, m.ChargeRateSixMonth };

				var oneMonthRental = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
				var sixMonthRental = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;

				double rentalPrice = 0;

				if (bookRentVM.RentalDuration == SD.SixMonthCount)
				{
					rentalPrice = sixMonthRental;
				}
                else
                {
					rentalPrice = oneMonthRental;
                }

				var Model = new BookRentalViewModel
				{
					BookId = bookSelected.Id,
					RentalPrice = rentalPrice,
					Price = bookSelected.Price,
					Pages = bookSelected.Pages,
					FirstName = userDetails.ToList()[0].FirstName,
					LastName = userDetails.ToList()[0].LastName,
					BirthDate = userDetails.ToList()[0].BirthDate,
					ScheduledEndDate = bookRentVM.ScheduledEndDate,
					Author = bookSelected.Author,
					Availability = bookSelected.Avaibility,
					DateAdded = (DateTime)bookSelected.DateAdded,
					Description = bookSelected.Description,
					Email = email,
					GenreId = bookRentVM.GenreId,
					Genre = _context.Genres.Where(g => g.Id.Equals(bookSelected.GenreId)).First(),
					ISBN = bookSelected.ISBN,
					ImageUrl = bookSelected.ImageUrl,
					ProductDimensions = bookSelected.ProductDimensions,
					PublicationDate = bookSelected.PublicationDate,
					Publisher = bookSelected.Publisher,
					RentalDuration = bookRentVM.RentalDuration,
					Status = BookRent.StatusEnum.Requested.ToString(),
					Title = bookSelected.Title,
					UserId = userDetails.ToList()[0].Id,
					RentalPriceOneMonth = oneMonthRental,
					RentalPriceSixMonth = sixMonthRental
				};

				BookRent modelToAddToDb = new BookRent
				{
					BookId = bookSelected.Id,
					RentalPrice = rentalPrice,
					ScheduledEndDate = bookRentVM.ScheduledEndDate,
					RentalDuration = bookRentVM.RentalDuration,
					Status = BookRent.StatusEnum.Approved,
					UserId = userDetails.ToList()[0].Id
				};

				bookSelected.Avaibility -= 1;
				_context.BookRental.Add(modelToAddToDb);
				_context.SaveChanges();

				return RedirectToAction("Index");

			}
			return View();
		}

		public ActionResult Index()
		{
			string userid = User.Identity.GetUserId();
			var model = from br in _context.BookRental
						join b in _context.Books on br.BookId equals b.Id
						join u in _context.Users on br.UserId equals u.Id
						select new BookRentalViewModel
						{
							Id = br.Id,
							BookId = b.Id,
							RentalPrice = br.RentalPrice,
							Price = b.Price,
							Pages = b.Pages,
							FirstName = u.FirstName,
							LastName = u.LastName,
							BirthDate = u.BirthDate,
							ScheduledEndDate = br.ScheduledEndDate,
							Author = b.Author,
							Availability = b.Avaibility,
							DateAdded = (DateTime)b.DateAdded,
							Description = b.Description,
							Email = u.Email,
							GenreId = b.GenreId,
							Genre = _context.Genres.Where(g => g.Id.Equals(b.GenreId)).FirstOrDefault(),
							ISBN = b.ISBN,
							ImageUrl = b.ImageUrl,
							ProductDimensions = b.ProductDimensions,
							PublicationDate = b.PublicationDate,
							Publisher = b.Publisher,
							RentalDuration = br.RentalDuration,
							Status = br.Status.ToString(),
							Title = b.Title,
							UserId = u.Id

						};
			if (!User.IsInRole(SD.AdminUserRole))
			{
				model = model.Where(u => u.UserId.Equals(userid));

			}
			return View(model.ToList());
		}
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}