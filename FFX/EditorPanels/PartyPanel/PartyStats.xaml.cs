using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.Memory;

namespace Farplane.FFX.EditorPanels.PartyPanel
{
    /// <summary>
    /// Interaction logic for PartyStats.xaml
    /// </summary>
    public partial class PartyStats : UserControl
    { 

        private readonly int _offsetPartyStats = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);
        private readonly int _blockSize = Marshal.SizeOf<PartyMember>();
        private int _characterIndex;
        private bool _canWriteData;

        public PartyStats()
        {
            InitializeComponent();
        }

        public void Refresh(int characterIndex)
        {
            _canWriteData = false;
            
            _characterIndex = characterIndex;

            var partyMember = Party.ReadPartyMember(_characterIndex);

            TextTotalAP.Text = partyMember.ApTotal.ToString();
            TextCurrentAP.Text = partyMember.ApCurrent.ToString();

            TextCurrentHP.Text = partyMember.CurrentHp.ToString();
            TextCurrentMP.Text = partyMember.CurrentMp.ToString();
            TextMaxHP.Text = partyMember.CurrentHpMax.ToString();
            TextMaxMP.Text = partyMember.CurrentMpMax.ToString();

            TextBaseHP.Text = partyMember.BaseHp.ToString();
            TextBaseMP.Text = partyMember.BaseMp.ToString();

            TextSphereLevelCurrent.Text = partyMember.SphereLevelCurrent.ToString();
            TextSphereLevelTotal.Text = partyMember.SphereLevelTotal.ToString();

            var inParty = partyMember.InParty;

            var partyComboIndex = 0;

            switch (inParty)
            {
                case 0:
                    partyComboIndex = 2;
                    break;
                case 16:
                    partyComboIndex = 1;
                    break;
                case 17:
                    partyComboIndex = 0;
                    break;
            }

            ComboInParty.SelectedIndex = partyComboIndex;

            TextBaseStrength.Text = partyMember.BaseStrength.ToString();
            TextBaseDefense.Text = partyMember.BaseDefense.ToString();
            TextBaseMagic.Text = partyMember.BaseMagic.ToString();
            TextBaseMagicDef.Text = partyMember.BaseMagicDefense.ToString();
            TextBaseAgility.Text = partyMember.BaseAgility.ToString();
            TextBaseLuck.Text = partyMember.BaseLuck.ToString();
            TextBaseEvasion.Text = partyMember.BaseEvasion.ToString();
            TextBaseAccuracy.Text = partyMember.BaseAccuracy.ToString();

            _canWriteData = true;
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)return;

            try
            {
                int offset = 0;
                var textBox = (sender as TextBox);
                var charOffset = _offsetPartyStats + _characterIndex*_blockSize;
                switch (textBox.Name)
                {
                    case "TextTotalAP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("ApTotal") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextTotalAP.Text), false);
                        break;
                    case "TextCurrentAP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("ApCurrent") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextCurrentAP.Text), false);
                        break;
                    case "TextMaxHP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("CurrentHpMax") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextMaxHP.Text), false);
                        break;
                    case "TextMaxMP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("CurrentMpMax") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextMaxMP.Text), false);
                        break;
                    case "TextCurrentHP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("CurrentHp") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextCurrentHP.Text), false);
                        break;
                    case "TextCurrentMP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("CurrentMp") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextCurrentMP.Text), false);
                        break;
                    case "TextBaseHP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseHp") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextBaseHP.Text), false);
                        break;
                    case "TextBaseMP":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMp") + charOffset;
                        GameMemory.Write(offset, uint.Parse(TextBaseMP.Text), false);
                        break;
                    case "TextSphereLevelCurrent":
                        offset = (int)Marshal.OffsetOf<PartyMember>("SphereLevelCurrent") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextSphereLevelCurrent.Text), false);
                        break;
                    case "TextSphereLevelTotal":
                        offset = (int)Marshal.OffsetOf<PartyMember>("SphereLevelTotal") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextSphereLevelTotal.Text), false);
                        break;
                    case "TextBaseStrength":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseStrength") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseStrength.Text), false);
                        break;
                    case "TextBaseDefense":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseDefense") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseDefense.Text), false);
                        break;
                    case "TextBaseMagic":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMagic") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseMagic.Text), false);
                        break;
                    case "TextBaseMagicDef":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseMagicDefense") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseMagicDef.Text), false);
                        break;
                    case "TextBaseAgility":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseAgility") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseAgility.Text), false);
                        break;
                    case "TextBaseLuck":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseLuck") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseLuck.Text), false);
                        break;
                    case "TextBaseEvasion":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseEvasion") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseEvasion.Text), false);
                        break;
                    case "TextBaseAccuracy":
                        offset = (int)Marshal.OffsetOf<PartyMember>("BaseAccuracy") + charOffset;
                        GameMemory.Write(offset, byte.Parse(TextBaseAccuracy.Text), false);
                        break;
                }
                textBox.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error parsing input", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }

        private void ComboInParty_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_canWriteData) return;

            byte partyState = 0;

            switch (ComboInParty.SelectedIndex)
            {
                case 0:
                    partyState = 17;
                    break;
                case 1:
                    partyState = 16;
                    break;
                case 2:
                    partyState = 0;
                    break;
            }
            var charOffset = _offsetPartyStats + _characterIndex * _blockSize;
            var offset = (int)Marshal.OffsetOf<PartyMember>("InParty") + charOffset;

            GameMemory.Write(offset, partyState, false);

            Refresh(_characterIndex);
        }
    }
}
