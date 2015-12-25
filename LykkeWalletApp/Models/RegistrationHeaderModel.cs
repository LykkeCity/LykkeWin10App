
namespace LykkeWalletApp.Models
{
    public class RegistrationHeaderModel : BaseModel
    {

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }


        private string _descrption;
        public string Description
        {
            get { return _descrption; }
            set
            {
                _descrption = value;
                OnPropertyChanged(nameof(Description));
            }
        }

    }
}
