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
    public sealed class ExeModelIndexConverter : IValueConverter
    {
        public static readonly ExeModelIndexConverter Default = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }

            if (AppSettings.Workspace == null)
            {
                return string.Empty;
            }

            int modelIndex = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);

            return AppSettings.Workspace.GetModelName(modelIndex);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
