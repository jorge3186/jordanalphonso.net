using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fulcrum_api.FormObjects.Users
{
    public class PasswordChangeFO
    {
        public string existingPW { get; set; }

        public string newPW { get; set; }
    }
}