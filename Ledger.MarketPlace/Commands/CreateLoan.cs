using Ledger.MarketPlace.Domain;
using Ledger.MarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Commands
{
    public class CreateLoan
    {
        public CreateLoan(string bankName, string customerName, decimal amount, int years, decimal rateOfIntrest)
        {
            BankName = bankName;
            CustomerName = customerName;
            Amount = amount;
            Years = years;
            RateOfIntrest = rateOfIntrest;
        }
        public string BankName { get; }
        public string CustomerName { get; }
        public decimal Amount { get; }
        public int Years { get; }
        public decimal RateOfIntrest { get; }
        public bool Valid => (Amount > 0 && Years > 0 && RateOfIntrest > 0);
    }

    public class CreateLoanHandler{
        BankDataRepository dataRepository = new BankDataRepository();

        public void Handle (CreateLoan createLoan)
        {
            if (!createLoan.Valid) throw new ArgumentException("Input received negetive values");
            dataRepository.CreateLoan(createLoan);
        }
    }
}
