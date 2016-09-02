using fulcrum_services.Models.FulcrumUser;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using fulcrum_services.Services.IdentityOwin;
using fulcrum_services.NHibernate.CustomTypes;

namespace fulcrum_services.IdentityOwin
{
    public class FulcrumUserStore : IUserStore<FulcrumUser, long>,
                                    IUserPasswordStore<FulcrumUser, long>,
                                    IUserEmailStore<FulcrumUser, long>,
                                    IUserLockoutStore<FulcrumUser, long>,
                                    IUserPhoneNumberStore<FulcrumUser, long>,
                                    IUserRoleStore<FulcrumUser, long>,
                                    IUserSecurityStampStore<FulcrumUser, long>
    {
        private readonly IOwinService _owinService;

        public FulcrumUserStore(IOwinService owinService)
        {
            _owinService = owinService;
        }

        public Task AddToRoleAsync(FulcrumUser user, string roleName)
        {
            if (user != null && !string.IsNullOrEmpty(roleName))
            {
                IList<Role> roles = Role.getAllRoles();
                foreach (var role in roles)
                {
                    if (role.getCode().Equals(roleName))
                    {
                        _owinService.addRoleToUser(user, role);
                        return Task.FromResult(0);
                    }
                }
            }
            return Task.FromResult<object>(null);
        }

        public Task CreateAsync(FulcrumUser user)
        {
            if (user == null)
            {
                return Task.FromResult<FulcrumUser>(null);
            }
            _owinService.saveOrUpdate(user);
            return Task.FromResult(user);
        }

        public Task DeleteAsync(FulcrumUser user)
        {
            if (user == null)
            {
                return Task.FromResult<FulcrumUser>(null);
            }

            _owinService.delete(user);
            return Task.FromResult(user);
        }

        public void Dispose()
        {
        }

        public Task<FulcrumUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Task.FromResult<FulcrumUser>(null);
            }

            FulcrumUser user = _owinService.findByEmail(email);
            if (user != null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult<FulcrumUser>(null);
        }

