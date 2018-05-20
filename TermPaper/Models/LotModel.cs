using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TermPaper.Models
{
	public class LotModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public bool IsConfirmed { get; set; }

		[Display(Name = "Category")]
		public virtual CategoryDTO Category { get; set; }
		public int CategoryId { get; set; }

		public string UserId { get; set; }
		//public virtual UserDTO User { get; set; }
	}
}