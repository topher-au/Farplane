using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Farplane.FarplaneMod
{
    /// <summary>
    /// Interaction logic for ModLoadWindow.xaml
    /// </summary>
    public partial class ModLoadWindow : MetroWindow
    {
        private List<string> scriptNames;
        public bool ShowExceptionError = false;

        private bool Finished = false;

        public ModLoadWindow(string[] scripts)
        {
            InitializeComponent();
            scriptNames = new List<string>();
            foreach(var script in scripts)
                scriptNames.Add(System.IO.Path.GetFileName(script));
        }

        public void CompileStarted()
        {
            Dispatcher.Invoke(() =>
            {
                TextText.Text = "Starting compilation...";
                ProgressProgress.Maximum = scriptNames.Count;
            });

        }

        public void CompileProgress(int progress)
        {
            Dispatcher.Invoke(() =>
            {
                TextText.Text = $"Compiling {scriptNames[progress]}";
                ProgressProgress.Value = progress;
            });
        }

        public void CompileFinished()
        {
            Finished = true;
            Dispatcher.Invoke(() =>
            {
                ProgressProgress.Value = ProgressProgress.Maximum;
                if(ShowExceptionError)
                    MessageBox.Show(
                        "There were errors during script compilation. Please see the log files in the mods folder for full details.",
                        "Farplane Mod Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            });
        }

        public void CompileException()
        {
            ShowExceptionError = true;
        }

        private void ModLoadWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!Finished) e.Cancel = true;
        }
    }
}
