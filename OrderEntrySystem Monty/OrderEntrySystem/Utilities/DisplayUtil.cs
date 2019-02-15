using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public static class DisplayUtil
    {
        public static string GetControlDescription(MemberInfo memberInfo)
        {
            string result = ReflectionUtil.GetAttributePropertyValueAsString(memberInfo, typeof(EntityControlAttribute), "Description");
            if (result == null || result == string.Empty)
            {
                return memberInfo.Name;
            }
            else
            {
                return result;
            }
        }

        public static string GetFieldDescription(MemberInfo memberInfo)
        {
            string result = ReflectionUtil.GetAttributePropertyValueAsString(memberInfo, typeof(EntityDescriptionAttribute), "Description");
            if (result == null || result == string.Empty)
            {
                return memberInfo.Name.ToString();
            }
            else
            {
                return result;
            }
        }

        public static ControlType GetControlType(PropertyInfo propertyInfo)
        {
            object controlType = ReflectionUtil.GetAttributePropertyValue(propertyInfo, typeof(EntityControlAttribute), "ControlType");
            if (controlType == null)
            {
                return ControlType.None;
            }
            else
            {
                return (ControlType)controlType;
            }
        }

        public static int GetControlSequence(MemberInfo memberInfo)
        {
            int result = (int)ReflectionUtil.GetAttributePropertyValue(memberInfo, typeof(EntityControlAttribute), "Sequence");
            if (result <= 0)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static bool HasControl(MemberInfo memberInfo)
        {
            return ReflectionUtil.HasAttribute(memberInfo, typeof(EntityControlAttribute));
        }

        public static string GetColumnDescription(MemberInfo memberInfo)
        {
            string result = ReflectionUtil.GetAttributePropertyValueAsString(memberInfo, typeof(EntityColumnAttribute), "Description");
            if (result == null || result == string.Empty)
            {
                return memberInfo.Name.ToString();
            }
            else
            {
                return result;
            }
        }

        public static int GetColumnSequence(MemberInfo memberInfo)
        {
            int result = (int)ReflectionUtil.GetAttributePropertyValue(memberInfo, typeof(EntityColumnAttribute), "Sequence");

            return result;
        }

        public static int GetColumnWidth(MemberInfo memberInfo)
        {
            int result = (int)ReflectionUtil.GetAttributePropertyValue(memberInfo, typeof(EntityColumnAttribute), "Width");

            return result;
        }

        public static bool HasColumn(MemberInfo memberInfo)
        {
            return ReflectionUtil.HasAttribute(memberInfo, typeof(EntityColumnAttribute));
        }
    }
}
