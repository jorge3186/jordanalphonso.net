using fulcrum_api.Attributes.Controller;
using fulcrum_api.Constants;
using fulcrum_api.Controllers.FulcrumBase;
using fulcrum_api.FormObjects.Users;
using fulcrum_common.Utils;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.Models.SessionManagement;
using fulcrum_services.NHibernate;
using fulcrum_services.NHibernate.CustomTypes;
using fulcrum_services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace fulcrum_api.Controllers.Users
{
    [FulcrumRoutePrefix("/users")]
    public class UserController : FulcrumBaseController
    {
        private readonly IGenericService _genericService;

        public UserController(IGenericService genericService)
        {
            _genericService = genericService;
        }

        [FulcrumRoute("/{id:long}", true, F.GET)]
        public UserFO getUser(long id)
        {
            FulcrumUser user = _genericService.loadById<FulcrumUser>(id);
            FulcrumUserDetail details = null;
            IList<FulcrumUserRole> roles = null;
            if (user != null)
            {
                details = _genericService.loadByProperty<FulcrumUserDetail>("userId", id);
                roles = _genericService.loadListByProperty<FulcrumUserRole>("userId", id);
            }

            return new UserFO(user, details, roles);
        }

        [FulcrumRoute("", true, F.GET)]
        public IList<FulcrumUser> getAllUsers()
        {
            return _genericService.fetchAll<FulcrumUser>();
        }

        [FulcrumRoute("/create", true, F.POST)]
        [RestrictRoute(F.OWNER)]
        public HttpResponseMessage createNewUser(UserFO fo, HttpRequestMessage request)
        {
            if (fo.user != null)
            {
                fo.user.password = HashingUtil.hashPassword(fo.user.password);
                fo.user.UserName = fo.user.firstName.ToLower() + "." + fo.user.lastName.ToLower();

                FulcrumUser exists = _genericService.loadByProperty<FulcrumUser>(
                    "UserName", fo.user.UserName);
                if (exists != null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.Conflict, "User already Exists.");
                }
                else
                {
                    QueryResult<FulcrumUser> result = _genericService.saveOrUpdate(fo.user);

                    long id = 0;
                    foreach (var user in result._records)
                    {
                        id = user.id;
                        break;
                    }
                    if (fo.details != null)
                    {
                        fo.details.userId = id;
                        _genericService.saveOrUpdate(fo.details);
                    }
                    if (fo.roles != null)
                    {
                        foreach (var role in fo.roles)
                        {
                            role.userId = id;
                        }
                        _genericService.saveOrUpdate<FulcrumUserRole>(fo.roles);
                    }
                }
            }
            return request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [FulcrumRoute("/update/{id:long}", true, F.POST)]
        public HttpResponseMessage updateUserInfo(UserFO fo, HttpRequestMessage request, long id)
        {
            FulcrumUser user = _genericService.loadById<FulcrumUser>(id);
            FulcrumUserDetail details = _genericService.loadByProperty<FulcrumUserDetail>("userId", user.id);

            updateProperties(user, fo.user);
            updateProperties(details, fo.details);
            _genericService.saveOrUpdate(fo.user);
            _genericService.saveOrUpdate(fo.details);

            if (LoggedUser.roles().Contains(F.OWNER))
            {
                IList<FulcrumUserRole> roles = _genericService.loadListByProperty<FulcrumUserRole>(
                    "userId", user.id);

                IList<FulcrumUserRole> deleteList = updateProperties(roles, fo.roles, user);
                _genericService.saveOrUpdate<FulcrumUserRole>(fo.roles);
                _genericService.delete<FulcrumUserRole>(deleteList);
            }
            return request.CreateResponse(HttpStatusCode.OK, fo);
        }

        [FulcrumRoute("/delete/{id:long}", true, F.POST)]
        [RestrictRoute(F.OWNER)]
        public HttpResponseMessage deleteUser(HttpRequestMessage request, long id)
        {
            if (id.Equals(default(long)))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Id Specified");
            }

            FulcrumUser userToDelete = _genericService.loadById<FulcrumUser>(id);
            if (userToDelete != null)
            {
                _genericService.delete(userToDelete);
            }
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [FulcrumRoute("/pwChange/{id:long}", true, F.POST)]
        public HttpResponseMessage updatePassword(HttpRequestMessage request, PasswordChangeFO fo, long id)
        {
            FulcrumUser user = _genericService.loadById<FulcrumUser>(id);
            if (user != null && HashingUtil.matches(fo.existingPW, user.password))
            {
                string newPW = HashingUtil.hashPassword(fo.newPW);
                user.password = newPW;
                _genericService.saveOrUpdate(user);
                return request.CreateResponse(HttpStatusCode.OK, "Successfully Changed Password.");
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Id Provided");
        }

        private void updateProperties<T>(T dbValue, T foValue)
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var f in fields)
            {
                object updatedValue = updateProperty(f.Name, f.GetValue(dbValue), f.GetValue(foValue));
                if (!f.Name.Equals("Id"))
                f.SetValue(foValue, updatedValue);
            }
        }

        private IList<FulcrumUserRole> updateProperties(IList<FulcrumUserRole> dbValues, IList<FulcrumUserRole> foValues, FulcrumUser user)
        {
            IList<FulcrumUserRole> deleteList = new List<FulcrumUserRole>();
            if (dbValues.Count == 0 && foValues.Count > 0)
            {
                foreach (var r in foValues)
                {
                    r.userId = user.id;
                }
            }
            else if (dbValues.Count > 0 && foValues.Count == 0)
            {
                foreach (var r in dbValues)
                {
                    deleteList.Add(r);
                }
            }
            else if (dbValues.Count > 0 && foValues.Count > 0)
            {
                foreach (var r in foValues)
                {
                    r.userId = user.id;
                    foreach(var dbR in dbValues)
                    {
                        if (!dbR.role.getCode().Equals(r.role.getCode()))
                        {
                            deleteList.Add(dbR);
                        }
                    }
                }
            }
            return deleteList;
        }

        private object updateProperty(string field, object dbValue, object foValue)
        {
            object returnValue = null;
            if (!F.exlcudedProperties.Contains(field))
            {
                if (foValue == null && dbValue == null)
                {
                    return null;
                }
                if (foValue == null && dbValue != null)
                {
                    returnValue = dbValue;
                }
                else if (foValue != null && dbValue == null)
                {
                    returnValue = foValue;
                }
                else if (!dbValue.Equals(foValue))
                {
                    returnValue = foValue;
                }
                else
                {
                    returnValue = dbValue;
                }
            }
            else
            {
                returnValue = dbValue;
            }
            return returnValue;
        }
    }
}