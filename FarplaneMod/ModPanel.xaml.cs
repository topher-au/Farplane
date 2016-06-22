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
using MahApps.Metro.Controls;

namespace Farplane.FarplaneMod
{
    /// <summary>
    /// Interaction logic for ModPanel.xaml
    /// </summary>
    public partial class ModPanel : UserControl
    {
        private bool _refresh;
        private ModLoader.FarplaneMod[] _mods;

        public ModPanel()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            _refresh = true;

            GridNoMods.Visibility = Visibility.Visible;
            GroupModDetails.Visibility = Visibility.Collapsed;
            
            if (!Settings.Default.EnableMods)
            {
                TextNoMod.Text =
                    "Mods are currently disabled.\n\nEnable mods from the Settings panel\non the game select window.";
                _refresh = false;
                return;
            }

            _mods = ModLoader.GetLoadedMods();

            if (_mods == null)
            {
                TextNoMod.Text = "No mods are currently loaded.";
                _refresh = false;
                return;
            }

            GridNoMods.Visibility = Visibility.Collapsed;
            GroupModDetails.Visibility = Visibility.Visible;

            var selectedMod = ListMods.SelectedIndex;

            ListMods.Items.Clear();
            
            foreach (var mod in _mods)
                ListMods.Items.Add(mod.Mod.Name);

            if (selectedMod < ListMods.Items.Count && selectedMod >= 0)
            {
                ListMods.SelectedIndex = selectedMod;
            }
            else if (ListMods.Items.Count > 0)
            {
                ListMods.SelectedIndex = 0;
            }
            ShowModDetail(ListMods.SelectedIndex);

            _refresh = false;
        }

        public void ShowModDetail(int modIndex)
        {
            if (modIndex == -1)
            {
                // Show nothing
                GridNoMods.Visibility = Visibility.Visible;
                GroupModDetails.Visibility = Visibility.Collapsed;
                return;
            }

            GridNoMods.Visibility = Visibility.Collapsed;
            GroupModDetails.Visibility = Visibility.Visible;
            
            var mod = _mods[modIndex];

            CheckModEnabled.IsChecked = mod.Mod.GetState() == ModState.Activated;

            GroupModDetails.Header = mod.Mod.Name;
            TextAuthor.Text = mod.Mod.Author;
            TextDescription.Text = mod.Mod.Description;

            if (mod.Mod.ConfigButton != null)
            {
                ButtonConfigure.Content = mod.Mod.ConfigButton;
                ButtonConfigure.IsEnabled = true;
            }
            else
            {
                ButtonConfigure.Content = "No Configuration";
                ButtonConfigure.IsEnabled = false;
            }
        }

        private void ListMods_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refresh) return;
            ShowModDetail(ListMods.SelectedIndex);
        }

        private void ButtonConfigure_OnClick(object sender, RoutedEventArgs e)
        {
            if(ListMods.SelectedIndex >= 0 && ListMods.SelectedIndex < _mods.Length)
                _mods[ListMods.SelectedIndex].Mod.Configure(this.TryFindParent<Window>());
        }

        private void CheckModEnabled_OnClick(object sender, RoutedEventArgs e)
        {
            if (_refresh) return;
            if (ListMods.SelectedIndex >= 0 && ListMods.SelectedIndex < _mods.Length)
            {
                var mod = _mods[ListMods.SelectedIndex];
                if(CheckModEnabled.IsChecked.Value)
                    mod.Mod.Activate();
                else
                    mod.Mod.Deactivate();
            }
        }
    }
}