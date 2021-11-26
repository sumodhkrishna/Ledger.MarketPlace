using Ledger.MarketPlace.Commands;
using System;

namespace Ledger.MarketPlace.Strategy
{
    public class BalanceProcessorStrategy : ICommandProcessorStrategy
    {
        public void Process(string[] values)
        {
            var bankName = values[1].Trim();
            var customerName = values[2].Trim();
            var emiNumber = int.Parse(values[3].Trim());
            Console.WriteLine(new GetBalanceHandler().Handle(new GetBalance(bankName, customerName, emiNumber)));
        }
    }
}
