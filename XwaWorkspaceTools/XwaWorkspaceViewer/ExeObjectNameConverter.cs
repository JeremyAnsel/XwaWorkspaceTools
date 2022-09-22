using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace XwaWorkspaceViewer
{
    public sealed class ExeObjectNameConverter : IMultiValueConverter
    {
        public static readonly ExeObjectNameConverter Default = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }

            if (AppSettings.Workspace == null)
            {
                return string.Empty;
            }

            return AppSettings.Workspace.GetModelName((short)values[0], (short)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
