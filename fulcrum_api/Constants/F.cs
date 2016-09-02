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
using System.Linq;
using System.Web;

namespace fulcrum_api.Constants
{
    public class F
    {
        //http constants
        public const string GET = "GET";

        public const string PUT = "PUT";

        public const string POST = "POST";

        public const string DELETE = "DELETE";

        //Roles
        public const string OWNER = "OWN";

        public const string DEVELOPER = "DEV";

        public const string VISITOR = "VIST";

        public const string CLIENT = "CLNT";

        public const string ARTIST = "ART";

        //Excluded Properties
        public static IList<string> exlcudedProperties = new List<string>()
        {
            "Id",
            "id",
            "userId",
            "version",
            "modifed",
            "updatedTime",
            "updatedUser",
            "password"
        };

    }
}