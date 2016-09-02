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
using fulcrum_api.Resolvers.HttpRouteResolver;
using fulcrum_api.Security.Filters;
using System.Collections;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace fulcrum_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("https://jordanalphonso.net", "*", "POST, GET, PUT, DELETE, OPTIONS");
            cors.SupportsCredentials = true;
            config.EnableCors(cors);

            config.Filters.Add(new FulcrumAuthenticationFilter());
            config.Filters.Add(new FulcrumRouteRestrictionFilter());

            HttpRouteResolver.registerFulcrumRoutes(config);
        }
    }
}
