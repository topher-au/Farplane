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
using Farplane.FFX2.EditorPanels;
using MahApps.Metro.Controls;

namespace Farplane.FFX2
{
    /// <summary>
    /// Interaction logic for FFX2Editor.xaml
    /// </summary>
    public partial class FFX2Editor : MetroWindow
    {
        private General mGeneral = new General();
        private PartyEditor mPartyEditorYuna = new PartyEditor(Offsets.StatBases.Yuna, Offsets.AbilityBases.Yuna, "Yuna");
        private PartyEditor mPartyEditorRikku = new PartyEditor(Offsets.StatBases.Rikku, Offsets.AbilityBases.Rikku, "Rikku");
        private PartyEditor mPartyEditorPaine = new PartyEditor(Offsets.StatBases.Paine, Offsets.AbilityBases.Paine, "Paine");
        private CreaturePanel mCreaturePanel = new CreaturePanel();
        private CreatureTrapping mCreatureTrapping = new CreatureTrapping();
        private ItemsEditor mItemsEditor = new ItemsEditor();
        private DressphereEditor mDressphereEditor = new DressphereEditor();
        private AccessoriesEditor mAccessoriesEditor = new AccessoriesEditor();
        private GarmentGridEditor mGarmentGridEditor = new GarmentGridEditor();
        private DebugOptions mDebugOptions = new DebugOptions();

        public FFX2Editor()
        {
            InitializeComponent();
            var watcherThread = new Thread(ProcessChecker) {IsBackground = true};
            watcherThread.Start();
        }

        public void ProcessChecker()
        {
            while (true)
            {
                if (!MemoryReader.CheckProcess())
                {
                    Dispatcher.Invoke((MethodInvoker) delegate
                    {
                        DialogResult = true;
                        Close();
                    });
                    
                    return;
                }
                Thread.Sleep(100);
            }
        }

        private void TreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = (TreeViewItem) e.NewValue;
            switch ((string)item.Tag)
            {
                case "General":
                    mGeneral.Refresh();
                    EditorPanel.Content = mGeneral;
                    break;
                case "PartyEditorYuna":
                    mPartyEditorYuna.Refresh();
                    EditorPanel.Content = mPartyEditorYuna;
                    break;
                case "PartyEditorRikku":
                    mPartyEditorRikku.Refresh();
                    EditorPanel.Content = mPartyEditorRikku;
                    break;
                case "PartyEditorPaine":
                    mPartyEditorPaine.Refresh();
                    EditorPanel.Content = mPartyEditorPaine;
                    break;
                case "CreaturePanel":
                    CreaturePanel.Update();
                    EditorPanel.Content = mCreaturePanel;
                    break;
                case "CreatureTrapping":
                    mCreatureTrapping.Refresh();
                    EditorPanel.Content = mCreatureTrapping;
                    break;
                case "ItemEditor":
                    mItemsEditor.Refresh();
                    EditorPanel.Content = mItemsEditor;
                    break;
                case "AccessoriesEditor":
                    mAccessoriesEditor.Refresh();
                    EditorPanel.Content = mAccessoriesEditor;
                    break;
                case "DressphereEditor":
                    mDressphereEditor.Refresh();
                    EditorPanel.Content = mDressphereEditor;
                    break;
                case "GarmentGridEditor":
                    mGarmentGridEditor.Refresh();
                    EditorPanel.Content = mGarmentGridEditor;
                    break;
                case "DebugOptions":
                    mDebugOptions.Refresh();
                    EditorPanel.Content = mDebugOptions;
                    break;

                default:
                    break;
            }
        }

        private void RefreshAll()
        {
            mGeneral.Refresh();
            mPartyEditorYuna.Refresh();
            mPartyEditorRikku.Refresh();
            mPartyEditorPaine.Refresh();
            CreaturePanel.Update();
            mCreatureTrapping.Refresh();
            mItemsEditor.Refresh();
            mAccessoriesEditor.Refresh();
            mDressphereEditor.Refresh();
            mDebugOptions.Refresh();
        }

        private void RefreshAll_Click(object sender, RoutedEventArgs e)
        {
            RefreshAll();
        }
    }
}
