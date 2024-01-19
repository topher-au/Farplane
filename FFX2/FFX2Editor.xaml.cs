using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Farplane.FFX2.EditorPanels;
using Farplane.FFX2.EditorPanels.Party;
using Farplane.Memory;
using MahApps.Metro.Controls;
using Image = System.Windows.Controls.Image;

namespace Farplane.FFX2
{
    /// <summary>
    /// Interaction logic for FFX2Editor.xaml
    /// </summary>
    public partial class FFX2Editor : MetroWindow
    {
        private General _generalPanel = new General();
        private PartyPanel _partyPanel = new PartyPanel();
        private CreaturePanel _creaturePanel = new CreaturePanel();
        private CreatureTrapping _trappingPanel = new CreatureTrapping();
        private ItemsEditor _itemsPanel = new ItemsEditor();
        private DressphereEditor _dresspheresPanel = new DressphereEditor();
        private AccessoriesEditor _accessoriesPanel = new AccessoriesEditor();
        private GarmentGridEditor _garmentGridsPanel = new GarmentGridEditor();
        private DebugOptions _debugOptionsPanel = new DebugOptions();
        private Randomizer _randomizerPanel = new Randomizer();

        private int _defaultHeight = 540;
        private int _defaultWidth = 640;
        private bool _rolledUp = false;
        private bool _windowPinned = false;
        private BitmapImage _iconShrink = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/shrink.png"));
        private BitmapImage _iconExpand = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/expand.png"));

        public FFX2Editor()
        {
            InitializeComponent();
            GameMemory.ProcessExited += Close;
        }

        private void TreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = (TreeViewItem) e.NewValue;
            switch ((string)item.Name)
            {
                case "GeneralPanel":
                    _generalPanel.Refresh();
                    EditorPanel.Content = _generalPanel;
                    break;
                case "PartyPanel":
                    _partyPanel.Refresh();
                    EditorPanel.Content = _partyPanel;
                    break;
                case "CreaturePanel":
                    _creaturePanel.Refresh();
                    EditorPanel.Content = _creaturePanel;
                    break;
                case "TrappingPanel":
                    _trappingPanel.Refresh();
                    EditorPanel.Content = _trappingPanel;
                    break;
                case "ItemsPanel":
                    _itemsPanel.Refresh();
                    EditorPanel.Content = _itemsPanel;
                    break;
                case "AccessoriesPanel":
                    _accessoriesPanel.Refresh();
                    EditorPanel.Content = _accessoriesPanel;
                    break;
                case "DresspheresPanel":
                    _dresspheresPanel.Refresh();
                    EditorPanel.Content = _dresspheresPanel;
                    break;
                case "GarmentGridsPanel":
                    _garmentGridsPanel.Refresh();
                    EditorPanel.Content = _garmentGridsPanel;
                    break;
                case "DebugOptionsPanel":
                    _debugOptionsPanel.Refresh();
                    EditorPanel.Content = _debugOptionsPanel;
                    break;
                case "RandomizerPanel":
                    _randomizerPanel.Refresh();
                    EditorPanel.Content = _randomizerPanel;
                    break;
                default:
                    break;
            }
        }

        private void RefreshAll()
        {
            _generalPanel.Refresh();
            _partyPanel.Refresh();
            _creaturePanel.Refresh();
            _trappingPanel.Refresh();
            _itemsPanel.Refresh();
            _accessoriesPanel.Refresh();
            _dresspheresPanel.Refresh();
            _debugOptionsPanel.Refresh();
            _randomizerPanel.Refresh();
        }

        private void RefreshAll_Click(object sender, RoutedEventArgs e)
        {
            RefreshAll();
        }

        private void ButtonPin_Click(object sender, RoutedEventArgs e)
        {
            _windowPinned = !_windowPinned;
            ButtonPin.IsChecked = _windowPinned;

            this.Topmost = _windowPinned;
        }

        private void ButtonRollUp_Click(object sender, RoutedEventArgs e)
        {
            if (_rolledUp)
            {
                Left -= _defaultWidth - 210;

                GridContent.Visibility = Visibility.Visible;

                Width = _defaultWidth;
                Height = _defaultHeight;

                ButtonRollUp.Content = new Image() {Source = _iconShrink, Width = 16, Height = 16 };
            }
            else
            {
                Width = 210;
                Left += _defaultWidth - Width;

                Height = 30;
                GridContent.Visibility = Visibility.Hidden;
                ButtonRollUp.Content = new Image() { Source = _iconExpand, Width=16, Height=16 };
            }
            _rolledUp = !_rolledUp;

        }
    }
}
