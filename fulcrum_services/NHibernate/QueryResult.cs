using fulcrum_services.Models;
using System.Collections.Generic;

namespace fulcrum_services.NHibernate
{
    public class QueryResult<T> where T : BaseModel
    {
        public int _updated { get; set; }
        public int _inserted { get; set; }
        public int _deleted { get; set; }
        public ICollection<T> _records { get; set; }

        public QueryResult(int updated, int inserted, int deleted, ICollection<T> records)
        {
            _updated = updated;
            _inserted = inserted;
            _deleted = deleted;
            _records = records;
        }
    }
}
