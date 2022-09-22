using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace XwaWorkspaceEditor
{
    public sealed class RuConverter : IMultiValueConverter
    {
        public static readonly RuConverter Default = new();

        private int _craftIndex = int.MinValue;

        private IList<ViewModel.ObjectEntry> _objects = null;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3 || values.Any(t => t == DependencyProperty.UnsetValue))
            {
                return null;
            }

            uint raw = (uint)values[0];
            int craftIndex = System.Convert.ToInt32(values[1]);
            var objects = values[2] as IList<ViewModel.ObjectEntry>;

            _craftIndex = craftIndex;
            _objects = objects;

            ViewModel.ObjectEntry obj = null;

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].CraftIndex == craftIndex)
                {
                    obj = objects[i];
                    break;
                }
            }

            if (obj is null)
            {
                return null;
            }

            return XwaConvert.ToRu(obj.ShipCategory, (int)raw);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is not string s || !uint.TryParse(s, out uint v))
            {
                return new object[3];
            }

            if (targetTypes is null || targetTypes.Length != 3)
            {
                return new object[3];
            }

            ViewModel.ObjectEntry obj = null;

            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].CraftIndex == _craftIndex)
                {
                    obj = _objects[i];
                    break;
                }
            }

            if (obj is null)
            {
                return null;
            }

            var targets = new object[3];

            v = (uint)XwaConvert.FromRu(obj.ShipCategory, (int)v);

            if (targetTypes[0] == typeof(uint))
            {
                targets[0] = v;
            }
            else if (targetTypes[0] == typeof(int))
            {
                targets[0] = (int)v;
            }

            return targets;
        }
    }
}
