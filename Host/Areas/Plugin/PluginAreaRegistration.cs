using System.Web.Mvc;
using Common;

namespace Host.Areas.Pugin
{
    public class PluginAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Plugin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
           var r =  context.MapRoute(
                "Plugin_default",
                AreaName + "/{controller}/{action}/{id}",
                new { controller="Functional", action = "Index", id = UrlParameter.Optional } ,
                null,
                new[] { "Host.Areas.Plugin.Controllers" }
            );
           r.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}