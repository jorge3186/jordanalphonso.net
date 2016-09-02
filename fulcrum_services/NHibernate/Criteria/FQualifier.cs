using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.NHibernate.Criteria
{
    public class FQualifier
    {
        public static readonly FQualifier EQ = new FQualifier("=", " = ");

        public static readonly FQualifier GT = new FQualifier(">", " > ");

        public static readonly FQualifier LT = new FQualifier("<", " < ");

        public static readonly FQualifier GE = new FQualifier(">=", " >= ");

        public static readonly FQualifier LE = new FQualifier("<=", " <= ");

        public static readonly FQualifier NULL = new FQualifier("NULL", " IS NULL ");

        public static readonly FQualifier NOT_NULL = new FQualifier("NOT NULL", " IS NOT NULL ");

        public static readonly FQualifier IN = new FQualifier("IN", " IN ");

        public string code { get; private set; }

        public string label { get; private set; }

        public FQualifier(string code, string label)
        {
            this.code = code;
            this.label = label;
        }
    }
}
