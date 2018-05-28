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
        private IUserService userService;
        private MappingHelper helper;
        private CurrentUserModel CurrentUser
        {
            get
            {
                var id = HttpContext.User.Identity.GetUserId();

                var lotDTOs = lotService.GetLots();
                var lotModels = helper.Mapper
                    .Map<IEnumerable<LotDTO>, ICollection<LotModel>>(lotDTOs)
                    .Where(x => x.UserId == HttpContext.User.Identity.GetUserId()).ToList();

                var userModel = new CurrentUserModel
                {
                    Id = id,
                    Lots = lotModels
                };

                return userModel;
            }
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

        private void CheckLotsExpiration()
        {
            lotService.GetLots()
                .Where(x => x.FinishDate < DateTime.Now)
                .ToList()
                .ForEach(x => lotService.Expire(x.Id));
        }

        public LotController(ILotService lotService, ICategoryService categoryService, IUserService userService)
        {
            this.userService = userService;
            this.lotService = lotService;
            this.categoryService = categoryService;
            helper = MappingHelper.GetInstance();
            CheckLotsExpiration();
        }


        public ActionResult Index(string category, string searchString)
        {
            IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
            var lots = helper.Mapper
                .Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs)
                .Where(x => x.IsConfirmed && !x.IsFinished);

            IEnumerable<CategoryDTO> categoryDTOs = categoryService.GetCategories();
            var categories = helper.Mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryDTOs);

            ViewBag.category = new SelectList(categories);

            if (!String.IsNullOrEmpty(searchString))
                lots = (lots.Where(s => s.Name.Contains(searchString))).ToList();

            if (!string.IsNullOrEmpty(category))
                lots = (lots.Where(x => x.Category.Name == category)).ToList();

            return View(lots);
        }

        public ActionResult Details(int id)
        {
            var lot = lotService.GetLotById(id);

            var userId = CurrentUser.Id;

            ViewBag.CurrentUserId = userId;

            if (lot.Biddings.Count > 0)
                ViewBag.LastBidderName = userService.GetUserById(lot.Biddings.Last().UserId).Name;

            LotModel lotModel = helper.Mapper.Map<LotModel>(lot);

            if (lot == null)
                return View("~/Views/Lot/NotFound.cshtml");
            else
                return View(lotModel);
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
                    StartPrice = model.StartPrice,
                    CurrentPrice = model.StartPrice,
                    BidRate = model.BidRate,
                    Category = categoryService.GetCategory(selectedId),
                    UserId = CurrentUser.Id,
                };

                lotService.CreateLot(lotDTO);

                return RedirectToAction("Index");
            }

            return View("CreateLot", model);
        }

        public ActionResult UserLotsListing()
        {
            var lots = CurrentUser.Lots;

            ViewBag.CurrentUserId = CurrentUser.Id;

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
                    StartPrice = model.StartPrice,
                    BidRate = model.BidRate,
                    CurrentPrice = model.StartPrice,
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