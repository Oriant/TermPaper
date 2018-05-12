using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TermPaper.Controllers
{
    public class HomeController : Controller
    {
        readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Description page.";

            return View();
        }
    }
}