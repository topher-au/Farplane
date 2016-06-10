using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Farplane.FarplaneMod;
using Farplane.Properties;

namespace Farplane.FFX.EditorPanels.Mods
{
    /// <summary>
    /// Interaction logic for ModsPanel.xaml
    /// </summary>
    public partial class ModsPanel : UserControl
    {
        private bool _modsEnabled => Settings.Default.EnableMods;
        private bool _refreshing;

        public ModsPanel()
        {
            InitializeComponent();

            if (!_modsEnabled)
            {
                ContentModsDisabled.Visibility = Visibility.Visible;
                ContentModsPanel.Visibility = Visibility.Collapsed;
                return;
            }

            ContentModsDisabled.Visibility = Visibility.Collapsed;
            ContentModsPanel.Visibility = Visibility.Visible;

            Refresh();
        }

        public void Refresh()
        {
            _refreshing = true;
            ListAvailableMods.Items.Clear();
            foreach (var mod in ModLoader.GetLoadedMods())
            {
                ListAvailableMods.Items.Add(mod.Name);
            }

            ListAvailableMods.SelectedIndex = 0;
            RefreshSelectedMod();
            _refreshing = false;
        }

        private void RefreshSelectedMod()
        {
            var modIndex = ListAvailableMods.SelectedIndex;
            if(modIndex == -1) return;

            var mod = ModLoader.GetLoadedMods()[modIndex];

            GroupSelectedMod.Header = mod.Name;
            TextAuthor.Text = mod.Author;
            TextDescription.Text = mod.Description;

            CheckActivated.IsChecked = ModLoader.GetModState(mod);
        }

        private void ListAvailableMods_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            _refreshing = true;
            RefreshSelectedMod();
            _refreshing = false;
        }

        private void CheckActivated_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_refreshing) return;

            var mod = ModLoader.GetLoadedMods()[ListAvailableMods.SelectedIndex];
            
            if(CheckActivated.IsChecked.Value)
                ModLoader.ActivateMod(mod);
            else
                ModLoader.DeactivateMod(mod);

            CheckActivated.IsChecked = mod.Activated;
        }

        private void ButtonOpenMods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mods\\");
        }
    }
}
