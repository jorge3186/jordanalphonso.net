using fulcrum_api.Attributes.Controller;
using fulcrum_common.Utils;
using fulcrum_services.Models.SessionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace fulcrum_api.Security.Filters
{
    public class FulcrumRouteRestrictionFilter : IFulcrumFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public int Order
        {
            get
            {
                return 2;
            }
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext context, IDictionary<string, object> defaults)
        {
            bool creds = (bool)DictionaryUtils.getByValueByKey(defaults, "credentials");
            if (!creds)
            {
                return null;
            }

            HttpResponseMessage result = await validateRestrictions(context, defaults);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private async Task<HttpResponseMessage> validateRestrictions(HttpControllerContext context, IDictionary<string, object> defaults)
        {
            var methodName = DictionaryUtils.getByValueByKey(defaults, "action") as string;
            MethodInfo methodInfo = context.Controller.GetType().GetMethod(methodName);

            if (methodInfo != null && Attribute.IsDefined(methodInfo, typeof(RestrictRouteAttribute)))
            {
                bool valid = false;
                RestrictRouteAttribute attrib = methodInfo.GetCustomAttribute<RestrictRouteAttribute>();
                string[] allowedRoles = attrib.AllowedRoles;
                for (int i =0; i < allowedRoles.Length; i++)
                {
                    if (LoggedUser.roles() != null && 
                        LoggedUser.roles().Contains(allowedRoles[i]))
                    {
                        valid = true;
                    }
                }

                if (valid)
                {
                    return null;
                }
                else
                {
                    return context.Request.CreateErrorResponse(
                        HttpStatusCode.Unauthorized, "Unauthorized Roles");
                }

            }
            else
            {
                return null;
            }
        }
    }
};