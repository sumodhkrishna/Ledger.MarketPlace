using Ledger.MarketPlace.Strategy;
using Ledger.MarketPlace.Utilities;
using System;

namespace Ledger.MarketPlace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var input = FileReader.ReadFile(args[0]);
                var inputCommandExecutor = new InputCommandContext();
                input.ForEach(command =>
                {
                    inputCommandExecutor.Process(command);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
