using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
    public class GenreController : Controller
    {

        private readonly ApplicationDbContext _context;
        public GenreController()
        {
            _context=new ApplicationDbContext();
        }
        // GET: Genre
        public ActionResult Index()
        {
            var genre = _context.Genres.ToList();
            return View(genre);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if(ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genre = _context.Genres.Find(id);
            if(genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}