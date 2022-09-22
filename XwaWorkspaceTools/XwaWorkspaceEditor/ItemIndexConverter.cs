using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;
using System.Collections;

namespace XwaWorkspaceEditor
{
    public class ItemIndexConverter : FrameworkContentElement, IValueConverter
    {
        public ItemIndexConverter()
        {
        }

        public int StartIndex
        {
            get { return (int)GetValue(StartIndexProperty); }
            set { SetValue(StartIndexProperty, value); }
        }

        public static readonly DependencyProperty StartIndexProperty =
            DependencyProperty.Register("StartIndex", typeof(int), typeof(ItemIndexConverter), new PropertyMetadata(0));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DataContext is not IList list)
            {
                return string.Empty;
            }

            int index = list.IndexOf(value) + StartIndex;

            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
