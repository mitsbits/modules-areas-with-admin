using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdminControllerAttribute : Attribute
    {
        public readonly string Prefix;
        public AdminControllerAttribute(string prefix)  
        {
            Prefix = prefix;
        }
    }
}
