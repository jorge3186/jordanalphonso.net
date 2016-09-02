using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.NHibernate.CustomTypes
{
    public class Role : ICustomType
    {
        public readonly static Role OWNER = new Role("OWN", "Owner");
        public readonly static Role DEVELOPER = new Role("DEV", "Developer");
        public readonly static Role VISITOR = new Role("VIST", "Visitor");
        public readonly static Role CLIENT = new Role("CLNT", "Client");
        public readonly static Role ARTIST = new Role("ART", "Artist");

        public string _code { get; private set; }
        public string _label { get; private set; }

        public Role(string code, string label)
        {
            _code = code;
            _label = label;
        }

        public static Role getRole(string codeOrLabel)
        {
            var fields = typeof(Role).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var f in fields)
            {
                Role role = (Role)f.GetValue(null);
                if (role._code.ToLower().Equals(codeOrLabel) ||
                    role._label.ToLower().Equals(codeOrLabel))
                {
                    return role;
                }
            }
            return null;
        }

        public static IList<Role> getAllRoles()
        {
            IList<Role> roles = new List<Role>();
            var fields = typeof(Role).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var f in fields)
            {
                Role role = (Role)f.GetValue(null);
                roles.Add(role);
            }
            return roles;
        }

        public ICustomType getType(string codeOrLabel)
        {
            return getRole(codeOrLabel);
        }

        public object getCode()
        {
            return _code;
        }
    }
}