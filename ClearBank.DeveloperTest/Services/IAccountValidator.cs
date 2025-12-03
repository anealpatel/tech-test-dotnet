using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountValidator
    {
        bool ValidateAccount(PaymentScheme paymentScheme, decimal amount, IAccount account);
    }
}