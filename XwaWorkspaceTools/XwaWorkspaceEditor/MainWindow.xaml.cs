using JeremyAnsel.Xwa.Workspace;
using System;
using System.Windows;
using System.Windows.Controls;

namespace XwaWorkspaceEditor
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

            string workingDirectory = this.GetWorkingDirectory();

            if (string.IsNullOrEmpty(workingDirectory) || !System.IO.Directory.Exists(workingDirectory))
            {
                this.Close();
                return;
            }

            try
            {
                this.DataContext = new ViewModel(workingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            string workingDirectory = this.GetWorkingDirectory();

            if (string.IsNullOrEmpty(workingDirectory) || !System.IO.Directory.Exists(workingDirectory))
            {
                return;
            }

            try
            {
                this.DataContext = new ViewModel(workingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Do you want to save {ViewModel.WorkingDirectory} ?", this.Title, MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                ViewModel.Save();

                MessageBox.Show($"{ViewModel.WorkingDirectory} saved.", this.Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetWorkingDirectory()
        {
            var dlg = new FolderBrowserForWPF.Dialog()
            {
                Title = "Choose a working directory containing " + XwaWorkspace.ExeName
            };

            if (dlg.ShowDialog() != true)
            {
                return null;
            }

            return dlg.FileName;
        }

        private void SectionsTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
