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
            var statBytes = MemoryReader.ReadBytes(_statsBase, StatsLength);
            var nameBytes = MemoryReader.ReadBytes(_nameBase, 8);

            TextAeonName.Text = StringConverter.ToString(nameBytes);

            TextBaseHP.Text = BitConverter.ToUInt32(statBytes, (int) PartyStatOffset.BaseHp).ToString();
            TextBaseMP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.BaseMp).ToString();

            TextOverdrive.Text = statBytes[(int)PartyStatOffset.OverdriveLevel].ToString();
            TextOverdriveMax.Text = statBytes[(int)PartyStatOffset.OverdriveMax].ToString();

            TextBaseStrength.Text = statBytes[(int)PartyStatOffset.BaseStrength].ToString();
            TextBaseDefense.Text = statBytes[(int)PartyStatOffset.BaseDefense].ToString();
            TextBaseMagic.Text = statBytes[(int)PartyStatOffset.BaseMagic].ToString();
            TextBaseMagicDef.Text = statBytes[(int)PartyStatOffset.BaseMagicDefense].ToString();
            TextBaseAgility.Text = statBytes[(int)PartyStatOffset.BaseAgility].ToString();
            TextBaseLuck.Text = statBytes[(int)PartyStatOffset.BaseLuck].ToString();
            TextBaseEvasion.Text = statBytes[(int)PartyStatOffset.BaseEvasion].ToString();
            TextBaseAccuracy.Text = statBytes[(int)PartyStatOffset.BaseAccuracy].ToString();
        }

        private void ButtonMaxOverdrive_Click(object sender, RoutedEventArgs e)
        {
            var statBytes = MemoryReader.ReadBytes(_statsBase, StatsLength);
            TextOverdrive.Text = statBytes[(int)PartyStatOffset.OverdriveMax].ToString();
            MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.OverdriveLevel, byte.Parse(TextOverdrive.Text));
            Refresh(_characterIndex);
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || _statsBase <= 0) return;

            try
            {
                switch ((sender as TextBox).Name)
                {
                    case "TextAeonName":
                        var nameBytes = StringConverter.ToFFXBytes(TextAeonName.Text);
                        var writeBuffer = new byte[9];
                        nameBytes.CopyTo(writeBuffer,0);
                        MemoryReader.WriteBytes(_nameBase, writeBuffer);
                        AeonsPanel.UpdateTabs();
                        break;
                    case "TextBaseHP":
                        MemoryReader.WriteBytes(_statsBase + (int) PartyStatOffset.BaseHp,
                            BitConverter.GetBytes(uint.Parse(TextBaseHP.Text)));
                        break;
                    case "TextBaseMP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.BaseMp,
                            BitConverter.GetBytes(uint.Parse(TextBaseMP.Text)));
                        break;
                    case "TextOverdrive":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.OverdriveLevel, byte.Parse(TextOverdrive.Text));
                        break;
                    case "TextOverdriveMax":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.OverdriveMax, byte.Parse(TextOverdriveMax.Text));
                        break;
                    case "TextBaseStrength":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseStrength, byte.Parse(TextBaseStrength.Text));
                        break;
                    case "TextBaseDefense":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseDefense, byte.Parse(TextBaseDefense.Text));
                        break;
                    case "TextBaseMagic":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseMagic, byte.Parse(TextBaseMagic.Text));
                        break;
                    case "TextBaseMagicDef":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseMagicDefense, byte.Parse(TextBaseMagicDef.Text));
                        break;
                    case "TextBaseAgility":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseAgility, byte.Parse(TextBaseAgility.Text));
                        break;
                    case "TextBaseLuck":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseLuck, byte.Parse(TextBaseLuck.Text));
                        break;
                    case "TextBaseEvasion":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseEvasion, byte.Parse(TextBaseEvasion.Text));
                        break;
                    case "TextBaseAccuracy":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.BaseAccuracy, byte.Parse(TextBaseAccuracy.Text));
                        break;
                }
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
