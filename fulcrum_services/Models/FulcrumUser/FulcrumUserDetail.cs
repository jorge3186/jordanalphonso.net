using fulcrum_services.NHibernate.CustomTypes;
using System.Collections.Generic;

namespace fulcrum_services.Models.FulcrumUser
{
    public class FulcrumUserDetail : BaseModel
    {
        public virtual long userId { get; set; }

        public virtual string address { get; set; }

        public virtual string state { get; set; }

        public virtual string city { get; set; }

        public virtual string alternateEmail { get; set; }

        public virtual string phoneNumber { get; set; }

        public virtual string alternatePhoneNumber { get; set; }

        public virtual bool phoneNumberConfirmed { get; set; }

        public virtual Company company { get; set; }
    }
}
