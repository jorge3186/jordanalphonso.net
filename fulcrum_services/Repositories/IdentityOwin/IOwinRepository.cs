using fulcrum_services.IdentityOwin;
using fulcrum_services.Models;
using fulcrum_services.Models.FulcrumUser;
using System.Collections.Generic;

namespace fulcrum_services.Repositories.IdentityOwin
{
    public interface IOwinRepository
    {
        void saveOrUpdate<T>(T obj) where T : BaseModel;

        void delete<T>(T obj) where T : BaseModel;

        void deleteUser(long id);

        T findById<T>(long id) where T : BaseModel;

        T fetchById<T>(long id) where T : BaseModel;

        IList<T> fetchByUserId<T>(long id) where T : BaseModel;

        T fetchByProperty<T>(string propertyName, object value) where T : BaseModel;

        LoginResponse validateLogin(string username, string password);

    }
}
