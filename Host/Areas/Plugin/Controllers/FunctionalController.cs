using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Host.Areas.Plugin.Controllers
{
    public class FunctionalController : Controller
    {

        public PartialViewResult Widget()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}