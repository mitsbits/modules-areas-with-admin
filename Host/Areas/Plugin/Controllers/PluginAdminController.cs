using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Host.Areas.Plugin.Controllers
{
    [AdminController("Plugin")]
    public class PluginAdminController : Controller, IAdminController
    {
        // GET: Pugin/PluginAdmin
        public  ActionResult Index()
        {
            return View("~/Areas/Plugin/Views/PluginAdmin/Index.cshtml");
        }


     
    }
}