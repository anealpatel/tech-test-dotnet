using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountValidator : IAccountValidator
    {
        /* 
         * Strictly speaking this code breaks SRP and OCP and can be refactored using a strategy pattern,
         * but I thought that would make much more unreadable given ther are only 3 cases. 
         */
        public bool ValidateAccount(PaymentScheme paymentScheme, decimal amount, IAccount account)
        {
            if (account == null) return false;

            bool isValid = false;

            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);

                case PaymentScheme.FasterPayments:
                    return (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && (account.Balance >= amount));

                case PaymentScheme.Chaps:
                    return (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && (account.Status == AccountStatus.Live));
            }

            return isValid;
        }
    }
}
