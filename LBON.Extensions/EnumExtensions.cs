using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LBON.Extensions
{
    public static class EnumExtensions
    {
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
