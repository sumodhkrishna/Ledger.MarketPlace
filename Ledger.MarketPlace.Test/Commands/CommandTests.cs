using Ledger.MarketPlace.Commands;
using System;
using Xunit;

namespace Ledger.MarketPlace.Test.Commands
{
    [Collection("Ledger.MarketPlace.Test")]
    public class CommandTests
    {
        [Fact]
        public void CreateLoanTest_NegetiveValuesThrowsException()
        {
            decimal loanAmount = 10000;
            int loanDuarationInYears = 5;
            decimal loanRateOfInterest = 4;

            CreateLoanHandler handler = new CreateLoanHandler();
            Assert.Throws<ArgumentException>(() => handler.Handle(new CreateLoan(TestData.BankNameIDIDI, TestData.CustomerNameDale, -100000, loanDuarationInYears, loanRateOfInterest)));
            Assert.Throws<ArgumentException>(() => handler.Handle(new CreateLoan(TestData.BankNameIDIDI, TestData.CustomerNameDale, loanAmount, -5, loanRateOfInterest)));
            Assert.Throws<ArgumentException>(() => handler.Handle(new CreateLoan(TestData.BankNameIDIDI, TestData.CustomerNameDale, loanAmount, loanDuarationInYears, -10)));
        }

        [Fact]
        public void GetBalance_Success()
        {
            new TestData().SeedLoans();
            GetBalance getBalance = new GetBalance(TestData.BankNameIDIDI, TestData.CustomerNameDaleSmall, 6);
            GetBalanceHandler handler = new GetBalanceHandler();
            var balance =  handler.Handle(getBalance);

            Assert.Equal("IDIDI Dale 2652 6", balance.ToString());
        }

        [Fact]
        public void GetBalance_NegetiveValuesThrowsException()
        {
            GetBalanceHandler handler = new GetBalanceHandler();

            Assert.Throws<ArgumentException>
                (() => handler.Handle(new GetBalance(TestData.BankNameIDIDI, TestData.CustomerNameDale, -10)));
        }

        [Fact]
        public void MakePayment_NegetiveValuesThrowsException()
        {
            MakePaymentHandler handler = new MakePaymentHandler();

            Assert.Throws<ArgumentException>
                (() => handler.Handle(new MakePayment(-1000,5, TestData.CustomerNameDale, TestData.BankNameIDIDI)));
            Assert.Throws<ArgumentException>
                (() => handler.Handle(new MakePayment(1000, -5, TestData.CustomerNameDale, TestData.BankNameIDIDI)));
        }


    }
}
