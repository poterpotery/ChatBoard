using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class EnumDescription
    {
        public static string GetEnumDescription(FeedbackEnum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute attribute =
                    field.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }
    }
}
