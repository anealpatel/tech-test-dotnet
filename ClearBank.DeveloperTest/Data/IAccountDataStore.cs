using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public interface IAccountDataStore
    {
        IAccount GetAccount(string accountNumber);
        void UpdateAccount(IAccount account);
    }
}