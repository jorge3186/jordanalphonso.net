using fulcrum_services.Models.FulcrumUser;
using Microsoft.AspNet.Identity;

namespace fulcrum_services.IdentityOwin
{
    public class FulcrumUserManager : UserManager<FulcrumUser, long>
    {
        public FulcrumUserManager(FulcrumUserStore store) : base(store)
        {
            configurePasswordValidator();
            configureUserValidator();
            configureSMSandEmailServices();
        }

        private void configurePasswordValidator()
        {
            PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequireUppercase = true
            };
        }

        private void configureUserValidator()
        {
            UserValidator = new UserValidator<FulcrumUser, long>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
        }

        private void configurePasswordHasher()
        {
            PasswordHasher = new BCryptPasswordHasher();
        }

        private void configureSMSandEmailServices()
        {
            //TODO: create sms and email services
        }

        private void configureTokenProvider()
        {
            UserTokenProvider = new FulcrumTokenProvider();
        }
    }
}
