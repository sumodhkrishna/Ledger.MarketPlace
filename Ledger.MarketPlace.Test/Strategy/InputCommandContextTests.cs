using Ledger.MarketPlace.Repository;
using Ledger.MarketPlace.Strategy;
using Ledger.MarketPlace.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Ledger.MarketPlace.Test.Strategy
{
    [Collection("Ledger.MarketPlace.Test")]
    public class InputCommandContextTests
    {
        private BankDataRepository repository;
        public InputCommandContextTests()
        {
            repository = new BankDataRepository();
        }

        [Fact]
        public void ProcessPayment_Success()
        {
            new TestData().SeedLoans();
            new InputCommandContext().Process("PAYMENT IDIDI Sam 1000 5");

            var balance = repository.GetBalance(6, TestData.BankNameIDIDI, TestData.CustomerNameSam);

            Assert.True(balance.PaidAmount == 3652);
            Assert.True(balance.LeftEmiCount == 4);
        }

        [Fact]
        public void ProcessLoan_Success()
        {
            new InputCommandContext().Process("LOAN MBI Potter 2000 2 2");

            var balance = repository.GetBalance(12, TestData.BankNameMBI, "Potter");

            Assert.True(balance.PaidAmount == 1044);
            Assert.True(balance.LeftEmiCount == 12);
        }

        [Fact]
        public void ProcessBalance_Success()
        {
            new TestData().SeedLoans();
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            new InputCommandContext().Process("BALANCE IDIDI John 3");

            string actual = stringWriter.ToString();
            Assert.Equal("IDIDI John 1326 9\r\n", actual);
        }
    }
}
