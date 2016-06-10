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
using Farplane.FFX.Data;

namespace Farplane.FFX.EditorPanels.Party
{
    /// <summary>
    /// Interaction logic for PartyStats.xaml
    /// </summary>
    public partial class PartyStats : UserControl
    {
        private const int StatsLength = 0x94;
        private int _statsBase;
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

            var partyMember = Data.Party.ReadPartyMember(_characterIndex);

            _statsBase = Offsets.GetOffset(OffsetType.PartyStatsBase) + StatsLength * _characterIndex;

            var statBytes = Memory.ReadBytes(_statsBase, StatsLength);

            TextTotalAP.Text = partyMember.ApTotal.ToString();
            TextCurrentAP.Text = partyMember.ApCurrent.ToString();

            TextCurrentHP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("CurrentHp")).ToString();
            TextCurrentMP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("CurrentMp")).ToString();
            TextMaxHP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("CurrentHpMax")).ToString();
            TextMaxMP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("CurrentMpMax")).ToString();

            TextBaseHP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("BaseHp")).ToString();
            TextBaseMP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("BaseMp")).ToString();

            TextSphereLevelCurrent.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("SphereLevelCurrent")].ToString();
            TextSphereLevelTotal.Text = BitConverter.ToUInt16(statBytes, StructHelper.GetFieldOffset<PartyMember>("SphereLevelTotal")).ToString();

            var inParty = statBytes[StructHelper.GetFieldOffset<PartyMember>("InParty")];

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

            TextBaseStrength.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseStrength")].ToString();
            TextBaseDefense.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseDefense")].ToString();
            TextBaseMagic.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseMagic")].ToString();
            TextBaseMagicDef.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseMagicDefense")].ToString();
            TextBaseAgility.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseAgility")].ToString();
            TextBaseLuck.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseLuck")].ToString();
            TextBaseEvasion.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseEvasion")].ToString();
            TextBaseAccuracy.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseAccuracy")].ToString();

            _canWriteData = true;
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || _statsBase <= 0) return;

            try
            {
                int offset = 0;
                var textBox = (sender as TextBox);
                switch (textBox.Name)
                {
                    case "TextTotalAP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("ApTotal", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextTotalAP.Text)));
                        break;
                    case "TextCurrentAP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("ApCurrent", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextCurrentAP.Text)));
                        break;
                    case "TextMaxHP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("CurrentHpMax", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextMaxHP.Text)));
                        break;
                    case "TextMaxMP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("CurrentMpMax", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextMaxMP.Text)));
                        break;
                    case "TextCurrentHP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("CurrentHp", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextCurrentHP.Text)));
                        break;
                    case "TextCurrentMP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("CurrentMp", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextCurrentMP.Text)));
                        break;
                    case "TextBaseHP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseHp", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextBaseHP.Text)));
                        break;
                    case "TextBaseMP":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseMp", _statsBase);
                        Memory.WriteBytes(offset, BitConverter.GetBytes(uint.Parse(TextBaseMP.Text)));
                        break;
                    case "TextSphereLevelCurrent":
                        offset = StructHelper.GetFieldOffset<PartyMember>("SphereLevelCurrent", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextSphereLevelCurrent.Text));
                        break;
                    case "TextSphereLevelTotal":
                        offset = StructHelper.GetFieldOffset<PartyMember>("SphereLevelTotal", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextSphereLevelCurrent.Text));
                        break;
                    case "TextBaseStrength":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseStrength", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseStrength.Text));
                        break;
                    case "TextBaseDefense":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseDefense", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseDefense.Text));
                        break;
                    case "TextBaseMagic":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseMagic", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseMagic.Text));
                        break;
                    case "TextBaseMagicDef":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseMagicDefense", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseMagicDef.Text));
                        break;
                    case "TextBaseAgility":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseAgility", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseAgility.Text));
                        break;
                    case "TextBaseLuck":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseLuck", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseLuck.Text));
                        break;
                    case "TextBaseEvasion":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseEvasion", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseEvasion.Text));
                        break;
                    case "TextBaseAccuracy":
                        offset = StructHelper.GetFieldOffset<PartyMember>("BaseAccuracy", _statsBase);
                        Memory.WriteByte(offset, byte.Parse(TextBaseAccuracy.Text));
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

            var offset = StructHelper.GetFieldOffset<PartyMember>("InParty", _statsBase);

            Memory.WriteByte(offset, partyState);

            Refresh(_characterIndex);
        }
    }
}
