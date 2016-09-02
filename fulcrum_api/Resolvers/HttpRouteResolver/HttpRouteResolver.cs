using fulcrum_api.Attributes.Controller;
using fulcrum_api.Controllers;
using fulcrum_common.Utils;
using fulcrum_services.Models.FulcrumUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace fulcrum_api.Resolvers.HttpRouteResolver
{
    public class HttpRouteResolver
    {
        private static string API = "/_fulcrum";

        public static void registerFulcrumRoutes(HttpConfiguration configuration)
        {
            buildAngularRoute(configuration);

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            IList<Type> controllers = filterOutControllers(types);

            foreach (var c in controllers)
            {
                IDictionary<FulcrumRouteAttribute, MethodInfo> routeInfo = getAllCustomRoutes(c);
                string prefix = getRoutePrefix(c);
                buildRouteDefaults(prefix, routeInfo, c.Name, configuration);
            }
        }

        private static void buildAngularRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("angular", "",
                new { controller = "angularLoader" });
        }

        private static string getRoutePrefix(Type controller)
        {
            if (Attribute.IsDefined(controller, typeof(FulcrumRoutePrefixAttribute)))
            {
                FulcrumRoutePrefixAttribute prefixAttr = controller
                    .GetCustomAttribute<FulcrumRoutePrefixAttribute>();
                return prefixAttr.prefix;
            }
            return StringUtil.E;
        }

        private static IDictionary<FulcrumRouteAttribute, MethodInfo> getAllCustomRoutes(Type controller)
        {
            IDictionary<FulcrumRouteAttribute, MethodInfo> fulcrumRouteInfo = 
                new Dictionary<FulcrumRouteAttribute, MethodInfo>();

            MethodInfo[] methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                if (Attribute.IsDefined(method, typeof(FulcrumRouteAttribute)))
                {
                    FulcrumRouteAttribute attrib = method.GetCustomAttribute<FulcrumRouteAttribute>();
                    fulcrumRouteInfo.Add(attrib, method);
                }
            }
            
            return fulcrumRouteInfo;
        }

        private static void buildRouteDefaults(string prefix, IDictionary<FulcrumRouteAttribute, MethodInfo> routeInfo, 
            string Controller, HttpConfiguration config)
        {
            string controllerName = Controller.Replace("Controller", "");

            foreach (var info in routeInfo)
            {
                IDictionary<string, object> innerInfo = new Dictionary<string, object>();
                innerInfo.Add("controller", controllerName);
                innerInfo.Add("methods", info.Key.HttpMethods);
                innerInfo.Add("action", info.Value.Name);
                innerInfo.Add("credentials", info.Key.Credentials);
                innerInfo.Add("validation", info.Key.Validation);

                string resolvedRoute = StringUtil.resolveRoute(API + prefix + info.Key.Route);

                innerInfo.Add("route", resolvedRoute);

                confiureSpecificRoute(config, innerInfo);
            }
        }

        private static void confiureSpecificRoute(HttpConfiguration config, 
            IDictionary<string, object> info)
        {
            var controllerName = DictionaryUtils.getByValueByKey(info, "controller");
            var actionName = DictionaryUtils.getByValueByKey(info, "action");
            var route = DictionaryUtils.getByValueByKey(info, "route") as string;
            var methods = DictionaryUtils.getByValueByKey(info, "methods") as string[];

            HttpRoute r = FulcrumRouteFactory.generateRoute(info);
            config.Routes.Add(controllerName + "-" + actionName + "-" + methods[0] as string, r);
        }

        private static IList<Type> filterOutControllers(Type[] types)
        {
            IList<Type> controllers = new List<Type>();
            foreach (var t in types)
            {
                if (t.GetTypeInfo().Name.EndsWith("Controller"))
                {
                    controllers.Add(t);
                }
            }
            return controllers;
        }
    }
}