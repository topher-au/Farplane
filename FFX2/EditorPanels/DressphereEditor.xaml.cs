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
    /// Interaction logic for DressphereEditor.xaml
    /// </summary>
    public partial class DressphereEditor : UserControl
    {
        private readonly int _offsetDresspheres = (int) OffsetType.DressphereCountBase;

        public DressphereEditor()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            var dressBytes = Memory.ReadBytes(_offsetDresspheres, 30);
            for(int d=1; d<=29; d++)
            {
                var dressBox = (TextBox)FindName("Dressphere" + d);
                if (dressBox == null) continue;

                dressBox.Text = dressBytes[d].ToString();
            }
        }

        private void DressBox_KeyDown(object sender, KeyEventArgs e)
        {
            var dressBox = (TextBox)sender;
            if (dressBox == null) return;
            switch (e.Key)
            {
                case Key.Enter:
                    var dressIndex = int.Parse(dressBox.Name.Substring(10));
                    var quantity = 0;
                    var parsed = int.TryParse(dressBox.Text, out quantity);
                    if (parsed && quantity <= 127 && quantity >= 0)
                    {
                        Memory.WriteBytes(_offsetDresspheres + dressIndex,
                            new byte[] {(byte) quantity});
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a number between 0 and 127.", "Value input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case Key.Escape:
                    Refresh();
                    dressBox.SelectionStart = 0;
                    dressBox.SelectionLength = dressBox.Text.Length;
                    break;
                default:
                    break;
            }
        }
    }
}
