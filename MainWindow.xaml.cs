using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using Farplane.Common.Controls;
using Farplane.Common.Dialogs;
using Farplane.FFX;
using Farplane.FFX2;
using Farplane.Properties;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Farplane.Memory;

namespace Farplane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ConfigFlyout _configFlyout = new ConfigFlyout();
        

        private int _splashCounter = 10;

        public MainWindow()
        {
            var exeVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (exeVersion > new Version(Settings.Default.SettingsVersion))
                Settings.Default.Upgrade();

            Settings.Default.SettingsVersion = exeVersion.ToString();
            Settings.Default.Save();

            try
            {
                // Load app theme and accent
                var currentTheme = ThemeManager.GetAppTheme(Settings.Default.AppTheme);
                var currentAccent = ThemeManager.GetAccent(Settings.Default.AppAccent);

                ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
            }
            catch
            {
                // Theme error, revert to default
                Settings.Default.AppTheme = "BaseLight";
                Settings.Default.AppAccent = "Blue";

                Settings.Default.Save();

                var currentTheme = ThemeManager.GetAppTheme(Settings.Default.AppTheme);
                var currentAccent = ThemeManager.GetAccent(Settings.Default.AppAccent);

                ThemeManager.ChangeAppStyle(Application.Current, currentAccent, currentTheme);
            }
            
            InitializeComponent();
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Title = string.Format(Title, $"{version.Major}.{version.Minor}.{version.Build}");
			
            Flyouts = new FlyoutsControl();
            Flyouts.Items.Add(_configFlyout);
        }

        private void FFX2_Click(object sender, RoutedEventArgs e)
        {
            _configFlyout.IsOpen = false;
            var processSelect = new ProcessSelectWindow("FFX-2") {Owner=this};
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();
                var FFX2Editor = new FFX2Editor();
                FFX2Editor.ShowDialog();
                GameMemory.Detach();
                if(Settings.Default.CloseWithGame) Environment.Exit(0);

                Show();
                Topmost = true;
                Topmost = false;
            }
        }

        private async void FFX_Click(object sender, RoutedEventArgs e)
        {
            _configFlyout.IsOpen = false;
            var processSelect = new ProcessSelectWindow("FFX") {Owner=this};
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();

                if (processSelect.ResultProcess == null)
                {
                    var FFXEditor = new FFXEditor(true);
                    FFXEditor.ShowDialog();
                }
                else
                {
                    var FFXEditor = new FFXEditor();
                    FFXEditor.ShowDialog();
                }
                GameMemory.Detach();
                if (Settings.Default.CloseWithGame) Environment.Exit(0);
                Show();
                Topmost = true;
                Topmost = false;
            }
        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            _configFlyout.IsOpen = !_configFlyout.IsOpen;
        }

        private void SplashLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _splashCounter--;
            if (_splashCounter > 0) return;

            _splashCounter = 10;
            var credits = new CreditsWindow() {Owner=this, ShowInTaskbar = false, WindowStartupLocation = WindowStartupLocation.CenterOwner};
            credits.ShowDialog();
        }
    }
}
