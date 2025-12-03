using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Tests.Support
{
    public interface IAccountDataStoreService
    {
        IAccountDataStore GetAccountDataStore();
    }
}
