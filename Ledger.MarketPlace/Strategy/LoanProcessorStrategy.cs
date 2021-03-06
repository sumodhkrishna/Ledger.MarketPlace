using Ledger.MarketPlace.Commands;

namespace Ledger.MarketPlace.Strategy
{
    public class LoanProcessorStrategy : ICommandProcessorStrategy
    {
        public void Process(string[] values)
        {
            var bankName = values[1].Trim();
            var customerName = values[2].Trim();
            var amount = decimal.Parse(values[3].Trim());
            var years = int.Parse(values[4].Trim());
            var rateOfInterest = int.Parse(values[5].Trim());
            new CreateLoanHandler().Handle(new CreateLoan(bankName, customerName, amount, years, rateOfInterest));
        }
    }
}
