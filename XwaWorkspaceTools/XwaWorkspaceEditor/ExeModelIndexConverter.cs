using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace XwaWorkspaceEditor
{
    public sealed class ExeModelIndexConverter : IMultiValueConverter
    {
        public static readonly ExeModelIndexConverter Default = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4 || values.Any(t => t == DependencyProperty.UnsetValue))
            {
                return string.Empty;
            }

            int modelIndex = System.Convert.ToInt32(values[0]);
            var objects = values[1] as IList<ViewModel.ObjectEntry>;
            var flightModelSpacecraft = values[2] as IList<ViewModel.FlightModelSpacecraftEntry>;
            var flightModelEquipment = values[3] as IList<ViewModel.FlightModelEquipmentEntry>;

            var obj = objects.ElementAtOrDefault(modelIndex);

            if (obj is null)
            {
                return string.Empty;
            }

            int dataIndex1 = obj.DataIndex1;
            int dataIndex2 = obj.DataIndex2;

            if (dataIndex1 == -1)
            {
                return string.Empty;
            }

            string entry = dataIndex1 switch
            {
                0 => flightModelSpacecraft.ElementAtOrDefault(dataIndex2)?.Value,
                1 => flightModelEquipment.ElementAtOrDefault(dataIndex2)?.Value,
                _ => null,
            };

            return entry != null ? Path.GetFileNameWithoutExtension(entry) : (dataIndex1 + ", " + dataIndex2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
