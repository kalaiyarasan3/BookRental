using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace BookRental.Controllers
{
    public class MembershipTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MembershipTypeController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: MembershipType
        public ActionResult Index()
        {
            return View(_context.membershipTypes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var InDb = _context.membershipTypes.Find(id);
            if (InDb == null)
            {
                return HttpNotFound();
            }
            return View(InDb);
        }

        public ActionResult Create()
        {
            return View(); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipType membershipType)
        {
            if (!ModelState.IsValid)
            {
                return View(membershipType);
            }
            _context.membershipTypes.Add(membershipType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var InDb = _context.membershipTypes.Find(id);
            if (InDb == null) { return HttpNotFound(); }

            return View(InDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembershipType membershipType)
        {
            if (!ModelState.IsValid)
            { return View(membershipType); }

            var Indb = _context.membershipTypes.Find(membershipType.Id);

            if (Indb == null) { return HttpNotFound(); }

            Indb.Name = membershipType.Name;
            Indb.SignUpFee = membershipType.SignUpFee;
            Indb.ChargeRateOneMonth = membershipType.ChargeRateOneMonth;
            Indb.ChargeRateSixMonth = membershipType.ChargeRateSixMonth;

            _context.Entry(Indb).State=EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id) 
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); } 

            var InDb=_context.membershipTypes.Find(id);

            if (InDb == null) { return HttpNotFound(); }

            return View(InDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirem(int? id)
        {
            var InDb = _context.membershipTypes.Find(id);
            _context.membershipTypes.Remove(InDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}