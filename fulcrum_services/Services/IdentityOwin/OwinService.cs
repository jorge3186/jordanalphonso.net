using System.Collections.Generic;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.NHibernate.CustomTypes;
using fulcrum_services.Repositories.IdentityOwin;
using fulcrum_services.Models;
using fulcrum_services.IdentityOwin;
using System;
using fulcrum_services.Models.SessionManagement;

namespace fulcrum_services.Services.IdentityOwin
{
    public class OwinService : IOwinService
    {
        private readonly IOwinRepository _owinRepo;

        public OwinService(IOwinRepository owinRepo)
        {
            _owinRepo = owinRepo;
        }

        public void addRoleToUser(FulcrumUser user, Role role)
        {
            FulcrumUserRole newRole = new FulcrumUserRole() { role = role, userId = user.id };
            _owinRepo.saveOrUpdate(newRole);
        }

        public void delete<T>(T user) where T : BaseModel
        {
            _owinRepo.delete(user);
        }

        public void deleteRole(FulcrumUser user, string role)
        {
            IList<FulcrumUserRole> userRoles = _owinRepo.fetchByUserId<FulcrumUserRole>(user.id);
            if (userRoles != null && userRoles.Count > 0)
            {
                foreach (var r in userRoles)
                {
                    if (r.role.getCode().Equals(role))
                    {
                        _owinRepo.delete(r);
                    }
                }
            }
        }

        public void deleteUser(long id)
        {
            _owinRepo.deleteUser(id);
        }

        public FulcrumUser findByEmail(string email)
        {
            return _owinRepo.fetchByProperty<FulcrumUser>("email", email);
        }

        public T findById<T>(long id) where T : BaseModel
        {
            return _owinRepo.findById<T>(id);
        }

        public FulcrumUser findByName(string userName)
        {
            return _owinRepo.fetchByProperty<FulcrumUser>("UserName", userName);
        }

        public IList<string> getRolesForUser(FulcrumUser user)
        {
            IList<FulcrumUserRole> userRoles = _owinRepo.fetchByUserId<FulcrumUserRole>(user.id);
            if (userRoles != null && userRoles.Count > 0)
            {
                IList<string> roles = new List<string>();
                foreach (var r in userRoles)
                {
                    roles.Add(r.role.getCode().ToString());
                }
                return roles;
            }
            return null;
        }

        public FulcrumUserDetail getUserDetails(FulcrumUser user)
        {
            return _owinRepo.fetchByProperty<FulcrumUserDetail>("userId", user.id);
        }

        public void saveOrUpdate<T>(T user) where T : BaseModel
        {
            _owinRepo.saveOrUpdate(user);
        }

        public LoginResponse validateUser(string username, string password)
        {
            LoginResponse response = _owinRepo.validateLogin(username, password);
            LoggedUser.setUserDetails(response.details);
            return response;
        }
    }
}
