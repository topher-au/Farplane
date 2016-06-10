using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for DebugOptions.xaml
    /// </summary>
    public partial class DebugOptions : UserControl
    {
        private bool refreshing = false;

        public DebugOptions()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            refreshing = true;

            CheckAllyInvincible.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.AllyInvincible);
            CheckEnemyInvincible.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.EnemyInvincible);
            CheckControlEnemies.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.ControlEnemies);
            CheckControlMonsters.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.ControlMonsters);
            CheckZeroMP.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.MPZero);
            CheckInfoOutput.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.InfoOutput);
            CheckAlwaysCritical.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.AlwaysCritical);
            CheckCritical.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Critical);
            CheckProbability.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Probability100);
            CheckDamageRandom.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.DamageRandom);
            CheckDamage1.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Damage1);
            CheckDamage9999.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Damage9999);
            CheckDamage99999.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Damage99999);
            CheckRareDrop.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.RareDrop100);
            CheckEXP100x.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.EXP100x);
            CheckGil100x.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.Gil100x);
            CheckAlwaysOversoul.IsChecked = Memory.ReadByteFlag((int)Offsets.DebugFlags.AlwaysOversoul);

            var firstAttack = Memory.ReadByte((int)Offsets.DebugFlags.FirstAttack);
            CheckAttackFirst.IsChecked = firstAttack != 0xFF;

            refreshing = false;
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (refreshing) return;

            var checkBox = (CheckBox) sender;
            byte[] checkedBytes = checkBox.IsChecked == true ? new byte[] {1} : new byte[] {0};

            if ((string)checkBox.Tag == "FirstAttack")
                checkedBytes = checkBox.IsChecked == true ? new byte[] {0x00} : new byte[] {0xFF};

            try
            {
                var offset = Enum.Parse(typeof (Offsets.DebugFlags), (string) checkBox.Tag);
                Memory.WriteBytes((int)offset, checkedBytes);
            }
            catch { }
            

        }
    }
}
