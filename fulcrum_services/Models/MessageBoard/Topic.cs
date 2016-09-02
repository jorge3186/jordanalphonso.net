using fulcrum_services.NHibernate.CustomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Models.MessageBoard
{
    public class Topic : BaseModel
    {
        public virtual string creatorName { get; set; }

        public virtual long creatorId { get; set; }

        public virtual DateTime createdTime { get; set; }

        public virtual string subject { get; set; }

        public virtual Company company { get; set; }

        public virtual IList<Message> messages { get; set; }
    }
}
