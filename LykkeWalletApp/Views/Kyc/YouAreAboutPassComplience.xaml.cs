using Windows.UI.Xaml;
using LykkeWalletApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YouAreAboutPassComplience
    {
        public FioModel Model { get; }

        public YouAreAboutPassComplience()
        {
            InitializeComponent();

            Model = new FioModel
            {
                FullName = "Dear, "+SharedData.PersonalData.FullName
            };

            DataContext = this;
        }

        private async void ButtonOkClick(object sender, RoutedEventArgs e)
        {
            await ServiceLocator.ApiAccess.StartCheckingDocumentsAsync();
            await this.NavigateToKycPending();
        }

    }
}
