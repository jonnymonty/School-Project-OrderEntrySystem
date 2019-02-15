using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public static class ReflectionUtil
    {
        public static bool HasAttribute(MemberInfo memberInfo, Type attributeType)
        {
            Attribute[] attributes = (Attribute[])memberInfo.GetCustomAttributes(attributeType, false);

            if (attributes.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static object GetAttributePropertyValue(MemberInfo memberInfo, Type attributeType, string propertyName)
        {
            object result = null;

            // Read all attributes of the specified type from the member.
            Attribute[] attributes = (Attribute[])memberInfo.GetCustomAttributes(attributeType, false);

            if (attributes.Length > 0)
            {
                // Get the first attribute (only one is supported).
                Attribute attribute = attributes[0];

                // Read the property from the attribute.
                PropertyInfo propertyInfo = attribute.GetType().GetProperty(propertyName);

                // If property is found...
                if (propertyInfo != null)
                {
                    // Read the value from the property.
                    result = propertyInfo.GetValue(attribute, null);
                }
            }

            return result;
        }

        public static string GetAttributePropertyValueAsString(MemberInfo memberInfo, Type attributeType, string propertyName)
        {
            object result = GetAttributePropertyValue(memberInfo, attributeType, propertyName);

            return result as string;
        }
    }
}