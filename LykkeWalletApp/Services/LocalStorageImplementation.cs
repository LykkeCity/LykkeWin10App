using Core;

namespace LykkeWalletApp.Services
{
    public class LocalStorageImplementation : ILocalStorage
    {
        public void Save(string field, string value)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var localSettings = applicationData.LocalSettings;
            localSettings.Values[field] = value;

        }

        public string Get(string field)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var localSettings = applicationData.LocalSettings;
            var value = localSettings.Values[field];
            return value?.ToString();
        }
    }
}
