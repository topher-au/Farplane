using System;
using System.Collections.Generic;
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
using Farplane.FFX.EditorPanels.Debug;
using Farplane.FFX.EditorPanels.Equipment;
using Farplane.FFX.EditorPanels.Items;
using Farplane.FFX.EditorPanels.Party;
using Farplane.FFX.EditorPanels.Boosters;
using Farplane.FFX.EditorPanels.SphereGrid;
using MahApps.Metro.Controls;
using TreeView = System.Windows.Controls.TreeView;

namespace Farplane.FFX
{
    /// <summary>
    /// Interaction logic for FFXEditor.xaml
    /// </summary>
    public partial class FFXEditor : MetroWindow
    {
        private PartyPanel _partyPanel = new PartyPanel();
        private AeonsPanel _aeonsPanel = new AeonsPanel();
        private ItemsPanel _itemsPanel = new ItemsPanel();
        private SphereGridPanel _sphereGridPanel = new SphereGridPanel();
        private EquipmentPanel _equipmentPanel = new EquipmentPanel();
        private DebugPanel _debugPanel = new DebugPanel();
        private BoostersPanel _boostersPanel = new BoostersPanel();

        private NotImplementedPanel _notImplementedPanel = new NotImplementedPanel();

        private int _defaultHeight = 540;
        private int _defaultWidth = 640;
        private bool _rolledUp = false;
        private bool _windowPinned = false;
            
        public FFXEditor()
        {
            InitializeComponent();
            var watcherThread = new Thread(ProcessChecker) { IsBackground = true };
            watcherThread.Start();
        }

        public void ProcessChecker()
        {
            while (true)
            {
                if (!MemoryReader.CheckProcess())
                {
                    Dispatcher.Invoke((MethodInvoker)delegate
                    {
                        Close();
                        
                    });

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
                case "DebugEditor":
                    _debugPanel.Refresh();
                    EditorContent.Content = _debugPanel;
                    break;
                case "Boosters":
                    _boostersPanel.Refresh();
                    EditorContent.Content = _boostersPanel;
                    break;
                default: // Panel not implemented
                    EditorContent.Content = _notImplementedPanel;
                    break;
            }
        }

        public void RefreshAllPanels()
        {
            // Refresh panels here
            _partyPanel.Refresh();
            _aeonsPanel.Refresh();
            _itemsPanel.Refresh();
            _sphereGridPanel.Refresh();
            _equipmentPanel.Refresh();
            _debugPanel.Refresh();
            _boostersPanel.Refresh();
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
                Left -= _defaultWidth - 170;

                GridContent.Visibility = Visibility.Visible;

                Width = _defaultWidth;
                Height = _defaultHeight;
            }
            else
            {
                Width = 170;
                Left += _defaultWidth - Width;
                
                Height = 30;
                GridContent.Visibility = Visibility.Hidden;
            }
            _rolledUp = !_rolledUp;
            
        }
    }
}
