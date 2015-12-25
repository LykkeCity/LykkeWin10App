using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using LykkeWalletApp.ApiAccess;
using LykkeWalletApp.Views.Kyc;
using LykkeWalletApp.Views.Main;
using LykkeWalletApp.Views.Pin;

namespace LykkeWalletApp
{
    public static class PageExt
    {

        public static async Task<IUICommand> ShowDialog(this string message)
        {
            var dialog = new MessageDialog(message);

            dialog.Commands.Add(new UICommand("Ok") {Id = 0});
      //      dialog.Commands.Add(new UICommand("No") {Id = 1});

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                // Adding a 3rd command will crash the app when running on Mobile !!!
                dialog.Commands.Add(new UICommand("Maybe later") {Id = 2});
            }

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            return result;
        }


        public static Task Navigate(this Page page, KycRegistrationStatusModel src)
        {
            if (src.KycStatus == KycStatus.Ok)
            {
                if (src.PinIsEntered)
                    page.NavigateToPinAuth();
                else
                    page.NavigateToPinEnter();

                return Task.FromResult(0);
            }

            if (src.KycStatus == KycStatus.NeedToFillData)
                return page.NavigateToUploadDocsDocs();

            if (src.KycStatus == KycStatus.Pending)
                return page.NavigateToKycPending();


            if (src.KycStatus == KycStatus.RestrictedArea)
                return page.NavigateToKycRestrictedArea();


            return Task.FromResult(0);
        }

        public static async Task NavigateToYouAreAboutToPssComplience(this Page page)
        {
            SharedData.PersonalData = await ServiceLocator.ApiAccess.GetPersonalDataAsync();
            page.Frame.Navigate(typeof(YouAreAboutPassComplience));
        }

        public static async Task NavigateToKycPending(this Page page)
        {
            SharedData.PersonalData =  await ServiceLocator.ApiAccess.GetPersonalDataAsync();
            page.Frame.Navigate(typeof (PendingKycView));
        }

        public static async Task NavigateToKycRestrictedArea(this Page page)
        {
            SharedData.PersonalData = await ServiceLocator.ApiAccess.GetPersonalDataAsync();
            page.Frame.Navigate(typeof(RestrictedAreaView));
        }

        public static async Task NavigateToKycOk(this Page page)
        {
            SharedData.PersonalData = await ServiceLocator.ApiAccess.GetPersonalDataAsync();
            page.Frame.Navigate(typeof(KycOkView));
        }

        public static async Task NavigateToUploadDocsDocs(this Page page)
        {
            var docsToUpload = await ServiceLocator.ApiAccess.CheckDocumentsToUploadAsync();

            if (docsToUpload.Selfie)
            {
                page.Frame.Navigate(typeof(MakeSalfieView));
                return;
            }

            if (docsToUpload.IdCard)
            {
                page.Frame.Navigate(typeof(MakeIdCard));
                return;
            }

            if (docsToUpload.ProofOfAddress)
            {
                page.Frame.Navigate(typeof(MakePoa));
                return;
            }

            await NavigateToYouAreAboutToPssComplience(page);
        }


        public static Task HandleException(this Page page, Exception ex)
        {
            var webException = ex as WebException;
            if (((HttpWebResponse) webException?.Response)?.StatusCode == HttpStatusCode.Unauthorized)
            {
                page.Frame.Navigate(typeof(MainPage));
                return Task.FromResult(0);
            }

            return ex.Message.ShowDialog();
        }

        public static Task UploadDocument(this Page page, string docType)
        {
            var stream = SharedData.TheStream.AsStream();
            var memoryStream = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(memoryStream);
            var data = Convert.ToBase64String(memoryStream.ToArray());
            return ServiceLocator.ApiAccess.UploadDocumentAsync(docType, "jpg", data);
        }


        public static void NavigateToPinEnter(this Page page)
        {
            SharedData.PinIsSetUp = false;
            page.Frame.Navigate(typeof(PinEnteringView));
        }


        public static void NavigateToPinAuth(this Page page)
        {
            SharedData.PinIsSetUp = true;
            page.Frame.Navigate(typeof(PinEnteringView));
        }

        public static void NavigateToMainContent(this Page page)
        {
            page.Frame.Navigate(typeof(MainPageView));
        }

    }
}
