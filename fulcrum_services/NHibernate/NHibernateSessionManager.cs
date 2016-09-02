using fulcrum_common.Utils;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace fulcrum_services.NHibernate
{
    public class NHibernateSessionManager
    {
        private static NHibernateSessionManager _instance;
        private static ISessionFactory _sessionFactory;

        public static NHibernateSessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    object _sync = new object();
                    lock (_sync)
                        _instance = new NHibernateSessionManager();
                }
                return _instance;
            }
        }

        public ISession Session
        {
            get
            {
                if (_sessionFactory == null)
                {
                    object _sync = new object();
                    lock (_sync)
                        _sessionFactory = getConfig().BuildSessionFactory();
                    return _sessionFactory.OpenSession();
                }
                return _sessionFactory.OpenSession();
            }
        }

        private Configuration getConfig()
        {
            Configuration cfg = new Configuration();
            cfg.SetProperties(ConfigurationUtil.getNHibernaterConfig());
            NHibernateTypeDefs.registerTypeDefs(cfg);
            cfg.AddAssembly("fulcrum_services");
            cfg.SetInterceptor(new FulcrumInterceptor());
            cfg.CurrentSessionContext<CallSessionContext>();
            return cfg;
        }

    }
}