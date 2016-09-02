using fulcrum_common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.NHibernate.Criteria
{
    public class FRestrictions
    {
        public IDictionary<string, IList<FRestriction>> restrictions { get; private set; }

        public  FRestriction append(string property, object value)
        {
            FRestriction rest = new FRestriction(value);
            IList<FRestriction> existingList = listExists(fulcrumRestrictions(), property);
            if (existingList != null)
            {
                existingList.Add(rest);
            }
            else
            {
                existingList = new List<FRestriction>();
                existingList.Add(rest);
            }
            fulcrumRestrictions().Add(property, existingList);

            return rest;
        }

        public FRestriction append(string property, object value, FQualifier qualifier)
        {
            FRestriction rest = new FRestriction(qualifier, value);
            IList<FRestriction> existingList = listExists(fulcrumRestrictions(), property);
            if (existingList != null)
            {
                existingList.Add(rest);
            }
            else
            {
                existingList = new List<FRestriction>();
                existingList.Add(rest);
            }
            fulcrumRestrictions().Add(property, existingList);

            return rest;
        }

        public void remove(string property)
        {
            fulcrumRestrictions().Remove(property);
        }

        public void reset()
        {
            fulcrumRestrictions().Clear();
        }

        private IDictionary<string, IList<FRestriction>> fulcrumRestrictions()
        {
            if (restrictions == null)
            {
                return new Dictionary<string, IList<FRestriction>>();
            }
            return restrictions;
        }

        private IList<FRestriction> listExists(IDictionary<string, IList<FRestriction>> restrictions, string property)
        {
            var result = DictionaryUtils.getByValueByKey(restrictions, property);
            if (result != null)
            {
                return result as IList<FRestriction>;
            }
            return null;
        }

        public  class FRestriction
        {
            public FQualifier qualifier { get; private set; }

            public object value { get; private set; }

            public FRestriction(FQualifier qualifier, object value)
            {
                this.qualifier = qualifier;
                this.value = value;
            }

            public FRestriction(object value)
            {
                qualifier = FQualifier.EQ;
                this.value = value;
            }
        }
    }
}
