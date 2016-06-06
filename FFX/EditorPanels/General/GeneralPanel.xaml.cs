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
using Farplane.Common;

namespace Farplane.FFX.EditorPanels.General
{
    /// <summary>
    /// Interaction logic for GeneralPanel.xaml
    /// </summary>
    public partial class GeneralPanel : UserControl
    {
        private readonly int _offsetGil = Offsets.GetOffset(OffsetType.CurrentGil);

        public GeneralPanel()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            var currentGil = MemoryReader.ReadInt32(_offsetGil);
            TextGil.Text = currentGil.ToString();
        }

        private void TextGil_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            int currentGil;

            try
            {
                currentGil = int.Parse(TextGil.Text);
            }
            catch
            {
                Error.Show("The value you entered was invalid.");
                return;
            }

            MemoryReader.WriteBytes(_offsetGil, BitConverter.GetBytes(currentGil));
        }
    }
}
