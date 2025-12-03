using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountValidator _accountValidator;
        private readonly IAccountDataStore _accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore, IAccountValidator accountValidator)
        {
            _accountDataStore = accountDataStore;
            _accountValidator = accountValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            IAccount account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            
            var result = new MakePaymentResult();
            result.Success = _accountValidator.ValidateAccount(request.PaymentScheme, request.Amount, account);

            if (result.Success)
            {
                account.Balance -= request.Amount;

                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }


    }
}
