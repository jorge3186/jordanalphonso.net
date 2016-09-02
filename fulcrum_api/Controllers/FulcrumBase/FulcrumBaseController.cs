using fulcrum_api.Security.Filters;
using fulcrum_common.Utils;
using fulcrum_services.Models.SessionManagement;
using fulcrum_services.NHibernate.Criteria;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using System.Xml.Serialization;

namespace fulcrum_api.Controllers.FulcrumBase
{
    public class FulcrumBaseController : IHttpController
    {
        protected FRestrictions restrictions;

        public FulcrumBaseController()
        {
            restrictions = new FRestrictions();
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, 
            CancellationToken cancellationToken)
        {
            IHttpController controller = controllerContext.Controller;
            HttpRequestMessage request = controllerContext.Request;
        
            var data = controllerContext.RouteData.Route.Defaults;
            var routeTemplate = DictionaryUtils.getByValueByKey(data, "route") as string;
            var action = DictionaryUtils.getByValueByKey(data, "action") as string;
            bool creds = (bool)DictionaryUtils.getByValueByKey(data, "credentials");

            HttpResponseMessage t = await executeFilters(controllerContext, data);
            if (t != null)
            {
                return t;
            } 
            else
            {
                IDictionary<string, object> uriObjs = handleUriVariables(routeTemplate, request, data);
                var methodParams = getParamsFromMethod(controllerContext, action, uriObjs);
                object returnValue = controller.GetType().GetMethod(action).Invoke(controller, methodParams);

                HttpResponseMessage response = handleReturnValue(controllerContext, returnValue);

                return response;
            }
        }

        private object[] getParamsFromMethod(HttpControllerContext context, string action, 
            IDictionary<string, object> uriObjs)
        {
            MethodInfo methodInfo = context.Controller.GetType().GetMethod(action);
            ParameterInfo[] parameters = methodInfo.GetParameters();

            if (parameters.Length == 0)
            {
                return null;
            }

            object[] methodParams = new object[parameters.Length];

            int counter = 0;
            foreach (var p in parameters)
            {
                if (p.ParameterType.Equals(typeof(HttpRequestMessage)))
                {
                    methodParams[counter] = context.Request;
                }
                else if (uriObjs.Count > 0 && 
                    DictionaryUtils.getByValueByKey(uriObjs, p.Name) != null)
                {
                    methodParams[counter] = DictionaryUtils.getByValueByKey(uriObjs, p.Name);
                }
                else
                {
                    methodParams[counter] = deserializeContentBody(
                        context.Request.Content, p.ParameterType);
                }
                counter++;
            }
            return methodParams;
        }

        private IDictionary<string, object> handleUriVariables(string routeTemplate, HttpRequestMessage request, 
            IDictionary<string, object> data)
        {
            IDictionary<string, object> uriObjs = new Dictionary<string, object>();
            if (routeTemplate.Contains("{"))
            {
                string[] requestSplit = request.RequestUri.Segments;
                string[] baseSplit = Regex.Split(routeTemplate, @"\/");
                int counter = 0;
                foreach (var uri in baseSplit)
                {

                    if (uri.Contains("{"))
                    {
                        var temp = Regex.Split(uri, @"\{(.*?)\}")[1];
                        string name = temp.Split(':')[0];
                        Type type = DictionaryUtils.getByValueByKey(data, name) as Type;
                        object value = TypeUtil.TryParse(type, requestSplit[counter + 1]);

                        uriObjs.Add(name, value);
                    }
                    counter++;
                }
            }
            return uriObjs;
        }

        private object deserializeContentBody(HttpContent content, Type ParamType)
        {
            object converted;
            var item = content.ReadAsStringAsync().Result as string;
            converted = JsonConvert.DeserializeObject(item, ParamType);
            
            return converted;
        }
        
        protected CookieHeaderValue generateCookie(UserDetails details)
        {
            var ck = new NameValueCollection();
            ck["sid"] = HashingUtil.hashString(details._userName);
            ck["token"] = details._securityKey;

            var cookie = new CookieHeaderValue("fulcrum", ck);
            cookie.Expires = DateTimeOffset.Now.AddMinutes(30);
            cookie.Domain = "jordanalphonso.net";
            cookie.HttpOnly = false;
            cookie.Secure = false;

            return cookie;
        }

        protected HttpResponseMessage handleReturnValue(HttpControllerContext controllerContext, object returnValue)
        {
            if (typeof(HttpResponseMessage).Equals(returnValue.GetType()))
            {
                return (HttpResponseMessage) returnValue;
            }

            return controllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue, "application/json");
        }

        private async Task<HttpResponseMessage> executeFilters(HttpControllerContext context, IDictionary<string, object> defaults)
        {
            HttpFilterCollection unsortedFilters = context.Configuration.Filters;
            IList<IFulcrumFilter> filters = getSortedFilterList(unsortedFilters);

            foreach (var filter in filters)
            {
                HttpResponseMessage filterTask =  await filter.ExecuteAsync(context, defaults);
                if (filterTask != null)
                {
                    return filterTask;
                }
            }
            return null;
        }

        private IList<IFulcrumFilter> getSortedFilterList(HttpFilterCollection filters)
        {
            IList<IFulcrumFilter> list = new List<IFulcrumFilter>();
            IEnumerator<FilterInfo> en = filters.GetEnumerator();
            
            while (en.MoveNext())
            {
                FilterInfo i = en.Current;
                IFulcrumFilter fi = (IFulcrumFilter) i.Instance;
                list.Add(fi);
            }

            return list.OrderBy(f => f.Order).ToList();
        }

        protected string getQueryParam(HttpRequestMessage request, string paramName)
        {
            IEnumerable<KeyValuePair<string, string>> query = request.GetQueryNameValuePairs();
            foreach (var param in query)
            {
                if (param.Key.Equals(paramName))
                {
                    return param.Value;
                }
            }
            return null;
        }
    }
}