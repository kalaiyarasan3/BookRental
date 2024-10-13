using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.Extensions
{
	public static class thumbnailExtensions
	{
		public static IEnumerable<ThumbnailModel> GetBookThumbnail(this IEnumerable<ThumbnailModel> thumbnails, ApplicationDbContext _context=null,string search=null)
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

			if (search != null)
			{
				var searchResult=thumbnails.Where(t => t.Title.ToLower().Contains(search.ToLower())).OrderBy(t => t.Title);
				return searchResult;
			}

			return thumbnails.OrderBy(t => t.Title);
		}
	}
}