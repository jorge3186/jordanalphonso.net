using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_common.Utils
{
    public class TypeUtil
    {
        public static Type getGenericType(string type)
        {
            if (type == null || type.Equals(StringUtil.E))
            {
                return null;
            }

            string convertedType = null;
            switch (type)
            {
                case "bool":
                case "boolean":
                    convertedType = "System.Boolean";
                    break;
                case "byte":
                    convertedType = "System.Byte";
                    break;
                case "char":
                    convertedType = "System.Char";
                    break;
                case "datetime":
                    convertedType = "System.DateTime";
                    break;
                case "datetimeoffset":
                    convertedType = "System.DateTimeOffset";
                    break;
                case "decimal":
                    convertedType = "System.Decimal";
                    break;
                case "double":
                    convertedType = "System.Double";
                    break;
                case "float":
                    convertedType = "System.Single";
                    break;
                case "int16":
                case "short":
                    convertedType = "System.Int16";
                    break;
                case "int32":
                case "int":
                    convertedType = "System.Int32";
                    break;
                case "int64":
                case "long":
                    convertedType = "System.Int64";
                    break;
                case "object":
                    convertedType = "System.Object";
                    break;
                case "sbyte":
                    convertedType = "System.SByte";
                    break;
                case "string":
                    convertedType = "System.String";
                    break;
                case "timespan":
                    convertedType = "System.TimeSpan";
                    break;
                case "uint16":
                case "ushort":
                    convertedType = "System.UInt16";
                    break;
                case "uint32":
                case "uint":
                    convertedType = "System.UInt32";
                    break;
                case "uint64":
                case "ulong":
                    convertedType = "System.UInt64";
                    break;
            }

            return Type.GetType(convertedType);
        }

        public static object TryParse(Type type, string input)
        {
            switch (type.ToString())
            {
                case "datetime":
                    DateTime timeResult;
                    bool timeValid = DateTime.TryParse(input,  out timeResult);
                    if (timeValid)
                    {
                        return timeResult;
                    }
                    return null;
                case "decimal":
                    decimal decimalResult;
                    bool decimalValid = decimal.TryParse(input, out decimalResult);
                    if (decimalValid)
                    {
                        return decimalResult;
                    }
                    return null;
                case "double":
                    double doubleResult;
                    bool doubleValid = double.TryParse(input, out doubleResult);
                    if (doubleValid)
                    {
                        return doubleResult;
                    }
                    return null;
                case "float":
                    float floatResult;
                    bool floatValid = float.TryParse(input, out floatResult);
                    if (floatValid)
                    {
                        return floatResult;
                    }
                    return null;
                case "System.Int16":
                    short shortResult;
                    bool valid = short.TryParse(input, out shortResult);
                    if (valid)
                    {
                        return shortResult;
                    }
                    return null;
                case "System.Int32":
                    int intResult;
                    bool intValid = int.TryParse(input, out intResult);
                    if (intValid)
                    {
                        return intResult;
                    }
                    return null;
                case "System.Int64":
                    long longResult;
                    bool longValid = long.TryParse(input, out longResult);
                    if (longValid)
                    {
                        return longResult;
                    }
                    return null;
                case "string":
                    return input as string;
                case "uint16":
                case "ushort":
                    ushort uShortResult;
                    bool uShortValid = ushort.TryParse(input, out uShortResult);
                    if (uShortValid)
                    {
                        return uShortResult;
                    }
                    return null;
                case "uint32":
                case "uint":
                    uint uIntResult;
                    bool uIntValid = uint.TryParse(input, out uIntResult);
                    if (uIntValid)
                    {
                        return uIntResult;
                    }
                    return null;
                case "uint64":
                case "ulong":
                    ulong uLongResult;
                    bool uLongValid = ulong.TryParse(input, out uLongResult);
                    if (uLongValid)
                    {
                        return uLongResult;
                    }
                    return null;
            }
            return null;
        }
    }
}
