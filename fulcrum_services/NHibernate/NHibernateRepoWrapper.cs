using fulcrum_services.Models;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace fulcrum_services.NHibernate
{
    public class NHibernateRepoWrapper
    {
        protected ISession getCurrentSession()
        {
            return NHibernateSessionManager.Instance.Session;
        }

        protected QueryResult<T> deleteEntity<T>(T obj) where T : BaseModel
        {
            ISession session = getCurrentSession();
            ITransaction tx = session.BeginTransaction();
            session.Delete(obj);
            tx.Commit();
            session.Flush();

            return new QueryResult<T>(0, 0, 1, null);
        }

        protected QueryResult<T> deleteEntities<T>(ICollection<T> objs) where T : BaseModel
        {
            ISession session = getCurrentSession();
            ITransaction tx = session.BeginTransaction();
            int counter = 0;

            foreach (var o in objs)
            {
                session.Delete(o);
                counter++;
            }
            tx.Commit();
            session.Flush();

            return new QueryResult<T>(0, 0, counter, null);
        }

        protected QueryResult<T> merge<T>(ICollection<T> objs) where T : BaseModel
        {
            int updated = 0;
            int inserted = 0;
            IList<T> savedList = new List<T>();
            if (objs != null && objs.Count > 0)
            {
                ISession session = getCurrentSession();
                ITransaction tx = session.BeginTransaction();
                foreach (var obj in objs)
                {
                    if (obj.version != 0)
                    {
                        session.SaveOrUpdate(obj);
                        updated++;
                    }
                    else
                    {
                        session.SaveOrUpdate(obj);
                        inserted++;
                    }
                    savedList.Add(obj);
                }
                tx.Commit();
                session.Flush();
            }

            return new QueryResult<T>(updated, inserted, 0, savedList);
        }

        protected QueryResult<T> merge<T>(T obj) where T : BaseModel
        {
            int inserted = 0;
            int updated = 0;
            IList<T> returnList = new List<T>();

            if (obj != null)
            {
                ISession session = getCurrentSession();
                ITransaction tx = session.BeginTransaction();
                session.SaveOrUpdate(obj);
                tx.Commit();
                session.Flush();
                returnList.Add(obj);

                if (obj.version != 0)
                {
                    updated++;
                }
                else
                {
                    inserted++;
                }
            }
            return new QueryResult<T>(updated, inserted, 0, returnList);
        }

        protected T fetch<T>(long id) where T : BaseModel
        {
            ISession session = getCurrentSession();
            T obj = (T) session.Get(typeof(T), id);
            return obj;
        }

        protected IList<T> fetch<T>() where T : BaseModel
        {
            ISession session = getCurrentSession();
            ICriteria crit = session.CreateCriteria<T>();
            return crit.List<T>();
        }

        protected T fetch<T>(string propertyName, object value) where T : BaseModel
        {
            ISession session = getCurrentSession();
            ICriteria criteria = session.CreateCriteria<T>();
            IList<T> results = criteria.Add(Restrictions.Eq(propertyName, value))
                .SetMaxResults(1)
                .List<T>();
            if (results != null && results.Count > 0)
            {
                return results.First();
            }
            return null;
        }

        protected IList<T> fetchList<T>(string propertyName, object value) where T : BaseModel
        {
            ISession session = getCurrentSession();
            ICriteria criteria = session.CreateCriteria<T>();
            return criteria.Add(Restrictions.Eq(propertyName, value))
                .List<T>();
        }
    }
}
