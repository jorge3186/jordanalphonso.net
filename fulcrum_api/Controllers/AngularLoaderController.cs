using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace fulcrum_api.Controllers
{
    public class AngularLoaderController : IHttpController
    {
        public Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var request = controllerContext.Request;
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;


            var index = File.ReadAllText(assemblyPath+"/angular_app/index.html", Encoding.UTF8);
           
            var result = request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(index);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return Task.FromResult(result);
        }
    }
}