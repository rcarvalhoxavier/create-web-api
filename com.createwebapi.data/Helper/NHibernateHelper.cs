using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace com.createwebapi.data.Helper
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISession _session;

        private static readonly object Padlock = new object();

        public static ISessionFactory SessionFactory
        {
            get
            {
                lock (Padlock)
                {
                    if (_sessionFactory == null) _sessionFactory = CreateSessionFactory();

                    return _sessionFactory;
                }
            }
        }

        public static ISession Session
        {
            get
            {
                if (!CurrentSessionContext.HasBind(SessionFactory))
                    CurrentSessionContext.Bind(SessionFactory.OpenSession());

                _session = SessionFactory.GetCurrentSession();
                return _session;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var cfg = GetConfiguration();
            return cfg.BuildSessionFactory();
        }

        public static Configuration GetConfiguration()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            return cfg;
        }

        public static void UnbindSessionContext()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
                CurrentSessionContext.Unbind(SessionFactory);
        }
    }
}