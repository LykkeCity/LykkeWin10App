namespace LykkeWalletApp.ApiAccess
{
    public static class KycStatus
    {
        public const string NeedToFillData = "NeedToFillData";
        public const string Pending = "Pending";
        public const string Ok = "Ok";
        public const string RestrictedArea = "RestrictedArea";
    }

    public class KycRegistrationStatusModel
    {
        public string KycStatus { get; set; }
        public bool PinIsEntered { get; set; }
        public string FullName { get; set; }
    }

}
