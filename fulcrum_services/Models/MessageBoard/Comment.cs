using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Models.MessageBoard
{
    public class Comment : BaseModel
    {
        public virtual string comment { get; set; }

        public virtual long creatorId { get; set; }

        public virtual string creatorName { get; set; }

        public virtual DateTime createdTime { get; set; }

        public virtual long messageId { get; set; }
    }
}
