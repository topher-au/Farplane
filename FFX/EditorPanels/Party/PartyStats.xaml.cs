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

        public PartyStats()
        {
            InitializeComponent();
        }

        public void Refresh(int characterIndex)
        {
            _characterIndex = characterIndex;
            _statsBase = Offsets.GetOffset(OffsetType.PartyStatsBase) + StatsLength * _characterIndex;

            var statBytes = MemoryReader.ReadBytes(_statsBase, StatsLength);

            TextTotalAP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.ApTotal).ToString();
            TextCurrentAP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.ApCurrent).ToString();

            TextCurrentHP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.CurrentHp).ToString();
            TextCurrentMP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.CurrentMp).ToString();
            TextMaxHP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.MaxHp).ToString();
            TextMaxMP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.MaxMp).ToString();

            TextBaseHP.Text = BitConverter.ToUInt32(statBytes, (int) PartyStatOffset.BaseHp).ToString();
            TextBaseMP.Text = BitConverter.ToUInt32(statBytes, (int)PartyStatOffset.BaseMp).ToString();

            TextSphereLevelCurrent.Text = statBytes[(int)PartyStatOffset.SphereLevelCurrent].ToString();
            TextSphereLevelTotal.Text = BitConverter.ToUInt16(statBytes, (int)PartyStatOffset.SphereLevelTotal).ToString();

            TextBaseStrength.Text = statBytes[(int)PartyStatOffset.BaseStrength].ToString();
            TextBaseDefense.Text = statBytes[(int)PartyStatOffset.BaseDefense].ToString();
            TextBaseMagic.Text = statBytes[(int)PartyStatOffset.BaseMagic].ToString();
            TextBaseMagicDef.Text = statBytes[(int)PartyStatOffset.BaseMagicDefense].ToString();
            TextBaseAgility.Text = statBytes[(int)PartyStatOffset.BaseAgility].ToString();
            TextBaseLuck.Text = statBytes[(int)PartyStatOffset.BaseLuck].ToString();
            TextBaseEvasion.Text = statBytes[(int)PartyStatOffset.BaseEvasion].ToString();
            TextBaseAccuracy.Text = statBytes[(int)PartyStatOffset.BaseAccuracy].ToString();
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || _statsBase <= 0) return;

            try
            {
                switch ((sender as TextBox).Name)
                {
                    case "TextTotalAP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.ApTotal,
                            BitConverter.GetBytes(uint.Parse(TextTotalAP.Text)));
                        break;
                    case "TextCurrentAP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.ApCurrent,
                            BitConverter.GetBytes(uint.Parse(TextCurrentAP.Text)));
                        break;
                    case "TextMaxHP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.MaxHp,
                            BitConverter.GetBytes(uint.Parse(TextMaxHP.Text)));
                        break;
                    case "TextMaxMP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.MaxMp,
                            BitConverter.GetBytes(uint.Parse(TextMaxMP.Text)));
                        break;
                    case "TextCurrentHP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.CurrentHp,
                            BitConverter.GetBytes(uint.Parse(TextCurrentHP.Text)));
                        break;
                    case "TextCurrentMP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.CurrentMp,
                            BitConverter.GetBytes(uint.Parse(TextCurrentMP.Text)));
                        break;
                    case "TextBaseHP":
                        MemoryReader.WriteBytes(_statsBase + (int) PartyStatOffset.BaseHp,
                            BitConverter.GetBytes(uint.Parse(TextBaseHP.Text)));
                        break;
                    case "TextBaseMP":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.BaseMp,
                            BitConverter.GetBytes(uint.Parse(TextBaseMP.Text)));
                        break;
                    case "TextSphereLevelCurrent":
                        MemoryReader.WriteByte(_statsBase + (int)PartyStatOffset.SphereLevelCurrent, byte.Parse(TextSphereLevelCurrent.Text));
                        break;
                    case "TextSphereLevelTotal":
                        MemoryReader.WriteBytes(_statsBase + (int)PartyStatOffset.SphereLevelTotal,
                            BitConverter.GetBytes(ushort.Parse(TextSphereLevelTotal.Text)));
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
