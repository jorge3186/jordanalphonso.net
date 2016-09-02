using System;
using System.Collections.Generic;
using fulcrum_services.Models;
using fulcrum_services.NHibernate;
using fulcrum_services.NHibernate.Criteria;
using fulcrum_services.Repositories;

namespace fulcrum_services.Services
{
    public class GenericService : IGenericService
    {
        private IGenericRepository _genericRepo;

        public GenericService(IGenericRepository genericRepo)
        {
            _genericRepo = genericRepo;
        }

        public QueryResult<T> delete<T>(ICollection<T> entities) where T : BaseModel
        {
            return _genericRepo.delete(entities);
        }

        public QueryResult<T> delete<T>(T entity) where T : BaseModel
        {
            return _genericRepo.delete(entity);
        }

        public IList<T> fetchAll<T>() where T : BaseModel
        {
            return _genericRepo.fetchAll<T>();
        }

        public IList<T> get<T>(FRestrictions restrictions) where T : BaseModel
        {
            return _genericRepo.get<T>(restrictions);
        }

        public T loadById<T>(long id) where T : BaseModel
        {
            return _genericRepo.loadById<T>(id);
        }

        public T loadByProperty<T>(string property, object value) where T : BaseModel
        {
            return _genericRepo.loadByProperty<T>(property, value);
        }

        public IList<T> loadListByProperty<T>(string property, object value) where T : BaseModel
        {
            return _genericRepo.loadListByProperty<T>(property, value);
        }

        public QueryResult<T> saveOrUpdate<T>(ICollection<T> entities) where T : BaseModel
        {
            return _genericRepo.saveOrUpdate(entities);
        }

        public QueryResult<T> saveOrUpdate<T>(T entity) where T : BaseModel
        {
            return _genericRepo.saveOrUpdate(entity);
        }
    }
}
