using System;
using System.ComponentModel;
using System.Globalization;

namespace FestivalApp.BL.Mappers
{
    public class EnumMapper : EnumConverter
    {
        public EnumMapper(Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value == null)
            {
                return string.Empty;
            }

            var fieldInfo = value.GetType().GetField(value.ToString() ?? string.Empty);
            if (fieldInfo == null)
            {
                return string.Empty;
            }

            var attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description)
                ? attributes[0].Description
                : value.ToString();
        }

    }
}