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

namespace Farplane.FFX.EditorPanels.Aeons
{
    /// <summary>
    /// Interaction logic for PartyStats.xaml
    /// </summary>
    public partial class AeonStats : UserControl
    {
        private const int StatsLength = 0x94;
        private int _statsBase;
        private int _nameBase;
        private int _characterIndex;

        public AeonStats()
        {
            InitializeComponent();
        }

        public void Refresh(int characterIndex)
        {
            _characterIndex = characterIndex;
            _statsBase = Offsets.GetOffset(OffsetType.PartyStatsBase) + StatsLength * _characterIndex;
            _nameBase = Offsets.GetOffset(OffsetType.AeonNames) + Offsets.AeonNames[_characterIndex-8];
            var statBytes = Memory.ReadBytes(_statsBase, StatsLength);
            var nameBytes = Memory.ReadBytes(_nameBase, 8);

            TextAeonName.Text = StringConverter.ToString(nameBytes);

            TextBaseHP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("BaseHp")).ToString();
            TextBaseMP.Text = BitConverter.ToUInt32(statBytes, StructHelper.GetFieldOffset<PartyMember>("BaseMp")).ToString();

            TextOverdrive.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("OverdriveLevel")].ToString();
            TextOverdriveMax.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("OverdriveMax")].ToString();

            TextBaseStrength.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseStrength")].ToString();
            TextBaseDefense.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseDefense")].ToString();
            TextBaseMagic.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseMagic")].ToString();
            TextBaseMagicDef.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseMagicDefense")].ToString();
            TextBaseAgility.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseAgility")].ToString();
            TextBaseLuck.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseLuck")].ToString();
            TextBaseEvasion.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseEvasion")].ToString();
            TextBaseAccuracy.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("BaseAccuracy")].ToString();
        }

        private void ButtonMaxOverdrive_Click(object sender, RoutedEventArgs e)
        {
            var statBytes = Memory.ReadBytes(_statsBase, StatsLength);
            TextOverdrive.Text = statBytes[StructHelper.GetFieldOffset<PartyMember>("OverdriveMax")].ToString();
            Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("OverdriveLevel"), byte.Parse(TextOverdrive.Text));
            Refresh(_characterIndex);
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || _statsBase <= 0) return;

            try
            {
                var textBox = (sender as TextBox);
                switch (textBox.Name)
                {
                    case "TextAeonName":
                        var nameBytes = StringConverter.ToFFXBytes(TextAeonName.Text);
                        var writeBuffer = new byte[9];
                        nameBytes.CopyTo(writeBuffer,0);
                        Memory.WriteBytes(_nameBase, writeBuffer);
                        AeonsPanel.UpdateTabs();
                        break;
                    case "TextBaseHP":
                        Memory.WriteBytes(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseHp"),
                            BitConverter.GetBytes(uint.Parse(TextBaseHP.Text)));
                        break;
                    case "TextBaseMP":
                        Memory.WriteBytes(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseMp"),
                            BitConverter.GetBytes(uint.Parse(TextBaseMP.Text)));
                        break;
                    case "TextOverdrive":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("OverdriveLevel"), byte.Parse(TextOverdrive.Text));
                        break;
                    case "TextOverdriveMax":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("OverdriveMax"), byte.Parse(TextOverdriveMax.Text));
                        break;
                    case "TextBaseStrength":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseStrength"), byte.Parse(TextBaseStrength.Text));
                        break;
                    case "TextBaseDefense":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseDefense"), byte.Parse(TextBaseDefense.Text));
                        break;
                    case "TextBaseMagic":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseMagic"), byte.Parse(TextBaseMagic.Text));
                        break;
                    case "TextBaseMagicDef":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseMagicDefense"), byte.Parse(TextBaseMagicDef.Text));
                        break;
                    case "TextBaseAgility":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseAgility"), byte.Parse(TextBaseAgility.Text));
                        break;
                    case "TextBaseLuck":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseLuck"), byte.Parse(TextBaseLuck.Text));
                        break;
                    case "TextBaseEvasion":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseEvasion"), byte.Parse(TextBaseEvasion.Text));
                        break;
                    case "TextBaseAccuracy":
                        Memory.WriteByte(_statsBase + StructHelper.GetFieldOffset<PartyMember>("BaseAccuracy"), byte.Parse(TextBaseAccuracy.Text));
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
    }
}
