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

namespace Farplane.FFX.EditorPanels.Party
{
    /// <summary>
    /// Interaction logic for PartyOverdrive.xaml
    /// </summary>
    public partial class PartyOverdrive : UserControl
    {
        private byte[] _odBytes, _odCounters;
        private readonly int _partyOffset = Offsets.GetOffset(OffsetType.PartyStatsBase);
        private int _characterIndex = -1;
        private bool _refreshing = false;

        public PartyOverdrive()
        {
            InitializeComponent();

            var gridRows = OverdriveMode.OverdriveModes.Length/3;
            if (OverdriveMode.OverdriveModes.Length%3 != 0) gridRows++;

            for (var i = 0; i < gridRows; i++)
                GridOverdrive.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(30)});

            for (int i = 0; i < OverdriveMode.OverdriveModes.Length; i++)
            {
                var overdriveCheckBox = new CheckBox()
                {
                    Content = OverdriveMode.OverdriveModes[i].Name,
                    VerticalAlignment = VerticalAlignment.Center
                };
                overdriveCheckBox.Checked += (sender, args) => ToggleOverdrive(sender);
                overdriveCheckBox.Unchecked += (sender, args) => ToggleOverdrive(sender);
                var overdriveTextBox = new TextBox()
                {
                    Text = "999",
                    Width = 50,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    MaxLength = 5
                };
                overdriveTextBox.KeyDown += OverdriveTextBox_KeyDown;
                var overdrivePanel = new DockPanel()
                {
                    Children =
                    {
                        overdriveCheckBox,
                        overdriveTextBox
                    },
                    Width = 140,
                    Height = 26,
                    Margin = new Thickness(2)
                };
                Grid.SetColumn(overdrivePanel, i/gridRows);
                Grid.SetRow(overdrivePanel, i%gridRows);
                GridOverdrive.Children.Add(overdrivePanel);
            }

            ComboCurrentOverdrive.ItemsSource = OverdriveMode.OverdriveModes;
        }

        private void OverdriveTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || _refreshing) return;

            try
            {
                switch ((sender as TextBox).Name)
                {
                    case "TextOverdriveCurrent":
                        MemoryReader.WriteByte(
                            _partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveLevel,
                            byte.Parse(TextOverdriveCurrent.Text));
                        TextOverdriveCurrent.SelectAll();
                        break;
                    case "TextOverdriveMax":
                        MemoryReader.WriteByte(
                            _partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveMax,
                            byte.Parse(TextOverdriveMax.Text));
                        TextOverdriveMax.SelectAll();
                        break;
                    default:
                        SetOverdriveCount(sender);
                        break;
                }
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 255.");
            }
            
        }

        public void ToggleOverdrive(object sender)
        {
            if (_refreshing) return;

            var panel = (sender as CheckBox).Parent as DockPanel;
            if (panel == null) return;

            var odIndex = GridOverdrive.Children.IndexOf(panel);

            OverdriveMode.ToggleOverdriveMode(_characterIndex, OverdriveMode.OverdriveModes[odIndex].BitIndex);
            Refresh(_characterIndex);
        }

        public void SetOverdriveCount(object sender)
        {
            if (_refreshing) return;

            var textBox = (sender as TextBox);
            var panel = textBox.Parent as DockPanel;
            if (panel == null) return;

            var odIndex = GridOverdrive.Children.IndexOf(panel);
            var odCount = 0;

            try
            {
                odCount = ushort.Parse(textBox.Text);
                OverdriveMode.SetOverdriveCounter(_characterIndex, OverdriveMode.OverdriveModes[odIndex].BitIndex, odCount);
            }
            catch
            {
                Error.Show("Please enter a value between 0 and 65535");
                return;
            }

            
            Refresh(_characterIndex);
            textBox.SelectAll();
        }

        public void Refresh(int characterIndex)
        {
            _refreshing = true;
            _characterIndex = characterIndex;

            var overdriveInfo =
                MemoryReader.ReadBytes(_partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveMode, 3);

            ComboCurrentOverdrive.SelectedIndex = overdriveInfo[0];
            TextOverdriveCurrent.Text = overdriveInfo[1].ToString();
            TextOverdriveMax.Text = overdriveInfo[2].ToString();

            var totalOverdrives = GridOverdrive.Children.Count;

            _odBytes =
                MemoryReader.ReadBytes(_partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveModes, 0x3);
            _odCounters =
                MemoryReader.ReadBytes(_partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveWarrior,
                    totalOverdrives*2);

            var learnedOverdrives = BitHelper.GetBitArray(_odBytes, totalOverdrives);

            for (int i = 0; i < totalOverdrives; i++)
            {
                var checkLearned = ((GridOverdrive.Children[i] as DockPanel).Children[0] as CheckBox);
                var textCount = ((GridOverdrive.Children[i] as DockPanel).Children[1] as TextBox);

                checkLearned.IsChecked = learnedOverdrives[OverdriveMode.OverdriveModes[i].BitIndex];
                textCount.Text =
                    BitConverter.ToUInt16(_odCounters, OverdriveMode.OverdriveModes[i].BitIndex*2).ToString();
            }


            _refreshing = false;
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            var currentMax = MemoryReader.ReadByte(
                            _partyOffset + (_characterIndex * 0x94) + (int)PartyStatOffset.OverdriveMax);
            MemoryReader.WriteByte(
                            _partyOffset + (_characterIndex * 0x94) + (int)PartyStatOffset.OverdriveLevel,
                            currentMax);
            Refresh(_characterIndex);
        }

        private void ComboCurrentOverdrive_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            MemoryReader.WriteByte(_partyOffset + (_characterIndex*0x94) + (int) PartyStatOffset.OverdriveMode,
                (byte) OverdriveMode.OverdriveModes[ComboCurrentOverdrive.SelectedIndex].BitIndex);
        }
    }
}