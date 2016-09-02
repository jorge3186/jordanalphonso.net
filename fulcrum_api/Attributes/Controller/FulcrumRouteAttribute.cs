/*********************************************************************************
 * 
 *  All contents Copyright(c) 2016 jordanalphonso.net. Unpublished-rights reserved 
 *	under the copyright laws of the United States and international conventions. 
 *	Use of a copyright notice is precautionary only and does not imply publication 
 *	or disclosure. This software contains confidential and personal information
 *  of jordanalphonso.net and it's clients. Use, disclosure, or reproduction is 
 *   prohibited without the prior written consent of LumaFX.net
 *  
 *  ------------------------------------------------------------------------------
 *  History:
 *  ------------------------------------------------------------------------------
 *  2016 - Jordan Alphonso - Creation  
 *   
 ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;

namespace fulcrum_api.Attributes.Controller
{
    public class FulcrumRouteAttribute : ActionFilterAttribute
    {
        public string Route { get; private set; }

        public string[] HttpMethods { get; private set; }

        public bool Credentials { get; private set; }

        public bool Validation { get; private set; }

        public FulcrumRouteAttribute(string Route,
                                     bool Credentials,
                                     bool Validation,
                                     params string[] HttpMethods)
        {
            this.Route = Route;
            this.HttpMethods = HttpMethods;
            this.Credentials = Credentials;
            this.Validation = Validation;
        }

        public FulcrumRouteAttribute(string Route,
                                     bool Credentials,
                                     params string[] HttpMethods)
        {
            this.Route = Route;
            this.HttpMethods = HttpMethods;
            this.Credentials = Credentials;
        }

        public FulcrumRouteAttribute(string Route,
                                     params string[] HttpMethods)
        {
            this.Route = Route;
            this.HttpMethods = HttpMethods;
        }
    }
}