using System;
using fulcrum_services.NHibernate.CustomTypes;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace fulcrum_services.Models.FulcrumUser
{
    public class FulcrumUserRole : BaseModel
    {
        public virtual long userId { get; set; }

        public virtual Role role { get; set; }

       
    }
}
