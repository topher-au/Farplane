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
using MahApps.Metro.Controls;

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for CreaturePanel.xaml
    /// </summary>
    public partial class CreaturePanel : UserControl
    {
        private CreatureEditor[] _editors = new CreatureEditor[8];
        private readonly int _offsetCreatureName = (int) OffsetType.CreatureNames;

        public delegate void UpdateCreaturesEvent();
        public static event UpdateCreaturesEvent UpdateCreatures;

        public CreaturePanel()
        {
            InitializeComponent();
            for (int i = 0; i < 8; i++)
            {
                _editors[i] = new CreatureEditor(i);
                var tabCreature = new MetroTabItem();
                tabCreature.Name = "Creature" + i;
                tabCreature.Header = "Creature " + i;
                tabCreature.Content = _editors[i];
                ControlsHelper.SetHeaderFontSize(tabCreature, 12);
                TabCreatures.Items.Add(tabCreature);
            }

            TabCreatures.Items.Add(new Button() {Content = "Test"});
            UpdateCreaturesMethod();
            UpdateCreatures += UpdateCreaturesMethod;
        }

        public static void Update()
        {
            UpdateCreatures?.Invoke();
        }

        public void UpdateCreaturesMethod()
        {
            var tabs = TabCreatures.Items.SourceCollection.OfType<TabItem>().ToArray();
            var creatureCount = MemoryReader.ReadByte(0x9FA6C1);

            for (int i = 0; i < 8; i++)
            {
                var creatureTab = tabs[i];
                if (creatureTab == null) continue;
                if (i >= creatureCount)
                {
                    creatureTab.Visibility = Visibility.Collapsed;
                    continue;
                }

                creatureTab.Visibility = Visibility.Visible;

                var nameBytes = MemoryReader.ReadBytes(_offsetCreatureName + (i*40), 18);
                var name = StringConverter.ToString(nameBytes);
                creatureTab.Header = name;
                _editors[i].Refresh();
            }
        }
    }
}
