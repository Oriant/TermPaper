using AutoMapper;
using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TermPaper.Models;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace TermPaper.Controllers
{
	public class LotController : Controller
	{
		private ILotService lotService;
		private ICategoryService categoryService;
        private IUserService userService;
        private IMapper lotMapper, categoryMapper; 

        public LotController(ILotService lotService, ICategoryService categoryService, IUserService userService)
		{
            this.userService = userService;
			this.lotService = lotService;
			this.categoryService = categoryService;
            lotMapper = new MapperConfiguration(cfg => cfg.CreateMap<LotDTO, LotModel>()).CreateMapper();
            categoryMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

		public ActionResult Index(string category, string searchString)
		{
			IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
            var lots = lotMapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

			IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
			var categories = categoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

			ViewBag.category = new SelectList(categories);

			if (!String.IsNullOrEmpty(searchString))
			{
				lots = (lots.Where(s => s.Name.Contains(searchString))).ToList();
			}

			if (!string.IsNullOrEmpty(category))
			{
				lots = (lots.Where(x => x.Category.Name == category)).ToList();
			}

			return View(lots);
		}

		public ActionResult Details(int id)
		{
			var lot = lotService.GetLotById(id);

			LotModel lotModel = lotMapper.Map<LotModel>(lot);

			if (lot == null)
				return View("~/Views/Lot/NotFound.cshtml");
			else
				return View(lotModel);
		}


        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<CategoryModel> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }

        private IEnumerable<CategoryModel> GetCategories()
        {
            IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
            var categories = categoryMapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(categoryDTOs);

            return categories;
        }

        public ActionResult MakeLot()
        {
            var model = new CreateLotModel();
            var categories = GetCategories();

            model.Categories = GetSelectListItems(categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeLot(CreateLotModel model)
        {
            var categories = GetCategories();
            model.Categories = GetSelectListItems(categories);

            if (ModelState.IsValid)
            {
                Session["CreateLotModel"] = model;

                Int32.TryParse(model.SelectedCategoryId, out int selectedId);
                string currentUserId = HttpContext.User.Identity.GetUserId();
                string currenUserName = HttpContext.User.Identity.Name;

                LotDTO lotDTO = new LotDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = categoryService.GetCategory(selectedId),
                    UserId = HttpContext.User.Identity.GetUserId()
                };

                lotService.CreateLot(lotDTO);

                return RedirectToAction("Index");
            }

            return View("MakeLot", model);
        }
    }
}