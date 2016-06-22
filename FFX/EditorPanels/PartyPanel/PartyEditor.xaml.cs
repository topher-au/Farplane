using System.Windows.Controls;
using Farplane.FFX.Values;

namespace Farplane.FFX.EditorPanels.PartyPanel
{
    /// <summary>
    /// Interaction logic for PartyEditor.xaml
    /// </summary>
    public partial class PartyEditor : UserControl
    {
        private int _character = -1;
        public PartyEditor()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            if (_character == -1) return;

            PartyStats.Refresh(_character);
            PartyAbilities.Refresh(_character);
            PartyOverdrive.Refresh(_character);
        }

        public void Load(Character character)
        {
            _character = (int) character;

            Refresh();
        }
    }
}
