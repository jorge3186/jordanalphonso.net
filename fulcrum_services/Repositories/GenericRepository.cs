using fulcrum_services.Models;
using fulcrum_services.NHibernate;
using System.Collections.Generic;
using System;
using fulcrum_services.NHibernate.Criteria;
using NHibernate;
using NHibernate.Criterion;
using static fulcrum_services.NHibernate.Criteria.FRestrictions;

namespace fulcrum_services.Repositories
{
    public class GenericRepository : NHibernateRepoWrapper, IGenericRepository
    {
        public QueryResult<T> delete<T>(ICollection<T> entities) where T : BaseModel
        {
            return deleteEntities(entities);
        }

        public QueryResult<T> delete<T>(T entity) where T : BaseModel
        {
            return deleteEntity(entity);
        }

        public IList<T> fetchAll<T>() where T : BaseModel
        {
            return fetch<T>();
        }

        public IList<T> get<T>(FRestrictions restrictions) where T : BaseModel
        {
            ISession session = getCurrentSession();
            ICriteria criteria = session.CreateCriteria<T>();
            handleRestrictions(criteria, restrictions);
            return criteria.List<T>();
        }

        public T loadById<T>(long id) where T : BaseModel
        {
            return fetch<T>(id);
        }

        public T loadByProperty<T>(string property, object value) where T : BaseModel
        {
            return fetch<T>(property, value);
        }

        public IList<T> loadListByProperty<T>(string property, object value) where T : BaseModel
        {
            return fetchList<T>(property, value);
        }

        public QueryResult<T> saveOrUpdate<T>(ICollection<T> entities) where T : BaseModel
        {
            return merge(entities);
        }

        public QueryResult<T> saveOrUpdate<T>(T entity) where T : BaseModel
        {
            return merge(entity);
        }

        private void handleRestrictions(ICriteria criteria, FRestrictions restrictions)
        {
            foreach (KeyValuePair<string, IList<FRestriction>> entry in restrictions.restrictions)
            {
                foreach (var rest in entry.Value)
                {
                    ICriterion restriction = getRestriction(entry.Key, rest);
                    criteria.Add(restriction);
                }
            }
        }

        private ICriterion getRestriction(string property, FRestriction rest)
        {
            ICriterion restriction = null;
            FQualifier qual = rest.qualifier;

            if (FQualifier.GE.Equals(qual))
            {
                restriction = Restrictions.Ge(property, rest.value);
            }
            else if (FQualifier.GT.Equals(qual))
            {
                restriction = Restrictions.Gt(property, rest.value);
            }
            else if (FQualifier.IN.Equals(qual))
            {
                restriction = Restrictions.In(property, rest.value as object[]);
            }
            else if (FQualifier.LE.Equals(qual))
            {
                restriction = Restrictions.Le(property, rest.value);
            }
            else if (FQualifier.LT.Equals(qual))
            {
                restriction = Restrictions.Lt(property, rest.value);
            }
            else if (FQualifier.NOT_NULL.Equals(qual))
            {
                restriction = Restrictions.IsNotNull(property);
            }
            else if (FQualifier.NULL.Equals(qual))
            {
                restriction = Restrictions.IsNull(property);
            }
            else
            {
                restriction = Restrictions.Eq(property, rest.value);
            }

            return restriction;
        }
    }
}
