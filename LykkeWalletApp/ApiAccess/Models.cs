using Core;

namespace LykkeWalletApp.ApiAccess
{
    public class AccountExistRespoModel
    {
        public bool IsEmailRegistered { get; set; }
    }


    public class RegsiterAccountRespModel
    {
        public string Token { get; set; }
    }

    public class AuthRespModel : KycRegistrationStatusModel
    {
        public string Token { get; set; }
    }


    public class PersonalDataRespModel : IPersonalData
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class DocumentsToUploadRespoModel
    {
        public bool IdCard { get; set; }
        public bool ProofOfAddress { get; set; }
        public bool Selfie { get; set; }
    }

    public class CheckPinSecurityRespModel
    {
        public bool Passed { get; set; }
    }

}
