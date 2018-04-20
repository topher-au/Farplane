using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Farplane.FFX.EditorPanels;
using Farplane.FFX.EditorPanels.Aeons;
using Farplane.FFX.EditorPanels.Battle;
using Farplane.FFX.EditorPanels.BlitzballPanel;
using Farplane.FFX.EditorPanels.Debug;
using Farplane.FFX.EditorPanels.Boosters;
using Farplane.FFX.EditorPanels.GeneralPanel;
using Farplane.FFX.EditorPanels.EquipmentPanel;
using Farplane.FFX.EditorPanels.ItemsPanel;
using Farplane.FFX.EditorPanels.MonsterArenaPanel;
using Farplane.FFX.EditorPanels.PartyPanel;
using Farplane.FFX.EditorPanels.SkillEditorPanel;
using Farplane.FFX.EditorPanels.SphereGridPanel;
using Farplane.Memory;
using Farplane.Properties;
using MahApps.Metro.Controls;
using MessageBox = System.Windows.MessageBox;
using ThreadState = System.Threading.ThreadState;
using TreeView = System.Windows.Controls.TreeView;

namespace Farplane.FFX
{
    /// <summary>
    /// Interaction logic for FFXEditor.xaml
    /// </summary>
    public partial class FFXEditor : MetroWindow
    {
        private GeneralPanel _generalPanel;
        private PartyPanel _partyPanel;
        private AeonsPanel _aeonsPanel;
        private ItemsPanel _itemsPanel;
        private SphereGridPanel _sphereGridPanel;
        private EquipmentPanel _equipmentPanel;
        private BlitzballPanel _blitzballPanel;
        private MonsterArenaPanel _monsterArenaPanel;
        private SkillEditorPanel _skillEditorPanel;
        private DebugPanel _debugPanel;
        private BattlePanel _battlePanel;
        private BoostersPanel _boostersPanel;

        private NotAvailablePanel _notAvailablePanel = new NotAvailablePanel();

        private int _defaultHeight = 620;
        private int _defaultWidth = 700;
        private bool _rolledUp = false;
        private bool _windowPinned = false;
        private BitmapImage _iconShrink = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/shrink.png"));
        private BitmapImage _iconExpand = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/expand.png"));

        public FFXEditor(bool fileMode = false)
        {
            InitializeComponent();

            if (fileMode)
            {
                // Set up window for file mode
            }
            else
            {
                // Set up window for process mode
                GameMemory.ProcessExited += Close;

                _generalPanel = new GeneralPanel();
                _partyPanel = new PartyPanel();
                _aeonsPanel = new AeonsPanel();
                _itemsPanel = new ItemsPanel();
                _sphereGridPanel = new SphereGridPanel();
                _equipmentPanel = new EquipmentPanel();
                _blitzballPanel = new BlitzballPanel();
                _monsterArenaPanel = new MonsterArenaPanel();
                _debugPanel = new DebugPanel();
                _battlePanel = new BattlePanel();
                _boostersPanel = new BoostersPanel();
            }

            // Set up general window parameters
            _skillEditorPanel = new SkillEditorPanel();
            
            (EditorTree.Items[0] as TreeViewItem).IsSelected = true;
        }


        private void EditorTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            var treeViewItem = treeView.SelectedItem as TreeViewItem;
            if (treeViewItem == null) return;

            try
            {
                switch (treeViewItem.Name)
                {
                    case "GeneralEditor":
                        _generalPanel.Refresh();
                        EditorContent.Content = _generalPanel;
                        break;
                    case "PartyEditor":
                        _partyPanel.Refresh();
                        EditorContent.Content = _partyPanel;
                        break;
                    case "AeonEditor":
                        _aeonsPanel.Refresh();
                        EditorContent.Content = _aeonsPanel;
                        break;
                    case "ItemEditor":
                        _itemsPanel.Refresh();
                        EditorContent.Content = _itemsPanel;
                        break;
                    case "SphereGridEditor":
                        _sphereGridPanel.Refresh();
                        EditorContent.Content = _sphereGridPanel;
                        break;
                    case "EquipmentEditor":
                        _equipmentPanel.Refresh();
                        EditorContent.Content = _equipmentPanel;
                        break;
                    case "BlitzballEditor":
                        _blitzballPanel.Refresh();
                        EditorContent.Content = _blitzballPanel;
                        break;
                    case "MonsterArenaEditor":
                        _monsterArenaPanel.Refresh();
                        EditorContent.Content = _monsterArenaPanel;
                        break;
                    case "BattleEditor":
                        _battlePanel.Refresh();
                        EditorContent.Content = _battlePanel;
                        break;
                    case "SkillEditor":
                        _skillEditorPanel.Refresh();
                        EditorContent.Content = _skillEditorPanel;
                        break;
                    case "DebugEditor":
                        _debugPanel.Refresh();
                        EditorContent.Content = _debugPanel;
                        break;
                    case "Boosters":
                        _boostersPanel.Refresh();
                        EditorContent.Content = _boostersPanel;
                        break;
                    default: // Panel not implemented
                        EditorContent.Content = _notAvailablePanel;
                        break;
                }
            }
            catch (NullReferenceException ex)
            {
                EditorContent.Content = _notAvailablePanel;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception loading panel:\n{ex.Message}");
            }
            
        }

        public void RefreshAllPanels()
        {
            // Refresh panels here
            _generalPanel?.Refresh();
            _partyPanel?.Refresh();
            _aeonsPanel?.Refresh();
            _itemsPanel?.Refresh();
            _sphereGridPanel?.Refresh();
            _equipmentPanel?.Refresh();
            _blitzballPanel?.Refresh();
            _monsterArenaPanel?.Refresh();
            _battlePanel?.Refresh();
            _debugPanel?.Refresh();
            _boostersPanel?.Refresh();
            _skillEditorPanel?.Refresh();
        }

        private void RefreshAll_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllPanels();
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
                Storyboard expandWindow = (Storyboard) this.Resources["ExpandWindow"];
                this.BeginStoryboard(expandWindow, HandoffBehavior.SnapshotAndReplace);
            }
            else
            {
                Storyboard shrinkWindow = (Storyboard) this.Resources["ShrinkWindow"];
                this.BeginStoryboard(shrinkWindow, HandoffBehavior.SnapshotAndReplace);
            }


            _rolledUp = !_rolledUp;
        }
    }
}