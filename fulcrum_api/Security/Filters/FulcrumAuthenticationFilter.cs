using fulcrum_common.Utils;
using fulcrum_services.Models.SessionManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace fulcrum_api.Security.Filters
{
    public class FulcrumAuthenticationFilter : IFulcrumFilter
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
                return 1;
            }
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext context, IDictionary<string, object> defaults)
        {
            bool creds = (bool) DictionaryUtils.getByValueByKey(defaults, "credentials");
            if (!creds)
            {
                return null;
            }
            else
            {
                Collection<CookieHeaderValue> cookies = context.Request.Headers.GetCookies("fulcrum");
                if (cookies == null || cookies.Count == 0 || LoggedUser.getDetails() == null)
                {
                    return context.Request.CreateErrorResponse(
                        HttpStatusCode.Unauthorized, "No Login");
                }

                else
                {
                    foreach (var cookie in cookies)
                    {
                        DateTime now = DateTime.Now;
                        var ck = cookie["fulcrum"];
                        string loggedUser = LoggedUser.userName();
                        string secCode = TokenGenerator.generateToken(LoggedUser.getDetails(), false);

                        if (!HashingUtil.matches(LoggedUser.userName(), ck["sid"]))
                        {
                            return context.Request.CreateErrorResponse(
                                HttpStatusCode.Unauthorized, "Invalid User");
                        }
                        else if (!HashingUtil.matches(secCode, ck["token"]))
                        {
                            LoggedUser.setUserDetails(null);
                            return context.Request.CreateErrorResponse(
                                HttpStatusCode.Unauthorized, "Invalid Credentials");
                        }
                        else if (LoggedUser.updatedTime != DateTime.MinValue 
                            && now.Subtract(LoggedUser.updatedTime) > TimeSpan.FromMinutes(10))
                        {
                            LoggedUser.setUserDetails(null);
                            return context.Request.CreateErrorResponse(
                                HttpStatusCode.Unauthorized, "Session Timeout");
                        }
                        else if (cookie.Expires <= now)
                        {
                            LoggedUser.setUserDetails(null);
                            return context.Request.CreateErrorResponse(
                                HttpStatusCode.Unauthorized, "Session Has Expired");
                        }
                        else
                        {
                            LoggedUser.updatedTime = now;
                            return null;
                        }
                    }
                    return null;
                }
            }
        }
    }
}