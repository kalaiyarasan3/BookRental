﻿using BookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRental.ViewModel
{
	public class BookViewModel
	{
		public IEnumerable<Genre> Genre { get; set; }
		public Book Book { get; set; }
	}
}