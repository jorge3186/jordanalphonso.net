using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using NHibernate.SqlTypes;
using System.Data;
using NHibernate;
using fulcrum_services.NHibernate.CustomTypes;
using System.Reflection;

namespace fulcrum_services.NHibernate.HibernateTypes
{
    public abstract class HibernateCustomType<T> : IUserType where T : ICustomType
    {
        public bool IsMutable
        {
            get
            {
                return false;
            }
        }

        public Type ReturnedType
        {
            get
            {
                return typeof(T);
            }
        }

        public SqlType[] SqlTypes
        {
            get
            {
                return new SqlType[] { NHibernateUtil.String.SqlType };
            }
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x == null ? typeof(bool).GetHashCode() + 473 : x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);

            if (obj == null) return null;

            IList<ICustomType> list = new List<ICustomType>();
            FieldInfo[] fields = ReturnedType.GetFields();
            foreach (var field in fields)
            {
                ICustomType type = (ICustomType)field.GetValue(null);
                list.Add(type);
            }

            foreach (var t in list)
            {
                if (obj.ToString().Equals(t.getCode()))
                {
                    return t;
                }
            }
            return null;

        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else if (value is ICustomType)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = ((ICustomType)value).getCode();
            }
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}