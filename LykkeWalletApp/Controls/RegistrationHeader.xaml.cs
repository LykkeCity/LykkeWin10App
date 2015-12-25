using System.ComponentModel;
using System.Runtime.CompilerServices;
using LykkeWalletApp.Annotations;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LykkeWalletApp.Controls
{
    public sealed partial class RegistrationHeader: INotifyPropertyChanged
    {
        public RegistrationHeader()
        {
            this.InitializeComponent();
            DataContext = this;
            Header = "Header";
            Description = "Description";

        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
