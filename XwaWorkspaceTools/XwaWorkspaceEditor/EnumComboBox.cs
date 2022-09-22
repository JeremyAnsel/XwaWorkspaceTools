using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace XwaWorkspaceEditor
{
    public class EnumComboBox : ComboBox
    {
        private Type enumType;

        static EnumComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumComboBox), new FrameworkPropertyMetadata(typeof(EnumComboBox)));
        }

        public EnumComboBox()
            : base()
        {
            this.IsEditable = true;
            this.IsReadOnly = true;
            this.AddHandler(TextBox.PreviewMouseLeftButtonDownEvent, new RoutedEventHandler(this.Button_Click));
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.Button_Click));
        }

        public Type EnumType
        {
            get { return this.enumType; }

            set
            {
                this.enumType = value;

                if (this.enumType != null)
                {
                    this.ItemsSource = Enum.GetValues(this.enumType);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = true;
        }
    }
}
