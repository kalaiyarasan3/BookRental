using BookRental.Models;
using BookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}