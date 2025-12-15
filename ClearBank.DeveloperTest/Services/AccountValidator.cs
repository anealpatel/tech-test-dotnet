using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountValidator : IAccountValidator
    {
        /* 
         * Strictly speaking this code breaks SRP and OCP and can be refactored using a strategy pattern,
         * but I thought that would make much more unreadable given there are only 3 cases. 
         */

        private readonly PaymentSchemeValidatorFactory _factory;

        public AccountValidator(PaymentSchemeValidatorFactory factory) 
        {
            _factory = factory;
        }   
        public bool ValidateAccount(PaymentScheme paymentScheme, decimal amount, IAccount account)
        {
            if (account == null) return false;

            IValidateAccount validator = _factory.GetPaymentSchemeValidator(paymentScheme);
            return validator.Validate(account, amount);
        }
    }

    public interface IValidateAccount
    {
        bool Validate(IAccount account, decimal amount);
    }

    public class PaymentSchemeValidatorFactory
    {
        private readonly Dictionary<PaymentScheme, IValidateAccount> _validators;

        public PaymentSchemeValidatorFactory()
        {
            _validators = new Dictionary<PaymentScheme, IValidateAccount>
            {
                { PaymentScheme.Bacs, new BacsAccountValidator() },
                { PaymentScheme.FasterPayments, new FasterPaymentsAccountValidator() },
                { PaymentScheme.Chaps, new ChapsAccountValidator() }
            };
        }

        public IValidateAccount GetPaymentSchemeValidator(PaymentScheme paymentScheme)
        {
            if (_validators.TryGetValue(paymentScheme, out var validator))
            {
                return validator;
            }
            throw new KeyNotFoundException($"No validator found for payment scheme: {paymentScheme}");
        }

    }

    public class BacsAccountValidator : IValidateAccount
    {
        public bool Validate(IAccount account, decimal amount)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }

    public class FasterPaymentsAccountValidator : IValidateAccount
    {
        public bool Validate(IAccount account, decimal amount)
        {
            return (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && (account.Balance >= amount));
        }
    }

    public class ChapsAccountValidator : IValidateAccount
    {
        public bool Validate(IAccount account, decimal amount)
        {
            return (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && (account.Status == AccountStatus.Live));
        }
    }

}
