using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Extensions
{
	public static class thumbnailExtensions
	{
		public static IEnumerable<ThumbnailModel> GetBookThumbnail(this IEnumerable<ThumbnailModel> thumbnails, ApplicationDbContext _context=null)
		{
			if (_context == null)
				_context = ApplicationDbContext.Create();

			thumbnails = (from b in _context.Books
						  select new ThumbnailModel
						  {
							  BookId = b.Id,
							  Title = b.Title,
							  Description = b.Description,
							  ImageUrl = b.ImageUrl,
							  Link = "/BookDetails/Index" + b.Id
						  }).ToList();


			return thumbnails.OrderBy(b => b.Title);
		}
	}
}