using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Tests.Contexts;
using ClearBank.DeveloperTest.Tests.Support;
using ClearBank.DeveloperTest.Types;
using Moq;

namespace ClearBank.DeveloperTest.Tests.StepDefinitions
{
    [Binding]
    public sealed class PaymentStepDefinitions
    {
        public readonly Context _ctx;

        public PaymentStepDefinitions(Context ctx)
        {
            _ctx = ctx;
            _ctx.Account = new Account();
        }

        [Given("an account status is {AccountStatus}")]
        public void GivenAnAccountStatusIsLive(AccountStatus status)
        {
            _ctx.Account.Status = status;
        }

        [Given("a has a balance of ${decimal}")]
        public void GivenAHasABalanceOf(decimal balance)
        {
            _ctx.Account.Balance = balance;

        }

        [Given("a has a payment scheme of {AllowedPaymentSchemes}")]
        public void GivenAHasAPaymentSchemeOf(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _ctx.Account.AllowedPaymentSchemes = allowedPaymentSchemes;
        }

        [Given("a {PaymentScheme} payment of ${decimal} is requested")]
        public void GivenAChapsPaymentOfIsRequested(PaymentScheme paymentScheme, decimal amount)
        {
            _ctx.MakePaymentRequest = new MakePaymentRequest
            {
                CreditorAccountNumber = "56785678",
                DebtorAccountNumber = "12341234",
                Amount = amount,
                PaymentDate = new DateTime(2025, 12, 2),
                PaymentScheme = paymentScheme
            };
        }

        [When("the payment is made")]
        public void WhenThePaymentIsMade()
        {
            Mock<IAccountDataStore> mockBackupAccountDataStore = new Mock<IAccountDataStore>();
            mockBackupAccountDataStore.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(_ctx.Account);

            Mock<IAccountDataStoreService> mockAccountDataStoreService = new Mock<IAccountDataStoreService>();
            mockAccountDataStoreService.Setup(x => x.GetAccountDataStore()).Returns(mockBackupAccountDataStore.Object);

            IPaymentService sut = new PaymentService(mockBackupAccountDataStore.Object, new AccountValidator());
            _ctx.MakePaymentResult = sut.MakePayment(_ctx.MakePaymentRequest);
        }

        [Then("the result should be successful")]
        public void ThenTheResultShouldBeSuccessful()
        {
            Assert.True(_ctx.MakePaymentResult.Success);
        }

        [Then("the result should be unsuccessful")]
        public void ThenTheResultShouldBeUnsuccessful()
        {
            Assert.False(_ctx.MakePaymentResult.Success);
        }

        [Then("the account balance should be ${float}")]
        public void ThenTheAccountBalanceShouldBe(float result)
        {
            Assert.Equal(Convert.ToDecimal(result), _ctx.Account.Balance);
        }
    }
}
