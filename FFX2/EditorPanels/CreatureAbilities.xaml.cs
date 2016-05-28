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
using Farplane.FFX2.Values;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for CreatureAbilities.xaml
    /// </summary>
    public partial class CreatureAbilities : UserControl
    {
        private int _creatureIndex = 0;

        private Button _boxButton;
        private int _boxIndex;

        public CreatureAbilities(int creatureIndex)
        {
            _creatureIndex = creatureIndex;

            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            var creatureBytes = MemoryReader.ReadBytes(Offsets.Creatures.CreatureAbilities + (_creatureIndex * 0xE38), 0x16);

            // Refresh commands
            for (int commandSlot = 0; commandSlot < 8; commandSlot++)
            {
                var commandId = BitConverter.ToUInt16(creatureBytes, commandSlot*2);
                var commandButton = (Button)this.FindName("Command" + commandSlot);
                if (commandButton == null) return;

                if (commandId == 0xFF || commandId == 0x0)
                {
                    commandButton.Content = string.Empty;
                }
                else
                {
                    var command = Commands.GetCommand(commandId);
                    if (command != null)
                    {
                        SetCommandText(commandButton, command);
                    }
                    else
                    {
                        var auto = AutoAbilities.GetAutoAbility(commandId);
                        SetAutoAbilityText(commandButton, auto);
                    }
                    
                }
                    
            }
        }

        private void CommandButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            var senderButton = (Button) sender;
            if (senderButton == null) return;

            var commandIndex = int.Parse(senderButton.Name.Substring(7));

                // ABILITY SEARCH

            var searchList = BuildSearchList();
            var textList = new List<string>();
            foreach(var item in searchList)
                textList.Add($"{item.ID.ToString("X4")} {item.Name}");

            var commandDialog = new CommandSelectDialog(textList);
            var currentCmd = ReadAbility(commandIndex);
            if (currentCmd != 0x0 && currentCmd != 0xFF)
                commandDialog.SearchForCommand(currentCmd.ToString("X2"));

            commandDialog.ShowDialog();
            if (commandDialog.DialogResult.HasValue && commandDialog.DialogResult.Value == false) return;

            var searchIndex = commandDialog.SearchResult;

            if (searchIndex == -1)
            {
                WriteAbility(commandIndex, 0xFF);
            }
            else
            {
                var searchCommand = searchList[searchIndex];
                WriteAbility(commandIndex, searchCommand.ID);
            }
            
        }

        private void WriteAbility(int index, int abilityId)
        {
            MemoryReader.WriteBytes(
                Offsets.Creatures.CreatureAbilities + (_creatureIndex * 0xE38) + (index * 2),
                BitConverter.GetBytes((ushort)abilityId));
            Refresh();
        }

        private ushort ReadAbility(int index)
        {
            var creatureBytes = MemoryReader.ReadBytes(Offsets.Creatures.CreatureAbilities + (_creatureIndex * 0xE38), 0x16);
            var abilityId = BitConverter.ToUInt16(creatureBytes, index*2);
            return abilityId;
        }

        private void ShowButtonBox(Button button, string defaultText)
        {
            Refresh();

            var inputBox = new TextBox() {Text = defaultText, SelectionStart = 0, SelectionLength = defaultText.Length, ContextMenu = null};
            button.Content = inputBox;
            button.UpdateLayout();
            inputBox.Focus();
            inputBox.KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Escape)
                {
                    // Cancel input
                    Refresh();
                    return;
                }

                if (args.Key != Key.Enter) return;

                // Attempt to parse command ID and write ability
                var commandIndex = int.Parse(button.Name.Substring(7));
                var commandId = -1;

                var foundId = int.TryParse(inputBox.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out commandId);

                if (!foundId) return;

                WriteAbility(commandIndex, commandId);
            };
        }

        private void SetAutoAbilityText(Button button, AutoAbility ability)
        {
            if(ability != null)
                button.Content = $"[{ability.ID.ToString("X4")}] {ability.Name}";
            else
                button.Content = $"Unknown ID";
        }

        private void SetCommandText(Button button, Command command)
        {
            if (command != null)
                button.Content = $"[{command.ID.ToString("X4")}] {command.Name}";
            else
                button.Content = $"[{command.ID.ToString("X4")}] ????";
            
        }

        private List<AbilitySearchItem> BuildSearchList()
        {
            var searchList = new List<AbilitySearchItem>();

            foreach (var command in Commands.CommandList)
                searchList.Add(new AbilitySearchItem {ID = command.ID, Name = command.Name, Type=AbilitySearchItem.AbilityType.Command});
            foreach (var command in AutoAbilities.AutoAbilityList)
                searchList.Add(new AbilitySearchItem { ID = command.ID, Name = command.Name, Type = AbilitySearchItem.AbilityType.AutoAbility });

            return searchList;

            //var searchList = new List<string>();
            //foreach (var command in Commands.CommandList)
            //    searchList.Add($"{command.ID.ToString("X2")} {command.Name}");
            //foreach (var command in AutoAbilities.AutoAbilityList)
            //    searchList.Add($"{command.ID.ToString("X2")} {command.Name}");
        }

        private void CommandButton_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            var senderButton = sender as Button;
            if (senderButton == null) return;

            var buttonIndex = int.Parse(senderButton.Name.Substring(7));
            var abilityId =
                MemoryReader.ReadInt16(Offsets.Creatures.CreatureAbilities + (0xE38*_creatureIndex) + (2*buttonIndex));
            ShowButtonBox(senderButton, abilityId.ToString("X2"));
        }

        class AbilitySearchItem
        {
            public AbilityType Type { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }

            public enum AbilityType
            {
                Command,
                AutoAbility
            }
        }
    }
}
