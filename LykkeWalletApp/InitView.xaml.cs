using System;

namespace LykkeWalletApp
{
    public sealed partial class InitView 
    {

        public InitView()
        {
            InitializeComponent();
            CheckRegistrationStatus();
        }

        private async void CheckRegistrationStatus()
        {
            try
            {
                var status = await ServiceLocator.ApiAccess.CheckRegistrationStatus();
                SharedData.RegistrationData.FullName = status.FullName;
                await this.Navigate(status);
            }
            catch (Exception ex)
            {
                await this.HandleException(ex);
            }
        }

    }
}
