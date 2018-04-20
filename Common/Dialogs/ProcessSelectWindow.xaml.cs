using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Farplane.FFX2;
using Farplane.Memory;
using Farplane.Properties;
using MahApps.Metro.Controls;

namespace Farplane.Common.Dialogs
{
    /// <summary>
    /// Interaction logic for ProcessSelectWindow.xaml
    /// </summary>
    public partial class ProcessSelectWindow : MetroWindow
    {
        private string _moduleName = string.Empty;
        private Process _selectedProcess;
        public Process ResultProcess => DialogResult == false ? null : _selectedProcess;
        private bool _ready;

        public ProcessSelectWindow(string moduleName)
        {
            InitializeComponent();
            _moduleName = moduleName;

            if (Settings.Default.ShowAllProcesses)
            {
                ButtonShowAll.IsChecked = true;
            }

            PopulateProcessList(_moduleName);
        }

        private void PopulateProcessList(string moduleName)
        {
            _ready = false;
            ProcessList.Items.Clear();

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (string.Compare(process.ProcessName,moduleName, StringComparison.CurrentCultureIgnoreCase) != 0 && !ButtonShowAll.IsChecked.Value) continue;

                try
                {
                    var processFile = process.MainModule.FileName;
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(processFile);

                    ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());

                    var processItem = new ProcessListItem
                    {
                        ProcessIcon = imageSource,
                        ProcessID = process.Id,
                        ProcessName = process.ProcessName,
                        Process = process
                    };

                    ProcessList.Items.Add(processItem);
                }
                catch
                {
                    continue;
                }
                
            }
            _ready = true;
        }

        private void SelectProcess_Click(object sender, RoutedEventArgs e)
        {
            if (!_ready || ProcessList.SelectedItems.Count != 1) return;

            var selectedProcess = ((ProcessListItem) ProcessList.SelectedItem).Process;
            var attachResult = GameMemory.Attach(selectedProcess);
            if (attachResult == false) return;
            LegacyMemoryReader.Attach(selectedProcess);
            _selectedProcess = selectedProcess;
            DialogResult = true;
            Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            PopulateProcessList(ButtonShowAll.IsChecked != null && ButtonShowAll.IsChecked.Value ? string.Empty : _moduleName);
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;
            RefreshList();
        }

        private void ButtonFileMode_OnClick(object sender, RoutedEventArgs e)
        {
            _selectedProcess = null;
            DialogResult = true;
            Close();
        }
    }

    class ProcessListItem
    {
        public ImageSource ProcessIcon { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public Process Process { get; set; }
    }

}