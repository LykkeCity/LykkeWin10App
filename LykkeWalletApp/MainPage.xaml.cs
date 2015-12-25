using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Common;
using RegFullName = LykkeWalletApp.Views.Regisrations.RegFullName;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LykkeWalletApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(SharedData.RegistrationData.Email))
            {
                Email.Text = SharedData.RegistrationData.Email;
                CheckEmail();
            }
        }


        private string _lastRequest;
        private bool _emailIsRegistered;

        private async Task CheckEmail()
        {

            if (PbLoading.Visibility == Visibility.Visible)
                return;

            if (_lastRequest == Email.Text)
                return;

            SignInUp.IsEnabled = false;
            EdtPswd.Visibility = Visibility.Collapsed;

            PbLoading.Visibility = Visibility.Visible;
            var currentRequestEmail = Email.Text;
            _lastRequest = currentRequestEmail;
            var response = await ServiceLocator.ApiAccess.AccountExistAsync(currentRequestEmail);
            PbLoading.Visibility = Visibility.Collapsed;
            if (currentRequestEmail != Email.Text && Email.Text.IsValidEmail())
            {
                await CheckEmail();
                return;
            }

            if (Email.Text.IsValidEmail())
            {
                _emailIsRegistered = response.IsEmailRegistered;

                EdtPswd.Visibility = _emailIsRegistered ? Visibility.Visible : Visibility.Collapsed;
                SignInUp.Content = response.IsEmailRegistered ? "Sign in" : "Sign up";
                SignInUp.IsEnabled = true;
            }

        }

        private async void StackPanel_KeyUp(object sender, KeyRoutedEventArgs e)
        {

            if (!Email.Text.IsValidEmail())
            {
                SignInUp.IsEnabled = false;
                EdtPswd.Visibility = Visibility.Collapsed;
                return;
            }
            await CheckEmail();

        }

        private async Task SignIn()
        {
            if (string.IsNullOrEmpty(EdtPswd.Password))
                return;

            try
            {
                var result = await ServiceLocator.ApiAccess.AuthAsync(Email.Text, EdtPswd.Password);
                SharedData.RegistrationData.FullName = result.FullName;
                await this.Navigate(result);
            }
            catch (Exception ex)
            {
                await this.HandleException(ex);
            }
        }

        private async void SignInUp_Click(object sender, RoutedEventArgs e)
        {
            SharedData.RegistrationData.Email = Email.Text;

            if (!_emailIsRegistered)
                Frame.Navigate(typeof (RegFullName));
            else
                await SignIn();

        }
    }
}
