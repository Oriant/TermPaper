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

namespace TermPaper.Controllers
{
	public class LotController : Controller
	{
		private ILotService lotService;
		private ICategoryService categoryService;

		public LotController(ILotService lotService, ICategoryService categoryService)
		{
			this.lotService = lotService;
			this.categoryService = categoryService;
		}

		public ActionResult Index(string lotCategory, string searchString)
		{
			IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
			var lotMapper = new MapperConfiguration(cfg => cfg.CreateMap<LotDTO, LotModel>()).CreateMapper();
			var lots = lotMapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

			IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
			var categoryMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
			var categories = categoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

			ViewBag.lotCategory = new SelectList(categories);

			if (!String.IsNullOrEmpty(searchString))
			{
				lots = (lots.Where(s => s.Name.Contains(searchString))).ToList();
			}

			if (!string.IsNullOrEmpty(lotCategory))
			{
				lots = (lots.Where(x => x.Category.Name == lotCategory)).ToList();
			}

			return View(lots);
		}

		public ActionResult Details(int id)
		{
			var lot = lotService.GetLotById(id);

			var lotMapper = new MapperConfiguration(cfg => cfg.CreateMap<LotDTO, LotModel>()).CreateMapper();
			LotModel lotModel = lotMapper.Map<LotModel>(lot);

			if (lot == null)
				return View("~/Views/Lot/NotFound.cshtml");
			else
				return View(lotModel);
		}


		/*
		public ActionResult MakeLot(int id)
		{
			try
			{
				IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
				var categoryMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
				var categories = categoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

				ViewBag.lotCategory = new SelectList(categories);

				CategoryDTO category = categoryService.GetCategory(id);
				var lot = new LotModel { CategoryId = id };

				return View(lot);
			}
			catch (BLL.Infrastructure.ValidationException ex)
			{
				return Content(ex.Message);
			}
		}
		[HttpPost]
		public ActionResult MakeLot(LotModel lot)
		{
			try
			{
				var lotDto = new LotDTO { Name = lot.Name, Description = lot.Description, IsConfirmed = lot.IsConfirmed, CategoryId = lot.CategoryId, Price = lot.Price };
				lotService.CreateLot(lotDto);
				return Content("<h2>Ваш заказ успешно оформлен</h2>");
			}
			catch (BLL.Infrastructure.ValidationException ex)
			{
				ModelState.AddModelError(ex.Message, ex.Property);
			}
			return View(lot);
		}
		*/
		public ActionResult MakeLot()
		{
			IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
			var categoryMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
			var categories = categoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

			ViewBag.lotCategory = new SelectList(categories);
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult MakeLot([Bind(Include = "Id,Name,Descriotion,Price,isConfirmed,Category")] LotDTO lotDto)
		{
			if (ModelState.IsValid)
			{
				lotService.CreateLot(lotDto);
				return RedirectToAction("Index");
			}

			return View(lotDto);
		}
	}
}