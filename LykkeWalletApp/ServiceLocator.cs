using LykkeWalletApp.ApiAccess;
using LykkeWalletApp.Services;

namespace LykkeWalletApp
{

    public static class ServiceLocator
    {
        public static readonly LocalStorageImplementation LocalStorage = new LocalStorageImplementation();

        private static SrvApiAccess _apiAccessInstance;
        public static SrvApiAccess ApiAccess => _apiAccessInstance ?? (_apiAccessInstance = new SrvApiAccess(LocalStorage));
    }

}
