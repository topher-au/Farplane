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
using Farplane.Memory;
using CheckBox = System.Windows.Controls.CheckBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Farplane.FFX.EditorPanels.Boosters
{
    /// <summary>
    /// Interaction logic for BoostersPanel.xaml
    /// </summary>
    public partial class BoostersPanel
    {
	    private static readonly int _offsetInBattle = Offsets.GetOffset(OffsetType.PartyInBattleFlags);
	    private static readonly int _offsetGainedAp = Offsets.GetOffset(OffsetType.PartyGainedApFlags);
	    private Thread _apThread;
	    private bool _sharedApEnabled;
	    private byte[] _sharedApState = new byte[8];

	    public BoostersPanel()
        {
            InitializeComponent();

	        _apThread = new Thread(SharedAPThread) {IsBackground = true};
	        _apThread.Start();

	        for (int i = 0; i < 8; i++)
	        {
		        ShareBoxes.Children.Add(new CheckBox()
		        {
			        Name = "CheckBoxAPShare" + i,
			        Content = (Character) i,
			        Margin = new Thickness(5),
			        IsChecked = i != 7
		        });
	        }
        }

        public void Refresh()
        {
            // No refresh logic for this panel
        }

        private void GiveAllItems_Click(object sender, RoutedEventArgs e)
        {
            Cheats.GiveAllItems();
        }

        private void MaxAllStats_Click(object sender, RoutedEventArgs e)
        {
            Cheats.MaxAllStats();
        }

        private void MaxSphereLevels_Click(object sender, RoutedEventArgs e)
        {
            Cheats.MaxSphereLevels();
        }

        private void LearnAllAbilities_Click(object sender, RoutedEventArgs e)
        {
            Cheats.LearnAllAbilities();
        }

	    private void SharedAPToggle_Click(object sender, RoutedEventArgs e)
	    {
		    _sharedApEnabled = !_sharedApEnabled;

		    ButtonSharedAP.Content = _sharedApEnabled ? "ENABLED" : "DISABLED";
	    }

	    private void UpdateSharedAPState()
	    {
		    var gainedAp = GameMemory.Read<byte>(_offsetGainedAp, 8);

		    for (int i = 0; i < 8; i++)
		    {
			    var box = (CheckBox) ShareBoxes.Children[i];
			    _sharedApState[i] = box.IsChecked.Value ? (byte) 1 : gainedAp[i];
		    }
	    }
	    private void SharedAPThread()
	    {
		    while (true)
		    {
			    // Shared AP mod
			    if (_sharedApEnabled)
			    {
				    if (!GameMemory.IsAttached) break;
				    try
				    {
					    Dispatcher.Invoke(UpdateSharedAPState);
				    }
				    catch (Exception ex)
				    {
					    // App probably exited, silent exception
				    }

				    var writeBuffer = new byte[8];
				    for (var i = 0; i < 8; i++)
				    {
					    writeBuffer[i] = _sharedApState[i];
				    }

				    GameMemory.Write(_offsetInBattle, writeBuffer);
				    GameMemory.Write(_offsetGainedAp, writeBuffer);
			    }

			    Thread.Sleep(10);
		    }
	    }
    }
}