using Ledger.MarketPlace.Dto;
using Ledger.MarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Commands
{
    public class GetBalance
    {
        public GetBalance(string bankName, string customerName, int emiNumber)
        {
            BankName = bankName;
            CustomerName = customerName;
            EmiNumber = emiNumber;
        }

        public string BankName { get; set; }
        public string CustomerName { get; set; }
        public int EmiNumber { get; set; }
        public bool Valid => this.EmiNumber >= 0;
    }

    public class GetBalanceHandler
    {
        BankDataRepository dataRepository = new BankDataRepository();
        public string Handle(GetBalance getBalance)
        {
            if(!getBalance.Valid) throw new ArgumentException("Input received negetive values");
            var balance = dataRepository.GetBalance(getBalance.EmiNumber, getBalance.BankName, getBalance.CustomerName);
            return balance.ToString();
        }
    }
}
