using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Regisrations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegFullName 
    {
        public RegFullName()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(SharedData.RegistrationData.FullName))
            {
                EdtFullName.Text = SharedData.RegistrationData.FullName;
                CheckBtnEnabled();
            }
        }

        private void BtnSbmt_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (RegContactPhone));
        }

        private void CheckBtnEnabled()
        {
            BtnSbmt.IsEnabled = !string.IsNullOrEmpty(SharedData.RegistrationData.FullName);

        }

        private void EdtFullName_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            SharedData.RegistrationData.FullName = EdtFullName.Text;
            CheckBtnEnabled();
        }
    }
}
