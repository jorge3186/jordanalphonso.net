using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.NHibernate.CustomTypes
{
    public interface ICustomType
    {
        ICustomType getType(string codeOrLabel);

        object getCode();
    }
}
