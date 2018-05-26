using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TermPaper.Models
{
	public class LotModel
	{
		public int Id { get; set; }

        [Required]
		public string Name { get; set; }

        [Required]
		public string Description { get; set; }

        [Required]
		public decimal Price { get; set; }

		public bool IsConfirmed { get; set; }

        public string UserId { get; set; }

        public CategoryModel Category { get; set; }


        [Display(Name = "Category")]
        [Required]
        public string SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}