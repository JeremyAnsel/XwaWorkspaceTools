using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XwaWorkspaceEditor
{
    public sealed class MetersConverter : IValueConverter
    {
        public static readonly MetersConverter Default = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is short shortValue)
            {
                return XwaConvert.ToMeters(shortValue);
            }

            if (value is int intValue)
            {
                return XwaConvert.ToMeters(intValue);
            }

            if (value is float floatValue)
            {
                return XwaConvert.ToMeters(floatValue);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string s || !float.TryParse(s, out float v))
            {
                return null;
            }

            v = XwaConvert.FromMeters(v);

            if (targetType == typeof(short))
            {
                return (short)Math.Round(v);
            }

            if (targetType == typeof(int))
            {
                return (int)Math.Round(v);
            }

            if (targetType == typeof(float))
            {
                return v;
            }

            return null;
        }
    }
}
