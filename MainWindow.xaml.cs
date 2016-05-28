using System;
using System.Collections.Generic;
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
            Title = string.Format(Title, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
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
            MessageBox.Show("Final Fantasy X is not currently implemented.");
        }
    }
}
