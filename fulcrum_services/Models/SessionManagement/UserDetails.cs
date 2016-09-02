using fulcrum_services.NHibernate.CustomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fulcrum_services.Models.SessionManagement
{
    public class UserDetails
    {
        public string _userName { get; set; }

        public string _firstName { get; set; }

        public string _lastName { get; set; }

        public string _email { get; set; }

        public string _phoneNumber { get; set; }

        public string _securityKey { get; set; }

        public Company _company { get; set; }

        public IList<string> _roles { get; set; }

        public UserDetails() { }

        public UserDetails(string userName,
                           string firstName,
                           string lastName,
                           string email,
                           string phoneNumber,
                           string securityKey,
                           Company company,
                           IList<string> roles)
        {
            _userName = userName;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phoneNumber = phoneNumber;
            _securityKey = securityKey;
            _roles = roles;
            _company = company;
        }
    }
}