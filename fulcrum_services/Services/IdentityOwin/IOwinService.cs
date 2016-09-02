using fulcrum_services.IdentityOwin;
using fulcrum_services.Models;
using fulcrum_services.Models.FulcrumUser;
using fulcrum_services.NHibernate.CustomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Services.IdentityOwin
{
    public interface IOwinService
    {
        void saveOrUpdate<T>(T user) where T : BaseModel;

        void delete<T>(T user) where T : BaseModel;

        void deleteUser(long id);

        void deleteRole(FulcrumUser user, string role);

        void addRoleToUser(FulcrumUser user, Role role);

        T findById<T>(long id) where T : BaseModel;

        FulcrumUser findByEmail(string email);

        FulcrumUser findByName(string userName);

        FulcrumUserDetail getUserDetails(FulcrumUser user);

        IList<string> getRolesForUser(FulcrumUser user);

        LoginResponse validateUser(string username, string password);
    }
}
