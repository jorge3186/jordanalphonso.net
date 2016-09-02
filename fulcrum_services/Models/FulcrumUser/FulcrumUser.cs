
using Microsoft.AspNet.Identity;
using System;

namespace fulcrum_services.Models.FulcrumUser
{
    public class FulcrumUser : BaseModel, IUser<long>
    {
        public virtual string firstName { get; set; }

        public virtual string lastName { get; set; }

        public virtual string UserName { get; set; }

        public virtual string password { get; set; }

        public virtual string secStamp { get; set; }

        public virtual string email { get; set; }

        public virtual bool emailConfirmed { get; set; }

        public virtual int accessFailedCount { get; set; }

        public virtual bool lockoutEnabled { get; set; }

        public virtual DateTime lockoutEndDate { get; set; }

        public virtual long Id
        {
            get
            {
                return id;
            }
        }
    }
}