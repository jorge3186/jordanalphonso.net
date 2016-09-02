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
using fulcrum_services.NHibernate.CustomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace fulcrum_api.Attributes.Controller
{
    public class RestrictRouteAttribute : ActionFilterAttribute
    {
        public string[] AllowedRoles { get; private set; }  
        
        public RestrictRouteAttribute(params string[] AllowedRoles)
        {
            this.AllowedRoles = AllowedRoles;
        }

    }
}