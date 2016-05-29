using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Farplane.Common;
using Farplane.FFX;
using Farplane.FFX2;
using MahApps.Metro.Controls;

namespace Farplane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Title = string.Format(Title, $"{version.Major}.{version.Minor}.{version.Revision}");
        }

        private void FFX2_Click(object sender, RoutedEventArgs e)
        {
            var processSelect = new ProcessSelectWindow("FFX-2");
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();
                var FFX2Editor = new FFX2Editor();
                FFX2Editor.ShowDialog();
                Show();
            }
        }

        private void FFX_Click(object sender, RoutedEventArgs e)
        {
            var processSelect = new ProcessSelectWindow("FFX");
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();
                var FFXEditor = new FFXEditor();
                FFXEditor.ShowDialog();
                Show();
            }
        }
    }
}
