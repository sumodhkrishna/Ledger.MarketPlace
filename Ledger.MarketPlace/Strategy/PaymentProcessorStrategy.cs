using Ledger.MarketPlace.Commands;

namespace Ledger.MarketPlace.Strategy
{
    public class PaymentProcessorStrategy : ICommandProcessorStrategy
    {
        public void Process(string[] values)
        {
            var bankName = values[1].Trim();
            var customerName = values[2].Trim();
            var amount = decimal.Parse(values[3].Trim());
            var emiNumber = int.Parse(values[4].Trim());
            new MakePaymentHandler()
                .Handle(new MakePayment(amount, emiNumber, customerName, bankName));
        }
    }
}
