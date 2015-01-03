using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Common;
using WebGrease;

namespace Host.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        private static Assembly[] ScopedAssemblies()
        {
            return
                BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .ToArray();
        }

        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterAdminRoutes(context);

            var r = context.MapRoute(
                 "Admin_default",
                 AreaName + "/{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new { controller = new NoAdminController() },
                 new[] { "Host.Areas.Admin.Controllers" }
             );

            r.DataTokens["UseNamespaceFallback"] = false;
        }

        private  void RegisterAdminRoutes(AreaRegistrationContext context)
        {
            var asmbls = ScopedAssemblies();
            foreach (var assembly in asmbls)
            {
                foreach (var iAdmin in assembly.DefinedTypes.Where(x => x.GetInterfaces().Count(i => i.Name == "IAdminController") > 0).ToList())
                {
                    var attrs = Attribute.GetCustomAttributes(iAdmin);

                    foreach (var attribute in attrs)
                    {
                        var controllerAttribute = attribute as AdminControllerAttribute;
                        if (controllerAttribute == null) continue;
                        var prefix = controllerAttribute.Prefix;
                        var controller = iAdmin.Name.Replace("Controller", string.Empty);
                        var nmspace = iAdmin.Namespace;

                        var rootPrefix = string.Format("{0}-{1}", AreaName, prefix);

                        var r = context.MapRoute(
                            string.Format("{0}_{1}", prefix, controller),
                            string.Format("{0}/{{controller}}/{{action}}/{{id}}", rootPrefix),
                            new { controller, id = UrlParameter.Optional },
                             new { controller = new YesAdminController() },
                            new[] { nmspace }
                            );

                        r.DataTokens["UseNamespaceFallback"] = false;
                    }
                }
            }

        }
    }
}