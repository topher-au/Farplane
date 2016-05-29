using System;
using System.Collections.Generic;
using System.Globalization;
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
using Farplane.FFX2.Values;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for CreatureTrapping.xaml
    /// </summary>
    public partial class CreatureTrapping : UserControl
    {
        private byte[] _trapBytes = new byte[60];
        private byte[] _podBytes = new byte[9];
        private bool _comboShowing = false;
        public CreatureTrapping()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            _trapBytes = MemoryReader.ReadBytes(Offsets.Creatures.CreatureTrapBase, 60);

            for (int i = 0; i < 15; i++)
            {
                var button = (Button)FindName("Trap" + i);
                if (button == null) continue;

                var creatureInTrap = GetTrap(i);
                if (creatureInTrap == 0 || creatureInTrap == 0xFF)
                {
                    button.Content = "Click to Set Trap";
                    continue;
                }
                var creature = Creatures.CreatureList.FirstOrDefault(c => c.ID == creatureInTrap);
                if (creature == null)
                {
                    button.Content = $"[{creatureInTrap.ToString("X4")}] ???? (Unknown ID)";
                }
                else
                {
                    button.Content = $"[{creature.ID.ToString("X4")}] {creature.Name}";
                }
            }

            _podBytes = MemoryReader.ReadBytes(Offsets.Creatures.CreaturePodBase, 9);
            var trapCount = _podBytes[0];
            for (int t = 0; t < 8; t++)
            {
                var trapButton = (Button)FindName("TrapItem" + t);
                if (trapButton == null) continue;

                if (t == trapCount)
                {
                    trapButton.Content = "+";
                    trapButton.Visibility = Visibility.Visible;
                } 
                else if(t > trapCount)
                {
                    trapButton.Visibility = Visibility.Collapsed;
                }
                else if (t < 8)
                {
                    trapButton.Content = GetPodName(t);
                    trapButton.Visibility = Visibility.Visible;
                }
                trapButton.Width = 38;
            }
            _comboShowing = false;
        }

        public string GetPodName(int podIndex)
        {
            switch (_podBytes[podIndex + 1])
            {
                case 0x14:
                    return "S";
                    break;
                case 0x1E:
                    return "M";
                    break;
                case 0x32:
                    return "L";
                    break;
                case 0x50:
                    return "SP";
                    break;
                default:
                    return "?";
                    break;
            }
        }

        public void DeletePod(int podIndex)
        {
            var count = _podBytes[0];
            var outBytes = new byte[9];
            for (int i = 1; i < podIndex +1; i++)
                outBytes[i] = _podBytes[i];
            for (int i = podIndex + 1; i < count; i++)
                if(i+1 < 9) outBytes[i] = _podBytes[i+1];
            count--;
            outBytes[0] = count;
            MemoryReader.WriteBytes(Offsets.Creatures.CreaturePodBase, outBytes);
        }

        private void TrapButton_RightMouse(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var trap = GetTrap(int.Parse(button.Name.Substring(4)));
            ShowButtonBox(button, trap.ToString("X4"));
        }

        private ushort GetTrap(int trapIndex)
        {
            return BitConverter.ToUInt16(_trapBytes, trapIndex*4);
        }

        private void SetTrap(int trapIndex, int creatureId)
        {
            MemoryReader.WriteBytes(Offsets.Creatures.CreatureTrapBase + (trapIndex*4),
                BitConverter.GetBytes((ushort) creatureId));
            Refresh();
        }

        private void ShowButtonBox(Button button, string defaultText)
        {
            Refresh();
            var trapId = int.Parse(button.Name.Substring(4));
            var inputBox = new TextBox();
            inputBox.Text = defaultText;
            inputBox.ContextMenu = null;
            button.Content = inputBox;
            button.KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Escape)
                {
                    Refresh();
                    return;
                }
                if (args.Key != Key.Enter) return;

                int newId = 0;
                var parsed = int.TryParse(inputBox.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                    out newId);
                if (parsed)
                {
                    SetTrap(trapId, newId);
                }
                else
                {
                    MessageBox.Show("Please enter a creature ID in hex!");
                    return;
                }
                
            };
            button.UpdateLayout();
            inputBox.SelectionStart = 0;
            inputBox.SelectionLength = inputBox.Text.Length;
            inputBox.Focus();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void TrapButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var trapId = int.Parse(button.Name.Substring(4));

            

            var creatureSearchList = new List<string>();
            foreach (var creature in Values.Creatures.CreatureList)
                creatureSearchList.Add($"{creature.ID.ToString("X4")} {creature.Name}");
            
            var creatureSearch = new SearchDialog(creatureSearchList);
            creatureSearch.ShowDialog();

            if (creatureSearch.DialogResult == false) return;

            var selectedCreature = Creatures.CreatureList[creatureSearch.ResultIndex];

            SetTrap(trapId, selectedCreature.ID);
        }

        private void TrapItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || _comboShowing) return;
            var itemIndex = int.Parse(button.Name.Substring(8));
            if (itemIndex == _podBytes[0])
            {
                ShowTrapCombo(button);
                return;
            }
            DeletePod(itemIndex);
            Refresh();
        }

        private void ShowTrapCombo(Button button)
        {
            _comboShowing = true;
            var trapCombo = new ComboBox
            {
                ItemsSource = new string[]
                {
                    "S",
                    "M",
                    "L",
                    "SP"
                },
                Width = 48,
                SelectedIndex = 0
            };

            trapCombo.KeyDown += (sender, args) =>
            {
                switch (args.Key)
                {
                    case Key.Enter:
                        if (trapCombo.SelectedIndex >= 0)
                        {
                            int trapID = 0;
                            switch (trapCombo.SelectedIndex)
                            {
                                case 0:
                                    trapID = 20;
                                    break;
                                case 1:
                                    trapID = 30;
                                    break;
                                case 2:
                                    trapID = 50;
                                    break;
                                case 3:
                                    trapID = 80;
                                    break;
                            }
                            _podBytes[_podBytes[0] + 1] = (byte)trapID;
                            _podBytes[0]++;
                            MemoryReader.WriteBytes(Offsets.Creatures.CreaturePodBase, _podBytes);
                        }
                        Refresh();
                        var nextButtonIndex = TrapPanel.Children.IndexOf(button) + 1;

                        if (nextButtonIndex < TrapPanel.Children.Count)
                        {
                            var nb = TrapPanel.Children[nextButtonIndex];
                            nb?.Focus();
                        }
                        else
                        {
                            button.Focus();
                        }
                        
                        return;
                    case Key.Escape:
                        Refresh();
                        break;
                    default:
                        break;
                }
            };

            button.Content = trapCombo;
            button.Width = 60;
            button.UpdateLayout();
            trapCombo.Focus();
        }
    }
}
