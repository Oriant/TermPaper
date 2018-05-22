using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TermPaper.Models;

namespace TermPaper.Controllers
{
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private IManagerService service;
        private IMapper lotMapper; 


        public ManagerController(IManagerService service)
        {
            this.service = service;
            lotMapper = new MapperConfiguration(cfg => cfg.CreateMap<LotDTO, LotModel>()).CreateMapper();
        }

        public ActionResult Index()
        {
            IEnumerable<LotDTO> lotsDTOs = service.GetUnconfirmedLots();
            var lots = lotMapper.Map<IEnumerable<LotDTO>, List<LotModel>>(lotsDTOs);

            return View(lots);
        }

        public ActionResult Confirm(int id)
        {
            service.ConfirmLot(id);
            return RedirectToAction("Index");
        }

        public ActionResult Decline(int id)
        {
            service.DeclineLot(id);
            return RedirectToAction("Index");
        }
    }
}