using fulcrum_services.Models;
using fulcrum_services.NHibernate;
using fulcrum_services.NHibernate.Criteria;
using System.Collections.Generic;

namespace fulcrum_services.Repositories
{
    public interface IGenericRepository
    {
        QueryResult<T> delete<T>(T entity) where T : BaseModel;

        QueryResult<T> delete<T>(ICollection<T> entities) where T : BaseModel;

        T loadById<T>(long id) where T : BaseModel;

        T loadByProperty<T>(string property, object value) where T : BaseModel;

        IList<T> loadListByProperty<T>(string property, object value) where T : BaseModel;

        QueryResult<T> saveOrUpdate<T>(T entity) where T : BaseModel;

        QueryResult<T> saveOrUpdate<T>(ICollection<T> entities) where T : BaseModel;

        IList<T> fetchAll<T>() where T : BaseModel;

        IList<T> get<T>(FRestrictions restrictions) where T : BaseModel;
    }
}
