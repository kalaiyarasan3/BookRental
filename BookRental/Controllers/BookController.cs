using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookRental.ViewModel;
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
		public ActionResult Create()
		{
			var genreInDb = _contect.Genres.ToList();
			var model = new BookViewModel
			{
				Genre= genreInDb
			};
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(BookViewModel viewModel)
		{
			if(!ModelState.IsValid)
			{
				var genreInDb = _contect.Genres.ToList();
				viewModel.Genre = genreInDb;
				return View(viewModel);
			}
			var book = new Book
			{
				Author=viewModel.Book.Author,
				Avaibility=viewModel.Book.Avaibility,
				DateAdded=viewModel.Book.DateAdded,
				Description=viewModel.Book.Description,
				Genre=viewModel.Book.Genre,
				GenreId=viewModel.Book.GenreId,
				ImageUrl=viewModel.Book.ImageUrl,
				ISBN=viewModel.Book.ISBN,
				Pages=viewModel.Book.Pages,
				Price=viewModel.Book.Price,
				ProductDimensions=viewModel.Book.ProductDimensions,
				PublicationDate=viewModel.Book.PublicationDate,
				Publisher=viewModel.Book.Publisher,
				Title=viewModel.Book.Title
			};
			_contect.Books.Add(book);
			_contect.SaveChanges();

			return RedirectToAction("Index");
		}
		protected override void Dispose(bool disposing)
		{
			_contect.Dispose();
		}
	}
}