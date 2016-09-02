using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Models.MessageBoard
{
    public class Message : BaseModel
    {
        public virtual string message { get; set; }

        public virtual long creatorId { get; set; }

        public virtual string creatorName { get; set; }

        public virtual DateTime createdTime { get; set; }

        public virtual long topicId { get; set; }

        public virtual IList<Comment> comments { get; set; }
    }
}
