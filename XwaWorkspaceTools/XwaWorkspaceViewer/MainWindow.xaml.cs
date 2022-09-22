using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XwaWorkspaceViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += Window_Loaded;
        }

        private ViewModel ViewModel
        {
            get { return (ViewModel)this.DataContext; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.SetWorkingDirectory();

            if (!System.IO.Directory.Exists(AppSettings.WorkingDirectory))
            {
                this.Close();
                return;
            }

            try
            {
                this.DataContext = new ViewModel(AppSettings.WorkingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetWorkingDirectory();

            if (!System.IO.Directory.Exists(AppSettings.WorkingDirectory))
            {
                return;
            }

            try
            {
                this.DataContext = new ViewModel(AppSettings.WorkingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetWorkingDirectory()
        {
            var dlg = new FolderBrowserForWPF.Dialog()
            {
                Title = "Choose a working directory containing " + XwaWorkspace.ExeName
            };

            if (dlg.ShowDialog() == true)
            {
                AppSettings.SetData(dlg.FileName);
            }
        }
    }
}
