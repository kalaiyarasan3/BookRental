using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
	public class BookController : Controller
	{
		private readonly ApplicationDbContext _contect;
		public BookController()
		{
			_contect = new ApplicationDbContext();
		}
		// GET: Book
		public ActionResult Index()
		{
			var bookInDb = _contect.Books.Include(x => x.Genre).ToList();
			return View(bookInDb);
		}

		protected override void Dispose(bool disposing)
		{
			_contect.Dispose();
		}
	}
}