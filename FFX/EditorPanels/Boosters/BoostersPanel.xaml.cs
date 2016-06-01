using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Farplane.FFX.Values;
using CheckBox = System.Windows.Controls.CheckBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Farplane.FFX.EditorPanels.Boosters
{
    /// <summary>
    /// Interaction logic for BoostersPanel.xaml
    /// </summary>
    public partial class BoostersPanel : UserControl
    {
        private static bool _sharedApEnabled = false;
        private byte[] _sharedApState = new byte[8];
        private readonly Thread _memoryModThread;
        private static readonly int _offsetInBattle = Offsets.GetOffset(OffsetType.PartyInBattleFlags);
        private static readonly int _offsetGainedAp = Offsets.GetOffset(OffsetType.PartyGainedApFlags);

        public BoostersPanel()
        {
            InitializeComponent();
            _memoryModThread = new Thread(MemoryModThread) { IsBackground = true };
            if(!_memoryModThread.IsAlive)
                _memoryModThread.Start();

            for (int i = 0; i < 8; i++)
            {
                    ShareBoxes.Children.Add(new CheckBox()
                    {
                        Name = "CheckBoxAPShare" + i,
                        Content = (Characters) i,
                        Margin = new Thickness(5),
                        IsChecked=i != 7
                    });
            }
                
        }

        private void MemoryModThread()
        {
            while (true)
            {
                if (_sharedApEnabled)
                {
                    if (!MemoryReader.CheckProcess()) break;
                    Dispatcher.Invoke(UpdateSharedAPState);

                    var writeBuffer = new byte[8];
                    for (var i = 0; i < 8; i++)
                    {
                        writeBuffer[i] = _sharedApState[i];
                    }
                        

                    MemoryReader.WriteBytes(_offsetInBattle, writeBuffer);
                    MemoryReader.WriteBytes(_offsetGainedAp, writeBuffer);
                }
                Thread.Sleep(100);
            }
        }

        private void UpdateSharedAPState()
        {
            var gainedAp = MemoryReader.ReadBytes(_offsetGainedAp, 8);

            for (int i = 0; i < 8; i++)
            {
                var box = (CheckBox) ShareBoxes.Children[i];
                _sharedApState[i] = box.IsChecked.Value ? (byte)1 : gainedAp[i];
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
