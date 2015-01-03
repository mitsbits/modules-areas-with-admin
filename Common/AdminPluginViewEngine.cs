using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
{
  public  class AdminPluginViewEngine : RazorViewEngine
    {
        public AdminPluginViewEngine()
            : base()
        {
            AreaViewLocationFormats = new[] {

            "~/Areas/%1/Views/{1}/{0}.cshtml",
            "~/Areas/%1/Views/Shared/{0}.cshtml",

        };

        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string prefix;
            if (IsAdminController(controllerContext, out prefix))
            {
                return base.CreatePartialView(controllerContext, partialPath.Replace("%1", prefix));
            }
            return null;
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string prefix;
            if (IsAdminController(controllerContext, out prefix))
            {
                return base.CreateView(controllerContext, viewPath.Replace("%1", prefix), masterPath);
            }
            return null;
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            string prefix;
            if (IsAdminController(controllerContext, out prefix))
            {
                return base.FileExists(controllerContext, virtualPath.Replace("%1", prefix));
            }
            return false;
        }

        private bool IsAdminController(ControllerContext controllerContext, out string prefix)
        {
            var iAdmin = controllerContext.Controller as IAdminController;
            if (iAdmin == null)
            {
                prefix = string.Empty;
                return false;
            }
            var attrs = Attribute.GetCustomAttributes(controllerContext.Controller.GetType());

            if (attrs.Any(a => a is AdminControllerAttribute))
            {
                var attr = attrs.FirstOrDefault(a => a is AdminControllerAttribute) as AdminControllerAttribute;
                if (attr != null)
                {
                    prefix = attr.Prefix;
                    return true;
                }

            }
            prefix = string.Empty;
            return false;
        }
    }
}
