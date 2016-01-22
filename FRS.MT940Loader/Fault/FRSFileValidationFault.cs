namespace FRS.MT940Loader.Fault
{
    public class FRSFileValidationFault
    {
        private int _code;
        private string _faultMessage;

        public int Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
            }
        }

        public string FaultMessage
        {
            get
            {
                return _faultMessage;
            }

            set
            {
                _faultMessage = value;
            }
        }

        public FRSFileValidationFault(int code, string faultMessage)
        {
            _code = code;
            _faultMessage = faultMessage;
        }
    }
}
