using fulcrum_common.Utils;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.Models.SessionManagement;

namespace fulcrum_api.Security
{
    public class TokenGenerator
    {
        private TokenGenerator() { }

        public static string generateToken(UserDetails details)
        {
            string token = buildUnHashedToken(details);

            return HashingUtil.hashString(token);
        }

        public static string generateToken(UserDetails details, bool hashed)
        {
            string token = buildUnHashedToken(details);
            
            string hashedToken = HashingUtil.hashString(token);
            if (hashed)
            {
                return hashedToken;
            }
            return token;
        }

        public static string generateToken(FulcrumUser user, FulcrumUserDetail detail)
        {
            string token = buildUnHashedToken(user, detail);

            return HashingUtil.hashString(token);
        }

        private static bool validateToken(UserDetails details, string token)
        {
            string unhashedToken = buildUnHashedToken(details);
            return HashingUtil.matches(unhashedToken, token);
        }

        public static string buildUnHashedToken(UserDetails details)
        {
            string token;
            if (details._lastName.Length > 10)
            {
                token = details._lastName.Substring(4, 5);
            }
            else
            {
                token = details._lastName.Substring(2);
            }

            token = token + details._phoneNumber.Substring(2);
            token = token + details._email.Substring(3);
            return token;
        }

        private static string buildUnHashedToken(FulcrumUser user, FulcrumUserDetail details)
        {
            string token;
            if (user.lastName.Length > 10)
            {
                token = user.lastName.Substring(4, 5);
            }
            else
            {
                token = user.lastName.Substring(2);
            }

            token = token + details.phoneNumber.Substring(2);
            token = token + user.email.Substring(3);
            return token;
        }
    }
}