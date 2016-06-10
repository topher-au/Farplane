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
using Farplane.FFX.Values;

namespace Farplane.FFX.EditorPanels.SphereGrid
{
    /// <summary>
    /// Interaction logic for SphereGridEditor.xaml
    /// </summary>
    public partial class SphereGridEditor : UserControl
    {
        private int currentNode = 0;
        private bool _refreshing = false;

        public SphereGridEditor()
        {
            InitializeComponent();
            ComboNodeType.ItemsSource = SphereGridNode.GetNames();
        }

        public void Refresh()
        {
            _refreshing = true;
            refreshNode();
            _refreshing = false;
        }

        private void refreshNode()
        {
            var node = SphereGridNode.ReadNode(currentNode);

            TextCurrentNode.Text = $"Currently editing node #{currentNode}";
            ComboNodeType.SelectedIndex = node.Type.ID;
            
            var activations = BitHelper.GetBitArray(new byte[] { node.ActivatedBy }, 7);
            for (int i = 0; i < 7; i++)
            {
                (PanelNodeActivatedBy.Children[i] as CheckBox).IsChecked = activations[i];
            }
        }

        private void ComboNodeType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;
            var byteOffset = Offsets.GetOffset(OffsetType.SphereGridNodes) + currentNode*40 + NodeOffset.NodeType;

            Memory.WriteByte((int) byteOffset, (byte) SphereGridNode.NodeTypes[ComboNodeType.SelectedIndex].ID);

        }

        private void SphereGridActivation_Changed(object sender, RoutedEventArgs e)
        {
            if (_refreshing) return;
            var senderBox = sender as CheckBox;
            var senderIndex = PanelNodeActivatedBy.Children.IndexOf(senderBox);

            var byteOffset = Offsets.GetOffset(OffsetType.SphereGridNodes) + currentNode * 40 + NodeOffset.ActivatedBy;
            var actBytes = Memory.ReadByte((int) byteOffset);

            actBytes = BitHelper.ToggleBit(actBytes, senderIndex);

            Memory.WriteByte((int)byteOffset, actBytes);
            Refresh();
        }

        private void ButtonSelectNode_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = BitConverter.ToUInt16(Memory.ReadBytes(Offsets.GetOffset(OffsetType.SphereGridCursor), 2),0);
            currentNode = selectedNode;
            
            Refresh();
        }
    }
}
