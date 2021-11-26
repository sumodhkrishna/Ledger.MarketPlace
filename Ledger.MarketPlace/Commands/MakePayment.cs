using Ledger.MarketPlace.Repository;
using System;

namespace Ledger.MarketPlace.Commands
{
    public class MakePayment
    {
        public MakePayment(decimal amount, int emiNumber, string customerName, string bankName)
        {
            Amount = amount;
            EmiNumber = emiNumber;
            CustomerName = customerName;
            BankName = bankName;
        }

        public decimal Amount { get; set; }
        public int EmiNumber { get; set; }
        public string CustomerName { get; set; }
        public string BankName { get; set; }
        public bool Valid => Amount > 0 && EmiNumber >= 0;
    }

    public class MakePaymentHandler
    {
        BankDataRepository dataRepository = new BankDataRepository();
        public void Handle(MakePayment payment)
        {
            if (!payment.Valid) throw new ArgumentException("Negetive values are not allowed for amount and EmiNumber");
            dataRepository.MakePayment(payment.Amount, payment.EmiNumber, payment.BankName, payment.CustomerName);
        }
    }
}
