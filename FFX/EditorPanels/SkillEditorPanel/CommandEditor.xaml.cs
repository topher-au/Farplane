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
using Farplane.FFX.Data;

namespace Farplane.FFX.EditorPanels.SkillEditorPanel
{
    /// <summary>
    /// Interaction logic for CommandEditor.xaml
    /// </summary>
    public partial class CommandEditor : UserControl
    {
        public CommandEditor()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            ListCommands.Items.Clear();
            var skillNames = Skills.GetSkillNames();
            foreach (var skill in skillNames)
            {
                ListCommands.Items.Add(skill);
            }
        }

        private void ListCommands_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Description.Text = Skills.GetSkillDescription(ListCommands.SelectedIndex);
        }
    }
}
