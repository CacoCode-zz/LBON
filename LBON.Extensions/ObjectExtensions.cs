using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LBON.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>Gets the display name.</summary>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string GetDisplayName(this ICustomAttributeProvider customAttributeProvider)
        {
            var displayAttribute = customAttributeProvider.GetAttribute<DisplayAttribute>();
            var displayName = displayAttribute != null ? displayAttribute.Name : customAttributeProvider.GetAttribute<DisplayNameAttribute>()?.DisplayName;
            return displayName;
        }

        /// <summary>Gets the description.</summary>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string GetDescription(this ICustomAttributeProvider customAttributeProvider)
        {
            var des = string.Empty;
            var desAttribute = customAttributeProvider.GetAttribute<DescriptionAttribute>();
            if (desAttribute != null) des = desAttribute.Description;
            return des;
        }

        /// <summary>Gets the type display or description.</summary>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string GetTypeDisplayOrDescription(this ICustomAttributeProvider customAttributeProvider)
        {
            var description = customAttributeProvider.GetDescription();
            if (description.IsNullOrWhiteSpace()) description = customAttributeProvider.GetDisplayName();
            return description ?? string.Empty;
        }

        /// <summary>Determines whether this instance is required.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>
        ///   <c>true</c> if the specified property information is required; otherwise, <c>false</c>.</returns>
        public static bool IsRequired(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetAttribute<RequiredAttribute>(true) != null) return true;
            //Boolean、Byte、SByte、Int16、UInt16、Int32、UInt32、Int64、UInt64、Char、Double、Single
            if (propertyInfo.PropertyType.IsPrimitive) return true;
            switch (propertyInfo.PropertyType.Name)
            {
                case "DateTime":
                case "Decimal":
                    return true;
            }
            return false;
        }

        /// <summary>Gets the attribute.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider assembly, bool inherit = false)
            where T : Attribute
        {
            return assembly
                .GetCustomAttributes(typeof(T), inherit)
                .OfType<T>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Enums to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">T must be of type System.Enum</exception>
        public static List<T> EnumToList<T>()
        {
            var enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var enumValArray = Enum.GetValues(enumType);

            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));
            return enumValList;
        }

        /// <summary>
        /// Enums to dictionary.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException">object is not an Enumeration</exception>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            var names = Enum.GetNames(t);
            var values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                select new { Key = names[i], Value = (int)values.GetValue(i) }).ToDictionary(k => k.Key, k => k.Value);
        }
    }
}
