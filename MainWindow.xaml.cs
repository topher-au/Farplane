using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Farplane.Properties;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace Farplane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ConfigFlyout _configFlyout = new ConfigFlyout();

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
            var processSelect = new ProcessSelectWindow("FFX-2") {Owner=this};
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();

                var FFX2Editor = new FFX2Editor();
                FFX2Editor.ShowDialog();

                if(Settings.Default.CloseWithGame) Environment.Exit(0);

                Show();
                Topmost = true;
                Topmost = false;
            }
        }

        private void FFX_Click(object sender, RoutedEventArgs e)
        {
            var processSelect = new ProcessSelectWindow("FFX") {Owner=this};
            processSelect.ShowDialog();

            if (processSelect.DialogResult == true)
            {
                Hide();

                var FFXEditor = new FFXEditor();
                FFXEditor.ShowDialog();

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
    }
}
