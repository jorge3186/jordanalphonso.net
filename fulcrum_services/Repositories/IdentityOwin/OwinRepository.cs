using fulcrum_services.Models;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.NHibernate;
using System.Collections.Generic;
using fulcrum_services.IdentityOwin;
using System;
using fulcrum_common.Utils;
using fulcrum_services.Models.SessionManagement;
using fulcrum_api.Security;
using fulcrum_services.NHibernate.CustomTypes;
using System.Collections.ObjectModel;

namespace fulcrum_services.Repositories.IdentityOwin
{
    public class OwinRepository : NHibernateRepoWrapper, IOwinRepository
    {
        public void delete<T>(T obj) where T : BaseModel
        {
            deleteEntity(obj);
        }

        public void deleteUser(long id)
        {
            FulcrumUser user = fetch<FulcrumUser>(id);
            if (user != null)
            {
                deleteEntity(user);
            }
        }

        public T fetchById<T>(long id) where T : BaseModel
        {
            return fetch<T>(id);
        }

        public T fetchByProperty<T>(string propertyName, object value) where T : BaseModel
        {
            return fetch<T>(propertyName, value);
        }

        public IList<T> fetchByUserId<T>(long id) where T : BaseModel
        {
            return fetchList<T>("userId", id);
        }

        public T findById<T>(long id) where T : BaseModel
        {
            return fetch<T>(id);
        }

        public void saveOrUpdate<T>(T obj) where T : BaseModel
        {
            merge(obj);
        }

        public LoginResponse validateLogin(string username, string password)
        {
            FulcrumUser user;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new LoginResponse(false, "Cannot have empty username or password.");
            }

            if (username.Contains("@"))
            {
                user = fetchByProperty<FulcrumUser>("email", username);
            }
            else
            {
                user = fetchByProperty<FulcrumUser>("UserName", username);
            }

            if (user == null)
            {
                return new LoginResponse(false, "Invalid Username.");
            }
            else if (user.accessFailedCount >= 3)
            {
                return new LoginResponse(false, "Account Locked due to too many unsuccessful attempts.");
            }
            else
            {
                if (HashingUtil.matches(password, user.password))
                {
                    FulcrumUserDetail userDetail = fetchByProperty<FulcrumUserDetail>("userId", user.id);
                    IList<FulcrumUserRole> userRoles = fetchList<FulcrumUserRole>("userId", user.id);
                    IList<string> simpleRoles = new List<string>();

                    foreach (var role in userRoles)
                    {
                        simpleRoles.Add(role.role.getCode().ToString());
                    }

                    UserDetails details = new UserDetails(
                        user.UserName,
                        user.firstName,
                        user.lastName,
                        user.email,
                        userDetail.phoneNumber,
                        TokenGenerator.generateToken(user, userDetail),
                        userDetail.company,
                        simpleRoles);
                    return new LoginResponse(true, "Success", details, user);
                }
                else
                {
                    user.accessFailedCount++;
                    saveOrUpdate(user);

                    return new LoginResponse(false, "Invalid credentials.");
                }
            }
        }

    }
}
