using JeremyAnsel.Xwa.Workspace;
using Microsoft.Win32;
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

namespace XwaShpInstaller
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

            if (!System.IO.Directory.Exists(this.ViewModel.WorkingDirectory))
            {
                this.Close();
                return;
            }

            try
            {
                this.DataContext = new ViewModel(this.ViewModel.WorkingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
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
                this.ViewModel.WorkingDirectory = dlg.FileName;
            }
        }

        private void OpenShpFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".shp",
                CheckFileExists = true,
                Filter = "SHP files (*.shp)|*.shp"
            };

            string fileName;

            if (dialog.ShowDialog(this) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                this.ViewModel.ShpFile = new XwaShpFile(fileName);
                this.SetSpeciesObjectCraftSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Open SHP file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetDefaultSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.SetSpeciesObjectCraftSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Set default indices", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetSpeciesObjectCraftSelection()
        {
            XwaShpFile shp = this.ViewModel.ShpFile;

            if (shp == null || shp.ObjectIndex < 0)
            {
                this.speciesList.SelectedIndex = 0;
                this.objectsList.SelectedIndex = 0;
                this.craftsList.SelectedIndex = 0;
                return;
            }

            int speciesIndex = this.ViewModel
                .Species
                .FirstOrDefault(t => t.SpecDesc?.CraftLongName?.StartsWith("*") ?? false)
                ?.Id ?? 0;

            int objectIndex = shp.ObjectIndex;

            int craftIndex = objectIndex < this.ViewModel.Objects.Count ? this.ViewModel.Objects[objectIndex].Object.CraftIndex : 0;

            this.speciesList.SelectedIndex = speciesIndex;
            this.objectsList.SelectedIndex = objectIndex;
            this.craftsList.SelectedIndex = craftIndex;

            this.speciesList.ScrollIntoView(this.speciesList.SelectedItem);
            this.objectsList.ScrollIntoView(this.objectsList.SelectedItem);
            this.craftsList.ScrollIntoView(this.craftsList.SelectedItem);
        }

        private void InstallShpFile_Click(object sender, RoutedEventArgs e)
        {
            XwaShpFile shp = this.ViewModel.ShpFile;

            if (shp == null)
            {
                return;
            }

            int speciesIndex = this.speciesList.SelectedIndex;

            if (speciesIndex < 1)
            {
                return;
            }

            int objectIndex = this.objectsList.SelectedIndex;

            if (objectIndex < 0)
            {
                return;
            }

            int craftIndex = this.craftsList.SelectedIndex;

            if (craftIndex < 0)
            {
                return;
            }

            if (MessageBox.Show(this, $"{shp.CraftLongName} will be installed.\nDo you want to continue?", "Install SHP file", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            if (this.ViewModel.Workspace == null)
            {
                return;
            }

            try
            {
                this.ViewModel.Workspace.InstallShpFile(shp, speciesIndex, objectIndex, craftIndex);
                this.ViewModel.Workspace.Write(this.ViewModel.Workspace.WorkingDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Install SHP file", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                this.DataContext = new ViewModel(this.ViewModel.WorkingDirectory);
                this.ViewModel.ShpFile = shp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            this.speciesList.SelectedIndex = speciesIndex;
            this.objectsList.SelectedIndex = objectIndex;
            this.craftsList.SelectedIndex = craftIndex;

            this.speciesList.ScrollIntoView(this.speciesList.SelectedItem);
            this.objectsList.ScrollIntoView(this.objectsList.SelectedItem);
            this.craftsList.ScrollIntoView(this.craftsList.SelectedItem);

            MessageBox.Show(this, $"{shp.CraftLongName} is installed.", "Install SHP file", MessageBoxButton.OK);
        }
    }
}
