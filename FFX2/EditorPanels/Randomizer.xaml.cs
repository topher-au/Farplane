using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for Randomizer.xaml
    /// </summary>
    public partial class Randomizer : UserControl
    {
        private bool refreshing = false;
        private bool isChecked;
        private CancellationTokenSource cancellationTokenSource;

        public Randomizer()
        {
            InitializeComponent();
            EquippedDressphereRandomizer.InitExp();
        }

        public void Refresh()
        {

        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (refreshing) return;

            var checkBox = (CheckBox) sender;
            if (checkBox.IsChecked == true)
            {
                StartAsyncTask();
                isChecked = true;
            }
            else
            {
                StopAsyncTask();
                isChecked = false;
            }

        }

        private void StopAsyncTask()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;
        }

        private void StartAsyncTask()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Task myTask = Task.Run(async () =>
                {
                    await EquippedDressphereRandomizer.Run(cancellationTokenSource.Token);
                });
            }

            catch (TaskCanceledException)
            {
                Console.WriteLine("test");
            }
        }
    }
}
