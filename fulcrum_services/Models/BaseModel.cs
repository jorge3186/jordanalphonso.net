using System;

namespace fulcrum_services.Models
{
    public class BaseModel
    {
        public virtual long id { get; set; }

        public virtual DateTime updatedTime { get; set; }

        public virtual string updatedUser { get; set; }

        public virtual int version { get; set; }

        public virtual bool modified { get; set; }
        
    }
}
