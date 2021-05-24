using System;
using System.Reflection;

namespace AutoDailyReport.Domain.Extensions
{
    public static class ObjectExt
    {
        public static bool IsHasAttribute<T>(this object objectItem) where T : Attribute
        {
            return GetObjectAttributes(objectItem, typeof(T)) != null;
        }

        public static string GetAttributeName<T>(this object objectItem) where T : Attribute
        {
            var attributeType = typeof(T);
            var attribute = GetObjectAttributes(objectItem, attributeType);
            string name = null;

            if (attribute != null)
            {
                var attributeProperty = attributeType.GetProperty("Name");

                if (attributeProperty != null)
                {
                    name = attributeProperty.GetValue(attribute).ToString();
                }
            }

            return name;
        }

        private static Attribute? GetObjectAttributes(object objectItem, Type type)
        {
            return objectItem.GetType().GetCustomAttribute(type, false);
        }
    }
}
