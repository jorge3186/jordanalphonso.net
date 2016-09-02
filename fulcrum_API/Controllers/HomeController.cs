using fulcrum_api.Attributes.Controller;
using fulcrum_api.Constants;
using fulcrum_api.Controllers.FulcrumBase;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace fulcrum_api.Controllers
{
    public class HomeController : FulcrumBaseController
    {
        private IDictionary<string, string> welcome =  new Dictionary<string, string>();

        [FulcrumRoute(Route: "/", Credentials: false, HttpMethods: F.GET)]
        public IDictionary<string, string> HomeScreen()
        {
            welcome.Add("Message", "Welcome to the Fulcrum Homepage!");
            welcome.Add("Copyright", "jordanalphonso.net - "+DateTime.Now.Year.ToString());
            return welcome;
        }
    }
}
