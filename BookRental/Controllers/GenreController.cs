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

        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genre = _context.Genres.Find(id);
            if(genre==null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }

            var genreInDb=_context.Genres.Find(genre.Id);

            if(genreInDb==null)
            {
                return HttpNotFound();
            }
            genreInDb.Name = genre.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genreInDb = _context.Genres.Find(id);
            if(genreInDb==null)
            {
                return HttpNotFound();
            }
            return View(genreInDb);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Genre genre)
        {
            var genreInDb = _context.Genres.Find(genre.Id);
            _context.Genres.Remove(genreInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}