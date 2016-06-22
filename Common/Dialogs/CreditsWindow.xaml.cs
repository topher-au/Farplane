using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Farplane.Common.Dialogs
{
    /// <summary>
    /// Interaction logic for CreditsWindow.xaml
    /// </summary>
    public partial class CreditsWindow : MetroWindow
    {
        public CreditsWindow()
        {
            InitializeComponent();

            TextMarquee.Text = string.Empty;

            var creditsText = Properties.Resources.Credits;
            var creditsStream = new StringReader(creditsText);
            var creditsLine = creditsStream.ReadLine();
            
            while (creditsLine != null)
            {
                TextMarquee.Text += creditsLine + '\n';
                creditsLine = creditsStream.ReadLine();
            }
            TextMarquee.InvalidateVisual();
        }


        private void CreditsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -TextMarquee.ActualHeight;
            doubleAnimation.To = GridMain.ActualHeight;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            TextMarquee.BeginAnimation(Canvas.BottomProperty, doubleAnimation);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }

}
