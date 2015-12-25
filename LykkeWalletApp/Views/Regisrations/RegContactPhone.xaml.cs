using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Regisrations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegContactPhone
    {
        public RegContactPhone()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(SharedData.RegistrationData.Phone))
            {
                EdtContactPhone.Text = SharedData.RegistrationData.Phone;
                CheckEnabled();
            }

        }

        private void BtnSbmt_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegPassword));
        }

        private void CheckEnabled()
        {
            BtnSbmt.IsEnabled = !string.IsNullOrEmpty(SharedData.RegistrationData.Phone);
        }

        private void EdtContactPhone_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
             SharedData.RegistrationData.Phone = EdtContactPhone.Text;

            CheckEnabled();
        }
    }
}
