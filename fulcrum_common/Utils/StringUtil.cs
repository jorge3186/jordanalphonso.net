using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_common.Utils
{
    public class StringUtil
    {
        public const string E = "";

        public static List<string> extractSubString(string main, char beginning, char end)
        {
            List<string> extracted = main.Split(beginning, end)
                    .Where((item, index) => index % 2 != 0).ToList();
            return extracted;
        }

        public static string resolveRoute(string route)
        {
            string resolvedRoute;
            if (route != null && route.ToString().StartsWith("/"))
            {
                resolvedRoute = route.ToString().Substring(1);
            }
            else
            {
                resolvedRoute = route.ToString();
            }
            return resolvedRoute;
        }

        public static string extractTypeFromRouteParamter(string routeParameter)
        {
            if (routeParameter.IndexOf(':') > 0)
            {
                return routeParameter.Split(':')[0];
            }
            return E;
        }
    }
}
