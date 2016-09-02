
namespace fulcrum_common.Utils
{
    public class HashingUtil
    {
        private HashingUtil() { }

        private const int SALT_STRENGTH = 12;

        public static string hashString(string value)
        {
            return BCrypt.Net.BCrypt.HashString(value, SALT_STRENGTH);
        }

        public static string hashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(SALT_STRENGTH);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static bool matches(string value, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(value, hashed);
        }
    }
}
