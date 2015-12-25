using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IdCardConfirmation 
    {
        public IdCardConfirmation()
        {
            InitializeComponent();
            SetImage();
        }

        private void SetImage()
        {
            var imgSrc = new BitmapImage();
            SharedData.TheStream.Seek(0);
            imgSrc.SetSource(SharedData.TheStream);
            CapturePreview.Source = imgSrc;
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            await this.UploadDocument("IdCard");
            Frame.GoBack();
            await this.NavigateToUploadDocsDocs();
        }
    }
}
