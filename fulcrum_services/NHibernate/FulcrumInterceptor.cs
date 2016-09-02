using NHibernate;
using System;
using NHibernate.Type;
using fulcrum_services.Models;
using fulcrum_services.Models.SessionManagement;

namespace fulcrum_services.NHibernate
{
    public class FulcrumInterceptor : EmptyInterceptor
    {
        private static string VERSION = "version";
        private static string UPDATED_TIME = "updatedTime";
        private static string UPDATED_USER = "updatedUser";

        public override bool OnSave(object entity, object id, object[] state, 
            string[] propertyNames, IType[] types)
        {
            return updateUserInfo(entity, id, state, propertyNames);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, 
            object[] previousState, string[] propertyNames, IType[] types)
        {
            return updateUserInfo(entity, id, currentState, propertyNames);
        }

        private bool updateUserInfo(object entity, object id, object[] state, object[] propertyNames)
        {
            if (entity is BaseModel)
            {
                DateTime now = DateTime.Now;
                int index = 0;

                foreach (var prop in propertyNames)
                {
                    if (UPDATED_USER.Equals(prop))
                    {
                        state[index] = LoggedUser.userName();
                    }
                    else if (UPDATED_TIME.Equals(prop))
                    {
                        state[index] = now;
                    }
                    else if (VERSION.Equals(prop) && id == null)
                    {
                        state[index] = 1;
                    }
                    else if (VERSION.Equals(prop))
                    {
                        state[index] = (int)state[index] + 1;
                    }
                    index++;
                }
            }

            return true;
        }
    }
}
