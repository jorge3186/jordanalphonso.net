using fulcrum_api.Attributes.Actions;
using fulcrum_api.Controllers;
using fulcrum_common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace fulcrum_api.Resolvers.HttpRouteResolver
{
    public class FulcrumRouteFactory
    {
        private FulcrumRouteFactory() { }

        public static HttpRoute generateRoute(IDictionary<string, object> generics)
        {
            string routeTemplate = DictionaryUtils.getByValueByKey(generics, "route") as string;
            HttpRouteValueDictionary dataTokens = new HttpRouteValueDictionary();
            HttpRouteValueDictionary def = new HttpRouteValueDictionary(generics);
            HttpActionDescriptor desc = generateDescriptor(generics);
            generateRouteParam(routeTemplate, def);

            dataTokens.Add("actions", new HttpActionDescriptor[] { desc });

            return new HttpRoute(routeTemplate, def, null, dataTokens);
        }

        private static HttpActionDescriptor generateDescriptor(IDictionary<string, object> generics)
        {
            var controller = DictionaryUtils.getByValueByKey(generics, "controller") as string;
            var action = DictionaryUtils.getByValueByKey(generics, "action") as string;

            return new FulcrumActionDescriptor(action, typeof(IDictionary<string, string>), typeof(HomeController));
        }

        private static void generateRouteParam(string routeTemplate, HttpRouteValueDictionary defaults)
        {
            if (routeTemplate.Contains("{"))
            {
                List<string> parameters = StringUtil.extractSubString(routeTemplate, '{', '}');
              
                foreach (var p in parameters)
                {
                    Type paramType = null;
                    if (p.IndexOf(':') > 0)
                    {
                        paramType = TypeUtil.getGenericType(p.Split(':')[1]);

                    }
                    defaults.Add(p.Split(':')[0], paramType);
                }
            }
        }
    }
}