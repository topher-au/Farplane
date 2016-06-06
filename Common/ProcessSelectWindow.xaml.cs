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
using Farplane.Properties;
using MahApps.Metro.Controls;

namespace Farplane.Common
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
                _moduleName = string.Empty;
                ShowAllProcesses.IsChecked = true;
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
                if (!process.ProcessName.Contains(moduleName)) continue;

                try
                {
                    var processFile = process.MainModule.FileName;
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(processFile);

                    ImageSource processIcon = null;
                    if (icon != null)
                        processIcon = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());

                    var processItem = new ProcessListItem
                    {
                        ProcessIcon = processIcon,
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
            MemoryReader.Attach(selectedProcess);
            DialogResult = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowAllProcesses_Click(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;
            ShowAllProcesses.IsChecked = !ShowAllProcesses.IsChecked;
            RefreshList();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            if (ShowAllProcesses.IsChecked)
                PopulateProcessList(string.Empty);
            else
                PopulateProcessList(_moduleName);
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