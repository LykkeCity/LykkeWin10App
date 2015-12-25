using LykkeWalletApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestrictedAreaView 
    {
        public FioModel Model { get; }
        public RestrictedAreaView()
        {
            InitializeComponent();
            Model = new FioModel
            {
                FullName = "Dear, " + SharedData.PersonalData.FullName+", we're terribly sorry, but Lykke Wallet can't be created based on the KYC information that you have provided."
            };

            DataContext = this;
        }
    }
}
