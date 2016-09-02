using fulcrum_services.Models.FulcrumUser;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace fulcrum_services.IdentityOwin
{
    public class FulcrumTokenProvider : IUserTokenProvider<FulcrumUser, long>
    {
        public Task<string> GenerateAsync(string purpose, UserManager<FulcrumUser, long> manager, FulcrumUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<FulcrumUser, long> manager, FulcrumUser user)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(string token, UserManager<FulcrumUser, long> manager, FulcrumUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<FulcrumUser, long> manager, FulcrumUser user)
        {
            throw new NotImplementedException();
        }
    }
}
