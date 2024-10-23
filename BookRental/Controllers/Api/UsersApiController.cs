using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRental.Controllers.Api
{
    public class UsersApiController : ApiController
    {
        public ApplicationDbContext _context;
        public UsersApiController() {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Get(string type,string query=null)
        {
            if(type.Equals("email")&&query !=null)
                    {
				var customerQuery = _context.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));

				return Ok(customerQuery.ToList());
            }
            if(type.Equals("name")&& query !=null)
            {
                var customerQuery = from u in _context.Users
                                    where u.Email.ToLower().Contains(query)
                                    select new { u.FirstName, u.LastName, u.BirthDate };

				return Ok(customerQuery.ToList()[0].FirstName + " " + customerQuery.ToList()[0].LastName + ";" + customerQuery.ToList()[0].BirthDate);

			}
            return Ok();
        }

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
	}
}
