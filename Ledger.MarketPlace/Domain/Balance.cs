using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Dto
{
    public class Balance
    {
        public Balance(string bankName, string customerName, decimal paidAmount, int leftEmiCount)
        {
            BankName = bankName;
            CustomerName = customerName;
            PaidAmount = paidAmount;
            LeftEmiCount = leftEmiCount;
        }

        public string BankName { get; set; }
        public string CustomerName { get; set; }
        public decimal PaidAmount { get; set; }
        public int LeftEmiCount { get; set; }

        public override string ToString()
            => this.BankName + " " + this.CustomerName + " " + this.PaidAmount + " " + this.LeftEmiCount;
    }
}
