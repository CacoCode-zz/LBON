using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LBON.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="nameInstead">if set to <c>true</c> [name instead].</param>
        /// <returns></returns>
        [Description("获取枚举DisplayName")]
        public static string GetDisplayName(this Enum value, bool nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            DisplayAttribute attribute = Attribute.GetCustomAttribute(type.GetField(name), typeof(DisplayAttribute)) as DisplayAttribute;
            if (attribute == null && nameInstead == true)
            {
                return name;
            }

            return attribute == null ? null : attribute.Name;
        }
    }
}
