using System;
using System.Collections.Generic;

namespace fulcrum_services.Models.SessionManagement
{
    public class LoggedUser
    {
        private static UserDetails _userDetails;

        public static DateTime updatedTime { get; set; }

        public static string userName()
        {
            return _userDetails._userName;
        }

        public static string firstName()
        {
            return _userDetails._firstName;
        }

        public static string lastName()
        {
            return _userDetails._lastName;
        }

        public static string email()
        {
            return _userDetails._email;
        }

        public static string phoneNumber()
        {
            return _userDetails._phoneNumber;
        }

        public static string securityKey()
        {
            return _userDetails._securityKey;
        }

        public static IList<string> roles()
        {
            return _userDetails._roles;
        }

        public static string getCompany()
        {
            if (_userDetails._company == null)
            {
                return null;
            }
            else
            {
                return _userDetails._company.getCode() as string;
            }
        }

        public static void setUserDetails(UserDetails details)
        {
            _userDetails = details;
        }

        public static UserDetails getDetails()
        {
            return _userDetails;
        }
    }
}
