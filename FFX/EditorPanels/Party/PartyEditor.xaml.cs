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
        private int _character = 0;
        public PartyEditor()
        {
            InitializeComponent();
        }

        public void Load(Characters character)
        {
            _character = (int) character;

            var statBase = Offsets.GetOffset(OffsetType.PartyStatsBase) + (int) (character)*0x94;

            PartyStats.Refresh(statBase);
        }
    }
}
