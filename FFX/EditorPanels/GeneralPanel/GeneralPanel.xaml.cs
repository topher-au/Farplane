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
using Farplane.Common;
using Farplane.FFX.Values;
using Farplane.Memory;

namespace Farplane.FFX.EditorPanels.GeneralPanel
{
    /// <summary>
    /// Interaction logic for GeneralPanel.xaml
    /// </summary>
    public partial class GeneralPanel : UserControl
    {
        
        private readonly Character[] _enumValues = (Character[])Enum.GetValues(typeof(Character));
        private bool _refreshing = false;

        public GeneralPanel()
        {
            InitializeComponent();

            _refreshing = true;
            var characterList = Enum.GetNames(typeof(Character));
            foreach (ComboBox comboBox in StackCurrentParty.Children)
            {
                comboBox.ItemsSource = characterList;
            }
            _refreshing = false;
        }

        public void Refresh()
        {
            _refreshing = true;
            
            TextGil.Text = Data.General.CurrentGil.ToString();
            TextTidusOverdrive.Text = Data.General.TidusOverdrive.ToString();


            var partyList = Data.Party.GetActiveParty();
            for (int i = 0; i < 8; i++)
            {
                var comboBox = StackCurrentParty.Children[i] as ComboBox;
                comboBox.SelectedIndex = Array.IndexOf(_enumValues, partyList[i]);
            }

            _refreshing = false;
        }

        private void TextGil_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            try
            {
                var currentGil = int.Parse(TextGil.Text);
                Data.General.CurrentGil = currentGil;
                TextGil.SelectAll();
            }
            catch
            {
                Error.Show("The value you entered was invalid.");
            }
        }

        private void TextTidusOverdrive_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            try
            {
                var tidusOverdrive = int.Parse(TextTidusOverdrive.Text);
                Data.General.TidusOverdrive = tidusOverdrive;
                TextTidusOverdrive.SelectAll();
            }
            catch
            {
                Error.Show("The value you entered was invalid.");
            }
        }

        private void PartyMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            var charArray = new Character[8];
            for (int i = 0; i < 8; i++)
            {
                var comboBox = StackCurrentParty.Children[i] as ComboBox;
                charArray[i] = _enumValues[comboBox.SelectedIndex];
            }
            Data.Party.SetActiveParty(charArray);
        }
    }
}
