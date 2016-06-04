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
using Farplane.Properties;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace Farplane.Common
{
    /// <summary>
    /// Interaction logic for ConfigFlyout.xaml
    /// </summary>
    public partial class ConfigFlyout : Flyout
    {
        private bool canSetTheme = false;

        public ConfigFlyout()
        {
            InitializeComponent();

            ComboTheme.ItemsSource = ThemeManager.AppThemes;
            ComboAccent.ItemsSource = ThemeManager.Accents;

            var currentTheme = ThemeManager.GetAppTheme(Settings.Default.AppTheme);
            var currentAccent = ThemeManager.GetAccent(Settings.Default.AppAccent);

            ComboTheme.SelectedIndex =
                ThemeManager.AppThemes.ToList().IndexOf(currentTheme);

            ComboAccent.SelectedIndex =
                ThemeManager.Accents.ToList().IndexOf(currentAccent);

            canSetTheme = true;
        }

        private void ComboAccent_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!canSetTheme) return;
            ThemeManager.ChangeAppStyle(Application.Current, (Accent)ComboAccent.SelectedItem, (AppTheme)ComboTheme.SelectedItem);
        }
    }
}
