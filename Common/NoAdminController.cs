using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Common
{
    public class NoAdminController : IRouteConstraint
    {
        private List<string> _bucket = new List<string>();
        public NoAdminController()
        {
       
            var asmbls = Assembly.GetCallingAssembly();
            foreach (var iAdmin in asmbls.DefinedTypes.Where(x => x.GetInterfaces().Count(i => i.Name == "IAdminController") > 0).ToList())
            {
                    _bucket.Add(iAdmin.Name.ToLower().Replace("controller", string.Empty));
            }

        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !_bucket.Contains(values["controller"].ToString().ToLower());
        }
    }
}
