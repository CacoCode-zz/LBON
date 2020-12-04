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
    }
}
