using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using LykkeWalletApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelfieConfirmaion 
    {



        public SelfieConfirmaion()
        {
            this.InitializeComponent();

            DataContext = this;

            SetImage();

        }

        private void SetImage()
        {
            var imgSrc = new BitmapImage();
            SharedData.TheStream.Seek(0);
            imgSrc.SetSource(SharedData.TheStream);


            CapturePreview.Source = imgSrc;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            await this.UploadDocument("Selfie");
            Frame.GoBack();
            await this.NavigateToUploadDocsDocs();
        }
    }
}
