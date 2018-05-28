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
        [Display(Name = "Start price")]
		public decimal StartPrice { get; set; }

        [Display(Name = "Current price")]
        public decimal CurrentPrice { get; set; }

        [Required]
        [Display(Name = "Bid Rate")]
        public decimal BidRate { get; set; }

		public bool IsConfirmed { get; set; }

        public bool IsFinished { get; set; }

        [Display(Name = "Date of start")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Date of finish")]
        public DateTime FinishDate { get; set; }

        public string UserId { get; set; }

        public CategoryModel Category { get; set; }

        public ICollection<BiddingModel> Biddings { get; set; }



        [Display(Name = "Category")]
        [Required]
        public string SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}