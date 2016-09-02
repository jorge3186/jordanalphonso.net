using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.Models.SessionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.IdentityOwin
{
    public class LoginResponse
    {
        public bool valid { get; set; }

        public string message { get; set; }

        public FulcrumUser user { get; set; }

        public UserDetails details { get; set; }

        public LoginResponse(bool valid, string message)
        {
            this.valid = valid;
            this.message = message;
        }

        public LoginResponse(bool valid, string message, UserDetails details, FulcrumUser user)
        {
            this.valid = valid;
            this.message = message;
            this.details = details;
            this.user = user;
        }
    }
}
