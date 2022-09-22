using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace XwaWorkspaceEditor
{
    public class EnumFlagsComboBox : ComboBox
    {
        private Type enumType;

        static EnumFlagsComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumFlagsComboBox), new FrameworkPropertyMetadata(typeof(EnumFlagsComboBox)));
        }

        public EnumFlagsComboBox()
            : base()
        {
            this.IsEditable = true;
            this.IsReadOnly = true;
            this.AddHandler(TextBox.PreviewMouseLeftButtonDownEvent, new RoutedEventHandler(this.Button_Click));
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.Button_Click));
            this.DropDownOpened += this.EnumFlagsComboBox_DropDownOpened;
            this.AddHandler(CheckBox.ClickEvent, new RoutedEventHandler(this.ItemClick));
        }

        public Type EnumType
        {
            get { return this.enumType; }

            set
            {
                this.enumType = value;

                if (this.enumType != null)
                {
                    this.ItemsSource = from object v in Enum.GetValues(this.enumType)
                                       where Convert.ToInt32(v) != 0
                                       select new CheckBox()
                                       {
                                           Content = v
                                       };
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = true;
        }

        private void EnumFlagsComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (this.EnumType != null)
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    int target = Convert.ToInt32(Enum.Parse(this.EnumType, this.Text));

                    foreach (CheckBox item in this.Items)
                    {
                        int c = Convert.ToInt32(item.Content);
                        item.IsChecked = ((target & c) == c);
                    }
                }
            }
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        {
            if (this.EnumType != null)
            {
                int target = 0;

                foreach (CheckBox item in this.Items)
                {
                    if (item.IsChecked.GetValueOrDefault())
                    {
                        target |= Convert.ToInt32(item.Content);
                    }
                }

                this.Text = Enum.ToObject(this.EnumType, target).ToString();
            }
        }
    }
}
