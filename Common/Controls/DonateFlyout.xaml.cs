using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
using MahApps.Metro.Controls;
using Image = System.Drawing.Image;

namespace Farplane.Common.Controls
{
    /// <summary>
    /// Interaction logic for DonateFlyout.xaml
    /// </summary>
    public partial class DonateFlyout : Flyout
    {
        public DonateFlyout()
        {
            InitializeComponent();
            try
            {
                DonationButton.Source = GetDonationButton();
            }
            catch
            {
                
            }
            
        }

        private ImageSource GetDonationButton()
        {
            var campaignImage = "https://pledgie.com/campaigns/31866.png?skin_name=chrome";
            var imageClient = new WebClient();
            var imageData = imageClient.DownloadData(campaignImage);
            MemoryStream stream = new MemoryStream(imageData);
            stream.Position = 0;
             var bitmap = Bitmap.FromStream(stream);
            bitmap.Save(stream, ImageFormat.Png);
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
            // Force the bitmap to load right now so we can dispose the stream.
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
            
        }

        private void DonationButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://pledgie.com/campaigns/31866");
            IsOpen = false;
        }
    }
}
