using AutoMapper;
using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TermPaper.Models;

namespace TermPaper.Controllers
{
	public class LotController : Controller
	{
		private LotService lotService;

		public LotController(LotService serv)
		{
			lotService = serv;
		}

		public ActionResult Index(string lotCategory, string searchString)
		{
			IEnumerable<LotDTO> lotsDTOs = lotService.GetLots();
			var lotMapper = new MapperConfiguration(cfg => cfg.CreateMap<LotDTO, LotModel>()).CreateMapper();
			var lots = lotMapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

			IEnumerable<CategoryDTO> categoryDTOs = lotService.GetCategories();
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
                return View("Not Found");
            else
                return View(lotModel);
        }



		/*public ActionResult MakeLot(int? id)
		{
			try
			{
				PhoneDTO phone = orderService.GetPhone(id);
				var order = new OrderViewModel { PhoneId = phone.Id };

				return View(order);
			}
			catch (ValidationException ex)
			{
				return Content(ex.Message);
			}
		}
		[HttpPost]
		public ActionResult MakeOrder(OrderViewModel order)
		{
			try
			{
				var orderDto = new OrderDTO { PhoneId = order.PhoneId, Address = order.Address, PhoneNumber = order.PhoneNumber };
				orderService.MakeOrder(orderDto);
				return Content("<h2>Ваш заказ успешно оформлен</h2>");
			}
			catch (ValidationException ex)
			{
				ModelState.AddModelError(ex.Property, ex.Message);
			}
			return View(order);
		}
		protected override void Dispose(bool disposing)
		{
			orderService.Dispose();
			base.Dispose(disposing);
		}
	*/
	}
}