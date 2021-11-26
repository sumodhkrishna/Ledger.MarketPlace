using Ledger.MarketPlace.Commands;
using Ledger.MarketPlace.Repository;
using System.Linq;
using Xunit;

namespace Ledger.MarketPlace.Test.Repository
{
    [Collection("Ledger.MarketPlace.Test")]
    public class DataRepositoyTests
    {
        private BankDataRepository dataRepository;
        public DataRepositoyTests()
        {
            dataRepository = new BankDataRepository();
        }

        [Theory]
        [InlineData(TestData.BankNameIDIDI, TestData.CustomerNameDale, 10000, 5, 4)]
        [InlineData(TestData.BankNameMBI, TestData.CustomerNameHarrySmall, 2000, 2, 2)]
        [InlineData(TestData.BankNameIDIDI, TestData.CustomerNameDaleSmall, 5000, 1, 6)]
        [InlineData(TestData.BankNameMBI, TestData.CustomerNameHarrySmall, 10000, 3, 7)]
        [InlineData(TestData.BankNameUON, TestData.CustomerNameShelly, 15000, 2, 9)]
        public void CreateLoanTest_Success(string bankName, string customerName, decimal loanAmount, int loanDuarationInYears, decimal loanRateOfInterest)
        {
            CreateLoan createLoan = new CreateLoan(bankName, customerName, loanAmount, loanDuarationInYears, loanRateOfInterest);

            dataRepository.CreateLoan(createLoan);
            var banks = BankData.Instance.Banks;
            var ididiCustomers = (banks.First(b => b.Name.Equals(bankName))).Customers;
            var customer = ididiCustomers.FirstOrDefault(c => c.Name.Equals(customerName));

            Assert.True(banks.Count() > 0);
            Assert.Contains(banks, b => b.Name.Equals(bankName));
            Assert.Contains(ididiCustomers, c => c.Name.Equals(customerName));
            Assert.True(customer.Loan.Amount == loanAmount && customer.Loan.Years == loanDuarationInYears && customer.Loan.RateOfInterest == loanRateOfInterest);
        }
        [Fact]
        public void GetBalanceTest_Success()
        {
            new TestData().SeedLoans();

            var balance1 = dataRepository.GetBalance(5, TestData.BankNameIDIDI, TestData.CustomerNameDale);
            var balance2 = dataRepository.GetBalance(40, TestData.BankNameIDIDI, TestData.CustomerNameDale);
            var balance3 = dataRepository.GetBalance(12, TestData.BankNameMBI, TestData.CustomerNameHarry);
            var balance4 = dataRepository.GetBalance(0, TestData.BankNameMBI, TestData.CustomerNameHarry);

            Assert.True(balance1.PaidAmount == 1000);
            Assert.True(balance1.LeftEmiCount == 55);
            Assert.True(balance2.PaidAmount == 8000);
            Assert.True(balance2.LeftEmiCount == 20);
            Assert.True(balance3.PaidAmount == 1044);
            Assert.True(balance3.LeftEmiCount == 12);
            Assert.True(balance4.PaidAmount == 0);
            Assert.True(balance4.LeftEmiCount == 24);

        }
        [Fact]
        public void MakePaymentTest_Success()
        {
            new TestData().SeedLoans();

            dataRepository.MakePayment(1000, 5, TestData.BankNameIDIDI, TestData.CustomerNameDaleSmall);
            dataRepository.MakePayment(5000, 10, TestData.BankNameMBI, TestData.CustomerNameHarrySmall);
            dataRepository.MakePayment(7000, 12, TestData.BankNameUON, TestData.CustomerNameShelly);

            var daleBal = dataRepository.GetBalance(6, TestData.BankNameIDIDI, TestData.CustomerNameDaleSmall);
            var harryBal = dataRepository.GetBalance(12, TestData.BankNameMBI, TestData.CustomerNameHarrySmall);
            var shellyBal = dataRepository.GetBalance(12, TestData.BankNameUON, TestData.CustomerNameShelly);

            Assert.True(daleBal.PaidAmount == 3652);
            Assert.True(daleBal.LeftEmiCount == 4);
            Assert.True(shellyBal.PaidAmount == 15856);
            Assert.True(shellyBal.LeftEmiCount == 3);
            Assert.True(harryBal.PaidAmount == 9044);
            Assert.True(harryBal.LeftEmiCount == 10);
        }

    }
}
