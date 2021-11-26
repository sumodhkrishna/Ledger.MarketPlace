using Ledger.MarketPlace.Commands;
using Ledger.MarketPlace.Domain;
using Ledger.MarketPlace.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.MarketPlace.Repository
{
    public class BankDataRepository
    {
        private BankData Instance = null;
        public BankDataRepository()
        {
            Instance = BankData.Instance;
        }

        public void MakePayment(decimal amount, int emiNumber, string bankName, string customerName)
        {
            var customer = GetCustomer(customerName, GetBank(bankName, Instance.Banks));
            customer.Loan.MakePayment(amount, emiNumber);
        }

        public Balance GetBalance(int emiNumber,string bankName, string customerName)
        {
            var customer = GetCustomer(customerName, GetBank(bankName, Instance.Banks));
            var amountAndLeftEmis = customer.Loan.GetPaidAmountAndLeftEmis(emiNumber);
            return new Balance(bankName, customerName, amountAndLeftEmis.paidAmount, amountAndLeftEmis.leftEmiCount);
        }

        public void CreateLoan(CreateLoan createLoan)
        {
            var bank = GetBank(createLoan.BankName, Instance.Banks);
            var customer = GetCustomer(createLoan.CustomerName, bank);
            customer.Loan = (new Loan(createLoan.Amount, createLoan.Years, createLoan.RateOfIntrest));
        }


        private Customer GetCustomer(string customerName, Bank bank)
        {
            var customer = bank.Customers.FirstOrDefault(c => c.Name.Equals(customerName));
            if (customer == null)
            {
                customer = new Customer(customerName);
                bank.Customers.Add(customer);
            }
            return customer;
        }

        private Bank GetBank(string bankName, List<Bank> banks)
        {
            var bank = banks.FirstOrDefault(b => b.Name.Equals(bankName));
            if (bank == null)
            {
                bank = new Bank(bankName);
                banks.Add(bank);
            }
            return bank;
        }
    }
}
