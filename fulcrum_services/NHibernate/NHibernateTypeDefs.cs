using fulcrum_services.NHibernate.HibernateTypes;
using NHibernate.Cfg;

namespace fulcrum_services.NHibernate
{
    public class NHibernateTypeDefs
    {

        /**
         * 
         * Register typedefs here. 
         * 
         **/
        public static void registerTypeDefs(Configuration config)
        {
            config.TypeDefinition<CompanyCustomType>(c => { c.Alias = "CompanyCustomType"; });
            config.TypeDefinition<RoleCustomType>(c => { c.Alias = "RoleCustomType"; });
        }

    }
}
