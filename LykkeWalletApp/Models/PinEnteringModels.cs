
namespace LykkeWalletApp.Models
{
    public class PinEnteringViewModel : BaseModel
    {

#region PinVisualisation
        private string _color0;
        public string Color0
        {
            get { return _color0; }
            set
            {
                if (_color0 == value)
                    return;
                _color0 = value;
                OnPropertyChanged(nameof(Color0));
            }
        }

        private string _color1;
        public string Color1
        {
            get { return _color1; }
            set
            {
                if (_color1 == value)
                    return;
                _color1 = value;
                OnPropertyChanged(nameof(Color1));
            }
        }


        private string _color2;
        public string Color2
        {
            get { return _color2; }
            set
            {
                if (_color2 == value)
                    return;
                _color2 = value;
                OnPropertyChanged(nameof(Color2));
            }
        }


        private string _color3;
        public string Color3
        {
            get { return _color3; }
            set
            {
                if (_color3 == value)
                    return;
                _color3 = value;
                OnPropertyChanged(nameof(Color3));
            }
        }

        private string _color4;
        public string Color4
        {
            get { return _color4; }
            set
            {
                if (_color4 == value)
                    return;
                _color4 = value;
                OnPropertyChanged(nameof(Color4));
            }
        }

        public static string GrayColor = "#D3D6DB";
        public static string ActiveColor = "#AB00FF";

        private void RefreshData()
        {
            Color0 = _data.Length < 1 ? GrayColor : ActiveColor;
            Color1 = _data.Length < 2 ? GrayColor : ActiveColor;
            Color2 = _data.Length < 3 ? GrayColor : ActiveColor;
            Color3 = _data.Length < 4 ? GrayColor : ActiveColor;
            Color4 = _data.Length < 5 ? GrayColor : ActiveColor;



        }
#endregion PinVisualisation

        public string EnteredPinFirstTime { get; set; }

        public string EnterPinPhrase { get; set; }

        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {

                _data = value;
                RefreshData();
            }
        }


        private bool _pinSecondTimeVisible;
        public bool PinSecondTime
        {
            get
            {
                return _pinSecondTimeVisible;
            }
            set
            {
                if (_pinSecondTimeVisible == value)
                    return;

                _pinSecondTimeVisible = value;
                OnPropertyChanged(nameof(PinSecondTime));

            }
        }


        public void InitForFirstTime()
        {
            Data = "";
            PinSecondTime = false;
        }

        public void InitForSecondTime()
        {
            EnteredPinFirstTime = Data;
            Data = "";
            PinSecondTime = true;
        }

    }
}
