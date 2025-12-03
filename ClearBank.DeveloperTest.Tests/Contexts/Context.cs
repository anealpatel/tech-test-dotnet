using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Contexts
{
    public class Context
    {
		private Account _account;
		private MakePaymentRequest _makePaymentRequest;
        private MakePaymentResult _makePaymentResult;

        public Account Account
        {
			get { return _account; }
			set { _account = value; }
		}

        public MakePaymentRequest MakePaymentRequest
        {
            get { return _makePaymentRequest; }
            set { _makePaymentRequest = value; }
        }

        public MakePaymentResult MakePaymentResult
        {
            get { return _makePaymentResult; }
            set { _makePaymentResult = value; }
        }
    }
}
