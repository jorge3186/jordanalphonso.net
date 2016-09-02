using fulcrum_services.Models.FulcrumUser;
using System.Collections.Generic;

namespace fulcrum_api.FormObjects.Users
{
    public class UserFO
    {
        public UserFO() { }

        public UserFO(FulcrumUser user, FulcrumUserDetail detail, IList<FulcrumUserRole> roles)
        {
            this.user = user;
            details = detail;
            this.roles = roles;
        }

        public FulcrumUser user { get; set; }

        public FulcrumUserDetail details { get; set; }

        public IList<FulcrumUserRole> roles { get; set; }
    }
}