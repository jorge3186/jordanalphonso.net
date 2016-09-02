using fulcrum_common.Utils;
using Microsoft.AspNet.Identity;

namespace fulcrum_services.IdentityOwin
{
    public class BCryptPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return HashingUtil.hashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (HashingUtil.matches(providedPassword, hashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
