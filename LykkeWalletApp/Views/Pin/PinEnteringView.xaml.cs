using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LykkeWalletApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LykkeWalletApp.Views.Pin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PinEnteringView
    {

        public PinEnteringViewModel Model { get; }

        public PinEnteringView()
        {
            InitializeComponent();
            Model = new PinEnteringViewModel
            {
                Data = string.Empty,
                EnterPinPhrase = SharedData.PinIsSetUp ? "Enter your PIN" : "Set up your pin"
            };

            DataContext = this;
            ChangePanelSecondTimeVisibility();
        }

        private void ChangePanelSecondTimeVisibility()
        {
            PanelSecondTime.Visibility = Model.PinSecondTime ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void ButtonPinClick(object sender, RoutedEventArgs e)
        {
            if (PbLoading.Visibility == Visibility.Visible)
                return;

            Model.Data += ((Control) sender).Tag.ToString();

            if (Model.Data.Length >= 5)
            {
                if (SharedData.PinIsSetUp)
                {
                    await AuthPins();
                }
                else
                {
                    if (Model.PinSecondTime)
                        await CheckPins();
                    else
                    {
                        Model.InitForSecondTime();
                        ChangePanelSecondTimeVisibility();
                    }
                }
            }

        }


        private async Task AuthPins()
        {
            try
            {
                PbLoading.Visibility = Visibility.Visible;


                var isPinOk = await ServiceLocator.ApiAccess.CheckPinCodeAsync(Model.Data);
                if (isPinOk)
                    this.NavigateToMainContent();
                else
                    await "Incorrect pin".ShowDialog();

            }
            catch (Exception ex)
            {
                await this.HandleException(ex);
            }
            finally
            {
                Model.InitForFirstTime();
                PbLoading.Visibility = Visibility.Collapsed;
            }
        }


        private async Task CheckPins()
        {

            if (Model.Data != Model.EnteredPinFirstTime)
            {
                await "Pins do not match".ShowDialog();
                Model.InitForFirstTime();
                ChangePanelSecondTimeVisibility();
                return;
            }

            try
            {
                PbLoading.Visibility = Visibility.Visible;

                if (SharedData.PinIsSetUp)
                {
                    var isPinOk = await ServiceLocator.ApiAccess.CheckPinCodeAsync(Model.Data);
                    if (isPinOk)
                        this.NavigateToMainContent();
                    else
                        await "Incorrect pin".ShowDialog();
                }
                else
                {
                    await ServiceLocator.ApiAccess.SetPinCodeAsync(Model.Data);
                    this.NavigateToMainContent();
                }

            }
            catch (Exception ex)
            {
                await this.HandleException(ex);
            }
            finally
            {
                PbLoading.Visibility = Visibility.Collapsed;
            }

        }
    }
}
