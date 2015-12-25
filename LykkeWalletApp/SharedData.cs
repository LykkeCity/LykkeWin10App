using System;
using Windows.Storage.Streams;
using Core;
using LykkeWalletApp.Models;

namespace LykkeWalletApp
{
    public static class SharedData
    {
        public static readonly RegistrationData RegistrationData  = new RegistrationData();

        public static IPersonalData PersonalData;

        public static bool PinIsSetUp;

        private static IRandomAccessStream _theStream;
        public static IRandomAccessStream TheStream
        {
            get { return _theStream; }
            set
            {
                if (_theStream != null)
                {
                    var disp = _theStream as IDisposable;
                    disp?.Dispose();
                }
                _theStream = value;
            }
        }

    }
}
