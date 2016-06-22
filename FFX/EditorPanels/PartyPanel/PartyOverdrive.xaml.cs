using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Farplane.Common;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;

namespace Farplane.FFX.EditorPanels.PartyPanel
{
    /// <summary>
    /// Interaction logic for PartyOverdrive.xaml
    /// </summary>
    public partial class PartyOverdrive : UserControl
    {
        private int _characterIndex = -1;
        private bool _refreshing = false;
        private readonly int _offsetPartyStats = OffsetScanner.GetOffset(GameOffset.FFX_PartyStatBase);
        private readonly int _sizePartyMember = Marshal.SizeOf<PartyMember>();

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
                var textBox = sender as TextBox;
                if (textBox == null) return;
                var writeOffset = _offsetPartyStats + _characterIndex * _sizePartyMember;
                switch (textBox.Name)
                {
                    case "TextOverdriveCurrent":
                        writeOffset += (int) Marshal.OffsetOf<PartyMember>("OverdriveLevel");
                        GameMemory.Write(writeOffset, byte.Parse(TextOverdriveCurrent.Text), false);
                        TextOverdriveCurrent.SelectAll();
                        break;
                    case "TextOverdriveMax":
                        writeOffset += (int) Marshal.OffsetOf<PartyMember>("OverdriveMax");
                        GameMemory.Write(writeOffset, byte.Parse(TextOverdriveMax.Text), false);
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

            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var panel = checkBox.Parent as DockPanel;
                if (panel == null) return;

                var odIndex = GridOverdrive.Children.IndexOf(panel);

                OverdriveMode.ToggleOverdriveMode(_characterIndex, OverdriveMode.OverdriveModes[odIndex].BitIndex);
            }
            Refresh(_characterIndex);
        }

        public void SetOverdriveCount(object sender)
        {
            if (_refreshing) return;

            var textBox = (sender as TextBox);
            if (textBox != null)
            {
                var panel = textBox.Parent as DockPanel;
                if (panel == null) return;

                var odIndex = GridOverdrive.Children.IndexOf(panel);

                try
                {
                    int odCount = ushort.Parse(textBox.Text);
                    OverdriveMode.SetOverdriveCounter(_characterIndex, OverdriveMode.OverdriveModes[odIndex].BitIndex,
                        odCount);
                }
                catch
                {
                    Error.Show("Please enter a value between 0 and 65535");
                    return;
                }
            }
            
            Refresh(_characterIndex);
            textBox?.SelectAll();
        }

        public void Refresh(int characterIndex)
        {
            _refreshing = true;
            _characterIndex = characterIndex;

            var totalOverdrives = OverdriveMode.OverdriveModes.Length;

            var charOffset = _offsetPartyStats + _characterIndex*_sizePartyMember;

            var offsetLevels = (int) Marshal.OffsetOf<PartyMember>("OverdriveMode") + charOffset;
            var offsetFlags = (int)Marshal.OffsetOf<PartyMember>("OverdriveModes") + charOffset;
            var offsetCounters = (int)Marshal.OffsetOf<PartyMember>("OverdriveWarrior") + charOffset;

            var odLevels = GameMemory.Read<byte>(offsetLevels, 3, false);
            var odBytes = GameMemory.Read<byte>(offsetFlags, 3, false);
            var odCounters = GameMemory.Read<byte>(offsetCounters, totalOverdrives*2, false);
            
            ComboCurrentOverdrive.SelectedIndex = odLevels[0];
            TextOverdriveCurrent.Text = odLevels[1].ToString();
            TextOverdriveMax.Text = odLevels[2].ToString();

            var learnedOverdrives = BitHelper.GetBitArray(odBytes, totalOverdrives);

            for (int i = 0; i < totalOverdrives; i++)
            {
                var dockPanel = GridOverdrive.Children[i] as DockPanel;
                if (dockPanel == null) continue;

                var checkLearned = (dockPanel.Children[0] as CheckBox);
                var textCount = (dockPanel.Children[1] as TextBox);

                if (checkLearned != null)
                    checkLearned.IsChecked = learnedOverdrives[OverdriveMode.OverdriveModes[i].BitIndex];
                if (textCount != null)
                    textCount.Text =
                        BitConverter.ToUInt16(odCounters, OverdriveMode.OverdriveModes[i].BitIndex*2).ToString();
            }


            _refreshing = false;
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            var charOffset = _offsetPartyStats + _characterIndex * _sizePartyMember;
            var levelOffset = (int)Marshal.OffsetOf<PartyMember>("OverdriveLevel") + charOffset;
            var currentMax = GameMemory.Read<byte>(levelOffset, 2, false);
            GameMemory.Write<byte>(levelOffset, currentMax[1], false);
            Refresh(_characterIndex);
        }

        private void ComboCurrentOverdrive_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            var charOffset = _offsetPartyStats + _characterIndex * _sizePartyMember;
            var offset = (int)Marshal.OffsetOf<PartyMember>("OverdriveMode") + charOffset;
            GameMemory.Write(offset, (byte)OverdriveMode.OverdriveModes[ComboCurrentOverdrive.SelectedIndex].BitIndex, false);
        }
    }
}