        public Task<FulcrumUser> FindByIdAsync(long userId)
        {
            FulcrumUser user = _owinService.findById<FulcrumUser>(userId);
            if (user != null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult<FulcrumUser>(null);
        }

        public Task<FulcrumUser> FindByNameAsync(string userName)
        {
            FulcrumUser user = _owinService.findByName(userName);
            if (user != null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult<FulcrumUser>(null);
        }

        public Task<int> GetAccessFailedCountAsync(FulcrumUser user)
        {
            if (user != null)
            {
                return Task.FromResult(user.accessFailedCount);
            }
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(FulcrumUser user)
        {
            if (user != null && !string.IsNullOrEmpty(user.email))
            {
                return Task.FromResult(user.email);
            }
            return Task.FromResult<string>(null);
        }

        public Task<bool> GetEmailConfirmedAsync(FulcrumUser user)
        {
            if (user != null && user.emailConfirmed)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> GetLockoutEnabledAsync(FulcrumUser user)
        {
            if (user != null && user.lockoutEnabled)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(FulcrumUser user)
        {
            if (user != null && user.lockoutEndDate != null)
            {
                return Task.FromResult(
                    new DateTimeOffset(DateTime.SpecifyKind(user.lockoutEndDate, DateTimeKind.Utc)));
            }
            return Task.FromResult(new DateTimeOffset());
        }

        public Task<string> GetPasswordHashAsync(FulcrumUser user)
        {
            if (user != null && !string.IsNullOrEmpty(user.password))
            {
                return Task.FromResult(user.password);
            }
            return Task.FromResult<string>(null);
        }

        public Task<string> GetPhoneNumberAsync(FulcrumUser user)
        {
            if (user != null)
            {
                FulcrumUserDetail details = _owinService.getUserDetails(user);
                if (details != null && !string.IsNullOrEmpty(details.phoneNumber))
                {
                    return Task.FromResult(details.phoneNumber);
                }
            }
            return Task.FromResult<string>(null);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(FulcrumUser user)
        {
            if (user != null)
            {
                FulcrumUserDetail details = _owinService.getUserDetails(user);
                if (details != null)
                {
                    return Task.FromResult(details.phoneNumberConfirmed);
                }
            }
            return Task.FromResult(false);
        }

        public Task<IList<string>> GetRolesAsync(FulcrumUser user)
        {
            if (user != null)
            {
                IList<string> roles = _owinService.getRolesForUser(user);
                if (roles != null && roles.Count > 0)
                {
                    return Task.FromResult(roles);
                }
            }
            return Task.FromResult<IList<string>>(null);
        }

        public Task<string> GetSecurityStampAsync(FulcrumUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(FulcrumUser user)
        {
            if (user != null)
            {
                return Task.FromResult(string.IsNullOrEmpty(user.password));
            }
            return Task.FromResult(false);
        }

        public Task<int> IncrementAccessFailedCountAsync(FulcrumUser user)
        {
            if (user != null)
            {
                user.accessFailedCount = user.accessFailedCount + 1;
                _owinService.saveOrUpdate(user);
            }
            return Task.FromResult(0);
        }

        public Task<bool> IsInRoleAsync(FulcrumUser user, string roleName)
        {
            if (user != null)
            {
                IList<string> userRoles = _owinService.getRolesForUser(user);
                if (userRoles != null && userRoles.Count > 0)
                {
                    foreach (var role in userRoles)
                    {
                        if (role.ToLower().Equals(roleName.ToLower()))
                        {
                            return Task.FromResult(true);
                        }
                    }
                }
            }
            return Task.FromResult(false);
        }

        public Task RemoveFromRoleAsync(FulcrumUser user, string roleName)
        {
            if (user != null)
            {
                _owinService.deleteRole(user, roleName);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }

        public Task ResetAccessFailedCountAsync(FulcrumUser user)
        {
            if (user != null)
            {
                user.accessFailedCount = 0;
                _owinService.saveOrUpdate(user);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }

        public Task SetEmailAsync(FulcrumUser user, string email)
        {
            if (user != null)
            {
                user.email = email;
                _owinService.saveOrUpdate(user);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(FulcrumUser user, bool confirmed)
        {
            if (user != null)
            {
                user.emailConfirmed = confirmed;
                _owinService.saveOrUpdate(user);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEnabledAsync(FulcrumUser user, bool enabled)
        {
            if (user != null)
            {
                user.lockoutEnabled = enabled;
                _owinService.saveOrUpdate(user);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEndDateAsync(FulcrumUser user, DateTimeOffset lockoutEnd)
        {
            if (user != null)
            {
                user.lockoutEndDate = lockoutEnd.UtcDateTime;
                _owinService.saveOrUpdate(user);
            }
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(FulcrumUser user, string passwordHash)
        {
            if (user != null)
            {
                user.password = passwordHash;
                _owinService.saveOrUpdate(user);
            }
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(FulcrumUser user, string phoneNumber)
        {
            if (user != null)
            {
                FulcrumUserDetail details = _owinService.getUserDetails(user);
                if (details != null)
                {
                    details.phoneNumber = phoneNumber;
                    _owinService.saveOrUpdate(details);
                }
            }
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(FulcrumUser user, bool confirmed)
        {
            if (user != null)
            {
                FulcrumUserDetail details = _owinService.getUserDetails(user);
                if (details != null)
                {
                    details.phoneNumberConfirmed = confirmed;
                    _owinService.saveOrUpdate(details);
                }
            }
            return Task.FromResult(0);
        }

        public Task SetSecurityStampAsync(FulcrumUser user, string stamp)
        {
            if (user != null)
            {
                user.secStamp = stamp;
                _owinService.saveOrUpdate(user);
            }
            return Task.FromResult(0);
        }

        public Task UpdateAsync(FulcrumUser user)
        {
            if (user != null)
            {
                _owinService.saveOrUpdate(user);
                return Task.FromResult(0);
            }
            return Task.FromResult<object>(null);
        }
    }
}
