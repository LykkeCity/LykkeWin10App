using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MakePoa : Page
    {
        public MakePoa()
        {
            this.InitializeComponent();
            StartPreview();
        }

        private readonly MediaCapture _mediaCapture = new MediaCapture();
        private async void StartPreview()
        {
            await _mediaCapture.InitializeAsync();
            CapturePreview.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
        }

        private async void BtnPickFileClick(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();

            if (file == null)
                return;

            var stream = await file.OpenStreamForReadAsync();
            SharedData.TheStream = stream.AsRandomAccessStream();
            Frame.Navigate(typeof(IdCardConfirmation));
        }

        private async void BtnPhotoTheDoc(object sender, RoutedEventArgs e)
        {

            SharedData.TheStream = new InMemoryRandomAccessStream();

            await _mediaCapture.StopPreviewAsync();
            await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), SharedData.TheStream);

            Frame.Navigate(typeof(PoaConfirmation));
        }
    }
}
