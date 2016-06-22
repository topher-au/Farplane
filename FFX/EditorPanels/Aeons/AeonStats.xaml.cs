using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Farplane.FFX.Data;
using Farplane.Memory;

namespace Farplane.FFX.EditorPanels.Aeons
{
    /// <summary>
    /// Interaction logic for PartyStats.xaml
    /// </summary>
    public partial class AeonStats : UserControl
    {
        private int _characterIndex;

        public AeonStats()
        {
            InitializeComponent();
        }

        public void Refresh(int characterIndex)
        {
            _characterIndex = characterIndex;
            var partyMember = Party.ReadPartyMember(_characterIndex);

            TextAeonName.Text = AeonName.GetName(characterIndex);

            TextBaseHP.Text = partyMember.BaseHp.ToString();
            TextBaseMP.Text = partyMember.BaseMp.ToString();

            TextOverdrive.Text = partyMember.OverdriveLevel.ToString();
            TextOverdriveMax.Text = partyMember.OverdriveMax.ToString();

            TextBaseStrength.Text = partyMember.BaseStrength.ToString();
            TextBaseDefense.Text = partyMember.BaseDefense.ToString();
            TextBaseMagic.Text = partyMember.BaseMagic.ToString();
            TextBaseMagicDef.Text = partyMember.BaseMagicDefense.ToString();
            TextBaseAgility.Text = partyMember.BaseAgility.ToString();
            TextBaseLuck.Text = partyMember.BaseLuck.ToString();
            TextBaseEvasion.Text = partyMember.BaseEvasion.ToString();
            TextBaseAccuracy.Text = partyMember.BaseAccuracy.ToString();
        }

        private void ButtonMaxOverdrive_Click(object sender, RoutedEventArgs e)
        {
            var partyMember = Party.ReadPartyMember(_characterIndex);
            TextOverdrive.Text = partyMember.OverdriveMax.ToString();
            Party.SetPartyMemberAttribute(_characterIndex, "OverdriveLevel", partyMember.OverdriveMax);
            Refresh(_characterIndex);
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            try
            {
                int offset = 0;
                var textBox = (sender as TextBox);
                switch (textBox.Name)
                {

                    case "TextOverdrive":
                        Party.SetPartyMemberAttribute(_characterIndex, "OverdriveLevel", byte.Parse(TextOverdrive.Text));
                        break;
                    case "TextOverdriveMax":
                        Party.SetPartyMemberAttribute(_characterIndex, "OverdriveMax", byte.Parse(TextOverdriveMax.Text));
                        break;
                    case "TextAeonName":
                        AeonName.SetName(_characterIndex, TextAeonName.Text);
                        break;
                    case "TextBaseHP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseHp") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, uint.Parse(TextBaseHP.Text), false);
                        break;
                    case "TextBaseMP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMp") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, uint.Parse(TextBaseMP.Text), false);
                        break;
                    case "TextBaseStrength":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseStrength") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseStrength.Text), false);
                        break;
                    case "TextBaseDefense":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseDefense") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseDefense.Text), false);
                        break;
                    case "TextBaseMagic":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMagic") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseMagic.Text), false);
                        break;
                    case "TextBaseMagicDef":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMagicDefense") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseMagicDef.Text), false);
                        break;
                    case "TextBaseAgility":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseAgility") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseAgility.Text), false);
                        break;
                    case "TextBaseLuck":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseLuck") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseLuck.Text), false);
                        break;
                    case "TextBaseEvasion":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseEvasion") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseEvasion.Text), false);
                        break;
                    case "TextBaseAccuracy":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseAccuracy") + Party.GetMemoryOffset(_characterIndex);
                        GameMemory.Write(offset, byte.Parse(TextBaseAccuracy.Text), false);
                        break;
                }
                AeonsPanel.UpdateTabs();
                textBox.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error parsing input", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }
    }
}
