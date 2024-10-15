using BookRental.Models;
using BookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.Ajax.Utilities;

namespace BookRental.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController() 
        {
            _context=new ApplicationDbContext();
        }
        // GET: User
        public ActionResult Index()
        {
            var user = from u in _context.Users
                       join m in _context.membershipTypes on u.MembershipId equals m.Id
                       select new UserViewModel
                       {
                           Id = u.Id,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Email = u.Email,
                           BirthDate = u.BirthDate,
                           Phone = u.Phone,
                           MembershipTypes = (ICollection<MembershipType>)_context.membershipTypes.ToList().Where(n => n.Id.Equals(u.MembershipId)),
                           Disabled = u.Disable
                       };
            var userList=user.ToList();
            return View(userList);
        }

		public ActionResult Details(string id)
		{
			if (id == null || id.Length == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = _context.Users.Find(id);
			UserViewModel model = new UserViewModel()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				BirthDate = user.BirthDate,
				Id = user.Id,
				MembershipTypeId = user.MembershipId,
				MembershipTypes = _context.membershipTypes.ToList(),
				Phone = user.Phone,
				Disabled = user.Disable
			};
			return View(model);
		}

		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = _context.Users.Find(id);

			if (user == null)
			{
				return HttpNotFound();
			}
			UserViewModel model = new UserViewModel()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				BirthDate = user.BirthDate,
				Id = user.Id,
				MembershipTypeId = user.MembershipId,
				MembershipTypes = _context.membershipTypes.ToList(),
				Phone = user.Phone,
				Disabled = user.Disable
			};

			return View(model);
		}


		//POST Method for EDIT Action
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserViewModel user)
		{
			if (!ModelState.IsValid)
			{
				UserViewModel model = new UserViewModel()
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					BirthDate = user.BirthDate,
					Id = user.Id,
					MembershipTypeId = user.MembershipTypeId,
					MembershipTypes = _context.membershipTypes.ToList(),
					Phone = user.Phone,
					Disabled = user.Disabled
				};
				return View(model);
			}
			else
			{
				var userInDb = _context.Users.Single(u => u.Id == user.Id);
				userInDb.FirstName = user.FirstName;
				userInDb.LastName = user.LastName;
				userInDb.BirthDate = user.BirthDate;
				userInDb.Email = user.Email;
				userInDb.MembershipId = user.MembershipTypeId;
				userInDb.Phone = user.Phone;
				userInDb.Disable = user.Disabled;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "User");
		}
		public ActionResult Delete(string id)
		{
			if (id == null || id.Length == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = _context.Users.Find(id);
			UserViewModel model = new UserViewModel()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				BirthDate = user.BirthDate,
				Id = user.Id,
				MembershipTypeId = user.MembershipId,
				MembershipTypes = _context.membershipTypes.ToList(),
				Phone = user.Phone,
				Disabled = user.Disable
			};
			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult UsersDeletePost(string id)
		{
			var userInDb = _context.Users.Find(id);
			if (id == null || id.Length == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			userInDb.Disable = true;
			_context.SaveChanges();
			return RedirectToAction("Index", "User");
		}
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}