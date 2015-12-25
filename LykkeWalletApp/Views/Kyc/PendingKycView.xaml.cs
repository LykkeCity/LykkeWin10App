using System.Threading.Tasks;
using LykkeWalletApp.ApiAccess;
using LykkeWalletApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Kyc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PendingKycView
    {
        public FioModel Model { get; }
        public PendingKycView()
        {
            InitializeComponent();
            Model = new FioModel
            {
                FullName = "Dear, " + SharedData.PersonalData.FullName
            };
            DataContext = this;
            CheckStatusLoop();
        }


        public async void CheckStatusLoop()
        {

            while (true)
            {
                var data = await ServiceLocator.ApiAccess.GetKycStatusAsync();

                if (data.KycStatus == KycStatus.RestrictedArea)
                {
                    await this.NavigateToKycRestrictedArea();
                    break;
                }

                if (data.KycStatus == KycStatus.Ok)
                {
                    await this.NavigateToKycOk();
                    break;
                }


                if (data.KycStatus == KycStatus.NeedToFillData)
                {
                    await this.NavigateToUploadDocsDocs();
                    break;
                }

                await Task.Delay(5000);
            }

        }
    }
}
