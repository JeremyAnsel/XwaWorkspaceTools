using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace XwaWorkspaceEditor
{
    public sealed class WeaponDurationConverter : IMultiValueConverter
    {
        public static readonly WeaponDurationConverter Default = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null || values.Length != 2 || values.Any(t => t == DependencyProperty.UnsetValue))
            {
                return null;
            }

            ushort integerPart = (ushort)values[0];
            ushort decimalPart = (ushort)values[1];

            float duration = integerPart + (float)decimalPart / 65536;
            return duration;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is not string s || !float.TryParse(s, out float v))
            {
                return new object[] { null, null };
            }

            if (targetTypes is null
                || targetTypes.Length != 2
                || targetTypes[0] != typeof(ushort)
                || targetTypes[1] != typeof(ushort))
            {
                return new object[] { null, null };
            }

            var targets = new object[2];

            targets[0] = (ushort)v;
            targets[1] = (ushort)((v % 1.0f) * 65536);

            return targets;
        }
    }
}
