using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using LykkeWalletApp.ApiAccess;
using LykkeWalletApp.Views.Kyc;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Regisrations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegPassword 
    {
        public RegPassword()
        {
            InitializeComponent();
        }

        private bool CheckSubmitEnabled()
        {
            if (PbLoading.Visibility == Visibility.Visible)
                return false;

            if (string.IsNullOrEmpty(Password.Password))
                return false;

            if (string.IsNullOrEmpty(PasswordAgain.Password))
                return false;

            if (Password.Password.Length<6)
                return false;

            return Password.Password == PasswordAgain.Password;
        }

        private void Password_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = CheckSubmitEnabled();


        }

        private async void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {

            PbLoading.Visibility = Visibility.Visible;
            BtnSubmit.IsEnabled = false;
            Password.IsEnabled = false;
            PasswordAgain.IsEnabled = false;

           // await Task.Delay(5000);
            await ServiceLocator.ApiAccess.RegisterAccount(SharedData.RegistrationData.Email, SharedData.RegistrationData.FullName,
               SharedData.RegistrationData.Phone, Password.Password);

            BtnSubmit.IsEnabled = true;
            Password.IsEnabled = true;
            PasswordAgain.IsEnabled = true;
            PbLoading.Visibility = Visibility.Collapsed;

            Frame.Navigate(typeof (MakeSalfieView));
        }
    }
}
