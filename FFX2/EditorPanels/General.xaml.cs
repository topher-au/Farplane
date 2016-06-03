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

namespace Farplane.FFX2.EditorPanels
{
    /// <summary>
    /// Interaction logic for PartyEditor.xaml
    /// </summary>
    public partial class General : UserControl
    {
        private readonly int _offsetCurrentGil = (int) OffsetType.CurrentGil;
        public General()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            NumGil.Text = MemoryReader.ReadUInt32(_offsetCurrentGil).ToString();
        }


        private void NumGil_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var gil = 0;
            var parsed = int.TryParse(NumGil.Text, out gil);
            if (!parsed)
            {
                // error parsing gil
                MessageBox.Show("The value you entered was invalid.", "Error parsing gil", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            MemoryReader.WriteBytes(_offsetCurrentGil, BitConverter.GetBytes((uint)gil));
        }
    }
}
