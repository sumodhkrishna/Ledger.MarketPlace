using System;
using System.Collections.Generic;

namespace Ledger.MarketPlace.Strategy
{
    public class InputCommandContext
    {
        private Dictionary<string, ICommandProcessorStrategy> serviceMap = new()
        {
            {"LOAN",new LoanProcessorStrategy() },
            {"BALANCE", new BalanceProcessorStrategy() },
            {"PAYMENT", new PaymentProcessorStrategy() }
        };
        public void Process(string input)
        {
            try
            {
                var inputArray = input.Trim().Split(' ');
                if (serviceMap.TryGetValue(inputArray[0].Trim(), out var service))
                {
                    service.Process(inputArray);
                }
            }
            catch(Exception ex)
            {
                //Swallowing exceptions - in case of better error handling can be replaced
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
