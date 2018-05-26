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
using TermPaper.Util;

namespace TermPaper.Controllers
{
    public class LotController : Controller
    {
        private ILotService lotService;
        private ICategoryService categoryService;
        private readonly IUserService userService;
        private MappingHelper helper;

        public LotController(ILotService lotService, ICategoryService categoryService, IUserService userService)
        {
            this.userService = userService;
            this.lotService = lotService;
            this.categoryService = categoryService;
            helper = MappingHelper.GetInstance();
        }

        public ActionResult Index(string category, string searchString)
        {
            IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
            var lots = helper.Mapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

            IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
            var categories = helper.Mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

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

            LotModel lotModel = helper.Mapper.Map<LotModel>(lot);

            if (lot == null)
                return View("~/Views/Lot/NotFound.cshtml");
            else
                return View(lotModel);
        }

        public ActionResult UserLotListing()
        {
            ViewBag.currentUserId = HttpContext.User.Identity.GetUserId();

            return View();
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
            var categories = Mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(categoryDTOs);

            return categories;
        }

        public ActionResult CreateLot()
        {
            var model = new LotModel();
            var categories = GetCategories();

            model.Categories = GetSelectListItems(categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLot(LotModel model)
        {
            var categories = GetCategories();
            model.Categories = GetSelectListItems(categories);

            if (ModelState.IsValid)
            {
                Int32.TryParse(model.SelectedCategoryId, out int selectedId);

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

            return View("CreateLot", model);
        }

        public ActionResult UserLotsListing()
        {
            IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
            var lots = helper.Mapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

            ViewBag.CurrentUserId = HttpContext.User.Identity.GetUserId();

            return View(lots);
        }

        public ActionResult Edit(int id)
        {
            var lot = lotService.GetLotById(id);
     
            var categories = GetCategories();
            
            var lotModel = helper.Mapper.Map<LotModel>(lot);
            lotModel.Categories = GetSelectListItems(categories);

            return View(lotModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LotModel model)
        {

            var categories = GetCategories();
            model.Categories = GetSelectListItems(categories);

            if (ModelState.IsValid)
            {
                Int32.TryParse(model.SelectedCategoryId, out int selectedId);

                LotDTO lotDTO = new LotDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = categoryService.GetCategory(selectedId),
                    UserId = HttpContext.User.Identity.GetUserId(),
                };

                lotService.Edit(lotDTO);

                return RedirectToAction("UserLotsListing");
            }

            return View("Edit", model);
        }
    }
}