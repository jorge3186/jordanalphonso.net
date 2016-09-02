using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_common.Utils
{
    public class ConfigurationUtil
    {
        private static readonly string CONNECTION_STRING = "connection.connection_string";
        private static readonly string CONNECTION_PROVIDER = "connection.provider";
        private static readonly string CONNECTION_DRIVER_CLASS = "connection.driver_class";
        private static readonly string DIALECT = "dialect";
        private static readonly string SHOW_SQL = "show_sql";

        public static IDictionary<string, string> getNHibernaterConfig()
        {
            IDictionary<string, string> configSettings = new Dictionary<string, string>();

            configSettings.Add(CONNECTION_PROVIDER, 
                ConfigurationManager.AppSettings[CONNECTION_PROVIDER]);
            configSettings.Add(CONNECTION_DRIVER_CLASS,
                ConfigurationManager.AppSettings[CONNECTION_DRIVER_CLASS]);
            configSettings.Add(DIALECT,
                ConfigurationManager.AppSettings[DIALECT]);
            configSettings.Add(SHOW_SQL,
                ConfigurationManager.AppSettings[SHOW_SQL]);
            configSettings.Add(CONNECTION_STRING,
                ConfigurationManager.ConnectionStrings[CONNECTION_STRING]
                .ConnectionString);

            return configSettings;
        }
    }
}
