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
using Farplane.FFX.Values;

namespace Farplane.FFX.EditorPanels.Party
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
