﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TermPaper.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
	}
}