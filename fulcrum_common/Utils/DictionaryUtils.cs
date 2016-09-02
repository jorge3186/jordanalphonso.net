using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_common.Utils
{
    public class DictionaryUtils
    {
        private DictionaryUtils() { }

        public static object getByValueByKey<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                if (pair.Key.Equals(key))
                {
                    return pair.Value;
                }
            }
            return null;
        }
    }
}
