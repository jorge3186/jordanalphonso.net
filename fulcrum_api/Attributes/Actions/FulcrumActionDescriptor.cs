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
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace fulcrum_api.Attributes.Actions
{
    public class FulcrumActionDescriptor : HttpActionDescriptor
    {
        public string _action { get; set; }
        public Type _returnType { get; set; }
        public Type _controllerType { get; set; }

        public FulcrumActionDescriptor(string action, Type returnType, Type controller)
        {
            _action = action;
            _returnType = returnType;
            _controllerType = controller;
        }

        public override string ActionName
        {
            get
            {
                return _action;
            }
        }

        public override Type ReturnType
        {
            get
            {
                return _returnType;
            }
        }

        public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
        {
            IHttpController controller = controllerContext.Controller;
            MethodInfo method = controller.GetType().GetMethod(_action);

            if (method != null)
            {
                var result = method.Invoke(controller, arguments.Values.ToArray());
                return Task.FromResult(result);
            }
            return Task.FromResult<object>(null);
        }

        public override Collection<HttpParameterDescriptor> GetParameters()
        {
            throw new NotImplementedException();
        }
    }
}