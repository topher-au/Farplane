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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farplane.FFX.EditorPanels.Boosters
{
    /// <summary>
    /// Interaction logic for BoostersPanel.xaml
    /// </summary>
    public partial class BoostersPanel : UserControl
    {
        private static bool _sharedApEnabled = false;
        private static readonly Thread _memoryModThread = new Thread(MemoryModThread) {IsBackground = true};
        private static readonly int _offsetInBattle = Offsets.GetOffset(OffsetType.PartyInBattleFlags);
        private static readonly int _offsetGainedAp = Offsets.GetOffset(OffsetType.PartyGainedApFlags);

        public BoostersPanel()
        {
            InitializeComponent();
            if(!_memoryModThread.IsAlive)
                _memoryModThread.Start();
        }

        private static void MemoryModThread()
        {
            while (true)
            {
                if (_sharedApEnabled)
                {
                    if (!MemoryReader.CheckProcess()) break;

                    var writeBuffer = new byte[7];
                    for (var i = 0; i < 7; i++)
                        writeBuffer[i] = 1;

                    MemoryReader.WriteBytes(_offsetInBattle, writeBuffer);
                    MemoryReader.WriteBytes(_offsetGainedAp, writeBuffer);
                }
                Thread.Sleep(100);
            }
        }

        public void Refresh()
        {
            // No refresh logic for this panel
        }

        private void SharedAP_Click(object sender, RoutedEventArgs e)
        {
            _sharedApEnabled = !_sharedApEnabled;

            ButtonSharedAP.Content = _sharedApEnabled ? "ENABLED" : "DISABLED";
        }
    }
}
