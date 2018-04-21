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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Farplane.FFX2
{
    /// <summary>
    /// Interaction logic for AbilitySlider.xaml
    /// </summary>
    public partial class AbilitySlider : MetroWindow
    {
        public int AP => (int)SliderAP.Value;
        public AbilitySlider(int maxValue, int currentValue)
        {
            InitializeComponent();

            SliderAP.Maximum = currentValue;
            SliderAP.Value = currentValue;
            SliderAP.Maximum = maxValue;
	        LabelAP.Content = $"{(int)SliderAP.Value} / {(int)SliderAP.Maximum}";
        }

        private void SliderAP_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LabelAP.Content = $"{(int)SliderAP.Value} / {(int)SliderAP.Maximum}";
        }

        private void ButtonMaster_OnClick(object sender, RoutedEventArgs e)
        {
            SliderAP.Value = SliderAP.Maximum;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
