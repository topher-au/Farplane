using System;
using System.Collections.Generic;
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
using Farplane.FFX.EditorPanels.Equipment;
using MahApps.Metro.Controls;
using TreeView = System.Windows.Controls.TreeView;

namespace Farplane.FFX
{
    /// <summary>
    /// Interaction logic for FFXEditor.xaml
    /// </summary>
    public partial class FFXEditor : MetroWindow
    {
        private EquipmentPanel _equipmentPanel = new EquipmentPanel();
        private NotImplementedPanel _notImplementedPanel = new NotImplementedPanel();
            
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
                        DialogResult = true;
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
                case "EquipmentEditor":
                    _equipmentPanel.Refresh();
                    EditorContent.Content = _equipmentPanel;
                    break;
                default: // Panel not implemented
                    EditorContent.Content = _notImplementedPanel;
                    break;
            }
        }

        public void RefreshAllPanels()
        {
            // Refresh panels here
            _equipmentPanel.Refresh();
        }
    }
}
