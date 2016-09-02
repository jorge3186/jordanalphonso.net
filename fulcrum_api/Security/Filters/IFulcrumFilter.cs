using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace fulcrum_api.Security.Filters
{
    public interface IFulcrumFilter : IFilter
    {
        Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext context, IDictionary<string, object> defaults);

        int Order { get; }
    }
}
