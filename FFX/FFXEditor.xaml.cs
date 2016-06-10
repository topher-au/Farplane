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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Farplane.FFX.EditorPanels;
using Farplane.FFX.EditorPanels.Aeons;
using Farplane.FFX.EditorPanels.Battle;
using Farplane.FFX.EditorPanels.BlitzballPanel;
using Farplane.FFX.EditorPanels.Debug;
using Farplane.FFX.EditorPanels.Equipment;
using Farplane.FFX.EditorPanels.Items;
using Farplane.FFX.EditorPanels.Party;
using Farplane.FFX.EditorPanels.Boosters;
using Farplane.FFX.EditorPanels.General;
using Farplane.FFX.EditorPanels.Mods;
using Farplane.FFX.EditorPanels.MonsterArena;
using Farplane.FFX.EditorPanels.SphereGrid;
using Farplane.FarplaneMod;
using Farplane.Properties;
using MahApps.Metro.Controls;
using ThreadState = System.Threading.ThreadState;
using TreeView = System.Windows.Controls.TreeView;

namespace Farplane.FFX
{
    /// <summary>
    /// Interaction logic for FFXEditor.xaml
    /// </summary>
    public partial class FFXEditor : MetroWindow
    {
        private GeneralPanel _generalPanel = new GeneralPanel();
        private PartyPanel _partyPanel = new PartyPanel();
        private AeonsPanel _aeonsPanel = new AeonsPanel();
        private ItemsPanel _itemsPanel = new ItemsPanel();
        private SphereGridPanel _sphereGridPanel = new SphereGridPanel();
        private EquipmentPanel _equipmentPanel = new EquipmentPanel();
        private BlitzballPanel _blitzballPanel = new BlitzballPanel();
        private MonsterArenaPanel _monsterArenaPanel = new MonsterArenaPanel();
        private DebugPanel _debugPanel = new DebugPanel();
        private BattlePanel _battlePanel = new BattlePanel();
        private BoostersPanel _boostersPanel = new BoostersPanel();
        private ModsPanel _modsPanel = new ModsPanel();

        private NotImplementedPanel _notImplementedPanel = new NotImplementedPanel();

        private int _defaultHeight = 540;
        private int _defaultWidth = 640;
        private bool _rolledUp = false;
        private bool _windowPinned = false;
        private BitmapImage _iconShrink = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/shrink.png"));
        private BitmapImage _iconExpand = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/expand.png"));

        private readonly Thread _gameThread;
        private bool _processMods = false;
            
        public FFXEditor()
        {
            InitializeComponent();

            _gameThread = new Thread(ProcessChecker) { IsBackground = true };
            _gameThread.Start();

            if (Settings.Default.EnableMods)
            {
                ModLoader.LoadAllMods(GameType.FFX);
                ModUpdateThread.Start();
                ModUpdateThread.ProcessMods = true;
            }

            (EditorTree.Items[0] as TreeViewItem).IsSelected = true;
        }

        public void CloseEditor()
        {
            if (Settings.Default.EnableMods)
            {
                ModUpdateThread.ProcessMods = false;
                ModUpdateThread.Stop();
                ModLoader.UnloadAllMods();
            }
            try
            {
                Close(); 
                
            } catch { }
        }

        public void ProcessChecker()
        {
            while (_gameThread.ThreadState == ThreadState.Background)
            {
                if (!Memory.CheckProcess())
                {
                    Dispatcher.Invoke(CloseEditor);
                    return;
                }
                Thread.Sleep(100);
            }
        }


        private void EditorTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            var treeViewItem = treeView.SelectedItem as TreeViewItem;
            if (treeViewItem == null) return;

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
                case "DebugEditor":
                    _debugPanel.Refresh();
                    EditorContent.Content = _debugPanel;
                    break;
                case "Boosters":
                    _boostersPanel.Refresh();
                    EditorContent.Content = _boostersPanel;
                    break;
                case "Mods":
                    _modsPanel.Refresh();
                    EditorContent.Content = _modsPanel;
                    break;
                default: // Panel not implemented
                    EditorContent.Content = _notImplementedPanel;
                    break;
            }
        }

        public void RefreshAllPanels()
        {
            // Refresh panels here
            _generalPanel.Refresh();
            _partyPanel.Refresh();
            _aeonsPanel.Refresh();
            _itemsPanel.Refresh();
            _sphereGridPanel.Refresh();
            _equipmentPanel.Refresh();
            _blitzballPanel.Refresh();
            _monsterArenaPanel.Refresh();
            _battlePanel.Refresh();
            _debugPanel.Refresh();
            _boostersPanel.Refresh();
            _modsPanel.Refresh();
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
                Left -= _defaultWidth - 210;

                GridContent.Visibility = Visibility.Visible;

                Width = _defaultWidth;
                Height = _defaultHeight;

                ButtonRollUp.Content = new Image() { Source = _iconShrink, Width = 16, Height = 16 };
            }
            else
            {
                Width = 210;
                Left += _defaultWidth - Width;

                Height = 30;
                GridContent.Visibility = Visibility.Hidden;
                ButtonRollUp.Content = new Image() { Source = _iconExpand, Width = 16, Height = 16 };
            }
            _rolledUp = !_rolledUp;

        }

        private void FFXEditor_OnClosing(object sender, CancelEventArgs e)
        {
            CloseEditor();
        }
    }
}
