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
using MahApps.Metro.Controls;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for StatsPanel.xaml
    /// </summary>
    public partial class StatsPanel : UserControl
    {
        public delegate void WriteData(object sender);
        public event WriteData WriteDataEvent;
        private int _partyIndex;
        private int _statsOffset;

        public StatsPanel()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (sender.GetType() != typeof(TextBox)) return;
            var senderTextBox = sender as TextBox;
            if (senderTextBox == null) return;

            try
            {
                switch (senderTextBox.Name)
                {
                    case "TextCurrentExperience":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.CurrentExperience,
                            BitConverter.GetBytes(uint.Parse(senderTextBox.Text)));
                        break;
                    case "ModHP":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.HPModifier,
                            BitConverter.GetBytes(uint.Parse(senderTextBox.Text)));
                        break;
                    case "ModMP":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.MPModifier,
                            BitConverter.GetBytes(uint.Parse(senderTextBox.Text)));
                        break;
                    case "TextStrength":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModStrength,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextDefense":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModDefense,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextMagic":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModMagic,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextMagicDefense":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModMagicDefense,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextAgility":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModAgility,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextAccuracy":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModAccuracy,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextEvasion":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModEvasion,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextLuck":
                        LegacyMemoryReader.WriteBytes(_statsOffset + (int)Offsets.StatOffsets.ModLuck,
                            new byte[] { byte.Parse(senderTextBox.Text) });
                        break;
                    case "TextName":
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred:\n{ex.Message}", "Error updating value", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Refresh(_partyIndex);
        }

        public void Refresh(int partyIndex)
        {
            _statsOffset = (int) OffsetType.PartyStatBase + 0x80*partyIndex;
            var statsBytes = LegacyMemoryReader.ReadBytes(_statsOffset, 0x80);

            TextCurrentExperience.Text = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.CurrentExperience).ToString();

            CurrentHP.Content = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.CurrentHP).ToString();
            MaxHP.Content = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.MaxHP).ToString();
            ModHP.Text = BitConverter.ToUInt32(statsBytes, (int) Offsets.StatOffsets.HPModifier).ToString();

            CurrentMP.Content = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.CurrentMP).ToString();
            MaxMP.Content = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.MaxMP).ToString();
            ModMP.Text = BitConverter.ToUInt32(statsBytes, (int)Offsets.StatOffsets.MPModifier).ToString();

            LabelStrength.Content = statsBytes[(int)Offsets.StatOffsets.Strength].ToString();
            LabelDefense.Content = statsBytes[(int)Offsets.StatOffsets.Defense].ToString();
            LabelMagic.Content = statsBytes[(int)Offsets.StatOffsets.Magic].ToString();
            LabelMagicDefense.Content = statsBytes[(int)Offsets.StatOffsets.MagicDefense].ToString();
            LabelAgility.Content = statsBytes[(int)Offsets.StatOffsets.Agility].ToString();
            LabelAccuracy.Content = statsBytes[(int)Offsets.StatOffsets.Accuracy].ToString();
            LabelEvasion.Content = statsBytes[(int)Offsets.StatOffsets.Evasion].ToString();
            LabelLuck.Content = statsBytes[(int)Offsets.StatOffsets.Luck].ToString();

            TextStrength.Text = statsBytes[(int)Offsets.StatOffsets.ModStrength].ToString();
            TextDefense.Text = statsBytes[(int)Offsets.StatOffsets.ModDefense].ToString();
            TextMagic.Text = statsBytes[(int)Offsets.StatOffsets.ModMagic].ToString();
            TextMagicDefense.Text = statsBytes[(int)Offsets.StatOffsets.ModMagicDefense].ToString();
            TextAgility.Text = statsBytes[(int)Offsets.StatOffsets.ModAgility].ToString();
            TextAccuracy.Text = statsBytes[(int)Offsets.StatOffsets.ModAccuracy].ToString();
            TextEvasion.Text = statsBytes[(int)Offsets.StatOffsets.ModEvasion].ToString();
            TextLuck.Text = statsBytes[(int)Offsets.StatOffsets.ModLuck].ToString();

            
        }
    }
}
