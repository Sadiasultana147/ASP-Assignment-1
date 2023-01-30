using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class JsonFormatter
    {
        public static string Convert(object item)
        {
            //Create StringBuilder Object
            StringBuilder stringBuilder = new StringBuilder();

            if (item == null)
            {
                stringBuilder.Append(" \"\" ");

            }
            else
            {
                // Get Parameter's Data Type
                Type type = item.GetType();

                // Get Parameter's Properties Info
                IEnumerable<PropertyInfo> propertyInfo = type.GetProperties();

                //Check Data Type is Primitive or string
                if (!item.GetType().IsPrimitive && item.GetType() != typeof(string))
                {
                    bool checkArray = typeof(IEnumerable).IsAssignableFrom(item.GetType());
                    // Json Structure
                    if (checkArray == true)
                    {
                        stringBuilder.Append($"[");
                    }
                    else
                    {
                        stringBuilder.Append($"{{");
                    }
                    if (checkArray == true)
                    {
                        foreach (var pInfo in propertyInfo)
                            if (pInfo.GetType().IsPrimitive || pInfo.GetType() == typeof(string))
                            {
                                stringBuilder.Append($"\"{pInfo}\", ");
                            }

                            else
                            {
                                stringBuilder.Append($"\"{pInfo.Name}\": {Convert(pInfo.GetValue(item))}, ");
                            }

                    }
                    else
                    {
                        foreach (var pInfo in propertyInfo)
                        {
                            if (pInfo.PropertyType.IsPrimitive || pInfo.PropertyType == typeof(string))
                            {
                                stringBuilder.Append($"\"{pInfo.Name}\": \"{pInfo.GetValue(item)}\", ");
                            }

                            else
                            {
                                stringBuilder.Append($"\"{pInfo.Name}\": {Convert(pInfo.GetValue(item))}, ");
                            }
                        }


                    }

                    stringBuilder.Remove(stringBuilder.ToString().Length - 2, 2);

                    if (checkArray == true)
                    {
                        stringBuilder.Append($"]");
                    }

                    else
                    {
                        stringBuilder.Append($"}}");
                    }
                }

                else
                {
                    stringBuilder.Append(item.ToString());
                }
            }

            return stringBuilder.ToString();
        }
    }
}
