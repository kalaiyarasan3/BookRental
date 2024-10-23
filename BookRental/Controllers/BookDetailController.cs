using BookRental.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookRental.Utility;
using BookRental.ViewModel;

namespace BookRental.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookDetailController()
        {
            _context = new ApplicationDbContext(); 
         
        }
        // GET: BookDetail
       
        public ActionResult Index(int? id=1)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            var bookModel = _context.Books.Include(x => x.Genre).SingleOrDefault(b => b.Id == id);

            var rentalPrice = 0.0;
            var oneMonthRental = 0.0;
            var sixMonthRental = 0.0;

            if (userId != null && !User.IsInRole(SD.AdminUserRole))
            {
                var chargeRate = from u in _context.Users
                                 join m in _context.membershipTypes on u.MembershipId equals m.Id
                                 where u.Id == userId
                                 select new
                                 {
                                     m.ChargeRateOneMonth,
                                     m.ChargeRateSixMonth
                                 };
                oneMonthRental = Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth / 100);
                sixMonthRental=Convert.ToDouble(bookModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth / 100);
            }

            BookRentalViewModel model = new BookRentalViewModel
            {
                BookId = bookModel.Id,
                ISBN = bookModel.ISBN,
                Author = bookModel.Author,
                Availability = bookModel.Avaibility,
                DateAdded = (DateTime)bookModel.DateAdded,
                Description = bookModel.Description,
                Genre = _context.Genres.FirstOrDefault(g => g.Id.Equals(bookModel.GenreId)),
                GenreId = bookModel.GenreId,
                ImageUrl = bookModel.ImageUrl,
                Pages = bookModel.Pages,
                Price = bookModel.Price,
                PublicationDate = bookModel.PublicationDate,
                ProductDimensions = bookModel.ProductDimensions,
                Title = bookModel.Title,
                Publisher = bookModel.Publisher,
                UserId = userId,
                RentalPrice = rentalPrice,
                RentalPriceOneMonth = oneMonthRental,
                RentalPriceSixMonth = sixMonthRental
            };

			return View(model);
        }
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
	}
}