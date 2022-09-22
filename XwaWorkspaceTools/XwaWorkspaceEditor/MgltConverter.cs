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
    public sealed class MgltConverter : IValueConverter
    {
        public static readonly MgltConverter Default = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is short shortValue)
            {
                return XwaConvert.ToMglt(shortValue);
            }

            if (value is int intValue)
            {
                return XwaConvert.ToMglt(intValue);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string s || !int.TryParse(s, out int v))
            {
                return null;
            }

            v = XwaConvert.FromMglt(v);

            if (targetType == typeof(short))
            {
                return (short)v;
            }

            if (targetType == typeof(int))
            {
                return v;
            }

            return null;
        }
    }
}
