using Ledger.MarketPlace.Commands;
using Ledger.MarketPlace.Repository;

namespace Ledger.MarketPlace.Test
{
    public class TestData
    {
        public const string BankNameIDIDI = "IDIDI";
        public const string CustomerNameDale = "DALE";
        public const string BankNameMBI = "MBI";
        public const string CustomerNameHarry = "HARRY";
        public const string BankNameUON = "UON";
        public const string CustomerNameDaleSmall = "Dale";
        public const string CustomerNameHarrySmall = "Harry";
        public const string CustomerNameShelly = "Shelly";
        public const string CustomerNameSam = "Sam";
        public const string CustomerNameJohn = "John";

        public void SeedLoans()
        {
            SeedLoan(BankNameIDIDI, CustomerNameDale, 10000, 5, 4);
            SeedLoan(BankNameIDIDI, CustomerNameSam, 5000, 1, 6);
            SeedLoan(BankNameMBI, CustomerNameHarry, 2000, 2, 2);
            SeedLoan(BankNameIDIDI, CustomerNameDaleSmall, 5000, 1, 6);
            SeedLoan(BankNameIDIDI, CustomerNameJohn, 5000, 1, 6);
            SeedLoan(BankNameMBI, CustomerNameHarrySmall, 10000, 3, 7);
            SeedLoan(BankNameUON, CustomerNameShelly, 15000, 2, 9);
        }

        private void SeedLoan(string bankName, string customerName, decimal loanAmount, int loanDuarationInYears, decimal loanRateOfInterest)
        {
            CreateLoan createLoan = new CreateLoan(bankName, customerName, loanAmount, loanDuarationInYears, loanRateOfInterest);
            var repo = new BankDataRepository();
            repo.CreateLoan(createLoan);
        }
    }
}