using fulcrum_api.Attributes.Controller;
using fulcrum_api.Constants;
using fulcrum_api.Controllers.FulcrumBase;
using fulcrum_api.FormObjects.Login;
using fulcrum_common.Utils;
using fulcrum_services.IdentityOwin;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.Models.SessionManagement;
using fulcrum_services.NHibernate.CustomTypes;
using fulcrum_services.Services.IdentityOwin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace fulcrum_api.Controllers.Login
{
    public class LoginController : FulcrumBaseController
    {
        private readonly IOwinService _owinService;

        public LoginController(IOwinService owinService)
        {
            _owinService = owinService;
        }

        [FulcrumRoute("/login", false, F.POST)]
        public HttpResponseMessage login(LoginFO fo, HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(fo.password) || string.IsNullOrEmpty(fo.userName))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty Username or Credentials");
            }
            else
            {
                LoginResponse response = _owinService.validateUser(fo.userName, fo.password);
                return handleLoginResponse(request, response);
            }   
        }

        [FulcrumRoute("/logout", true, F.GET)]
        public HttpResponseMessage logout(HttpRequestMessage request)
        {
            LoggedUser.setUserDetails(null);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [FulcrumRoute("resetLockout/{id:long}", true, F.GET, "Successfully logged out.")]
        [RestrictRoute(F.OWNER)]
        public HttpResponseMessage resetLockout(HttpRequestMessage request, long id)
        {
            FulcrumUser lockedUser = _owinService.findById<FulcrumUser>(id);
            lockedUser.accessFailedCount = 0;
            _owinService.saveOrUpdate(lockedUser);

            return request.CreateResponse(HttpStatusCode.OK, "Account has been reset.");
        }

        private HttpResponseMessage handleLoginResponse(HttpRequestMessage request, LoginResponse response)
        {
            if (response.valid)
            {
                IDictionary<string, object> rolesCompany = stripRolesFromDetails(response.user, response.details);
                HttpResponseMessage successMessage = request.CreateResponse(HttpStatusCode.OK, rolesCompany);
                successMessage.ReasonPhrase = response.message;
                CookieHeaderValue cookie = generateCookie(response.details);
                successMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return successMessage;
            }
            else
            {
                LoggedUser.setUserDetails(null);
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, response.message);
            }
        }

        private IDictionary<string, object> stripRolesFromDetails(FulcrumUser user, UserDetails details)
        {
            IDictionary<string, object> results = new Dictionary<string, object>();
            results.Add("user", LoggedUser.userName());
            results.Add("id", user.id);
            results.Add("roles", details._roles);
            if (details._company != null)
            {
                results.Add("company", details._company.getCode());
            }
            return results;
        }
    }
}