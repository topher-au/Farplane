using System.Windows;
using System.Windows.Controls;
using Farplane.Common;
using Farplane.FFX.Data;

namespace Farplane.FFX.EditorPanels.SphereGridPanel
{
    /// <summary>
    ///     Interaction logic for SphereGridEditor.xaml
    /// </summary>
    public partial class SphereGridEditor : UserControl
    {
        private bool _refreshing;
        private int _currentNode;

        public SphereGridEditor()
        {
            InitializeComponent();
            ComboNodeType.ItemsSource = SphereGrid.GetNames();
        }

        public void Refresh()
        {
            _refreshing = true;
            RefreshNode();
            _refreshing = false;
        }

        private void RefreshNode()
        {
            var node = SphereGrid.ReadNode(_currentNode);

            TextCurrentNode.Text = $"Currently editing node #{_currentNode}";
            ComboNodeType.SelectedIndex = node.NodeType;

            var activations = BitHelper.GetBitArray(new[] {node.ActivatedBy}, 8);
            for (var i = 0; i < 7; i++)
            {
                var checkBox = PanelNodeActivatedBy.Children[i] as CheckBox;
                if (checkBox != null)
                    checkBox.IsChecked = activations[i];
            }
        }

        private void ComboNodeType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_refreshing) return;

            SphereGrid.SetNodeType(_currentNode, SphereGrid.NodeTypes[ComboNodeType.SelectedIndex].ID);
        }

        private void SphereGridActivation_Changed(object sender, RoutedEventArgs e)
        {
            if (_refreshing) return;
            var senderBox = sender as CheckBox;
            var senderIndex = PanelNodeActivatedBy.Children.IndexOf(senderBox);

            var current = SphereGrid.ReadNode(_currentNode);
            var actCurrent = current.ActivatedBy;
            actCurrent = BitHelper.ToggleBit(actCurrent, senderIndex);
            SphereGrid.SetNodeActivation(_currentNode, actCurrent);
            Refresh();
        }

        private void ButtonSelectNode_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = SphereGrid.GetSelectedNode();
            _currentNode = selectedNode;

            Refresh();
        }
    }
}