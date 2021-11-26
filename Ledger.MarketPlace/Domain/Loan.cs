using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.MarketPlace.Domain
{
    public class Loan
    {

        public Loan(decimal amount, int years, decimal rateOfInterest)
        {
            TotalAmount = (amount * years * (rateOfInterest / 100)) + amount;
            Tenure = years * 12;
            EMIAmount = (decimal)Math.Ceiling(TotalAmount / Tenure);
            EmiSchedule = Enumerable.Repeat(EMIAmount, Tenure).ToArray(); 
            if(EmiSchedule.Sum() > TotalAmount) 
                this.EmiSchedule[this.EmiSchedule.Length - 1] = GetLastEmi(this.EmiSchedule.Length - 1,this.EmiSchedule);
            Amount = amount;
            Years = years;
            RateOfInterest = rateOfInterest;
            paymentHistoryAndUpdatedEMISchedule = new Dictionary<int, decimal[]>();
        }
        public decimal EMIAmount { get; private set; }
        public int Tenure { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal[] EmiSchedule { get; private set; }
        public decimal Amount { get; }
        public int Years { get; }
        public decimal RateOfInterest { get; }

        private Dictionary<int, decimal[]> paymentHistoryAndUpdatedEMISchedule;

        public (decimal paidAmount, int leftEmiCount) GetPaidAmountAndLeftEmis(int emiNumber) =>
            (paidAmount: calculatePaidAmount(emiNumber), leftEmiCount: calculateLeftEmisByTenure(emiNumber));
        public void MakePayment(decimal lumpSumAmount, int emiNumber)
        {
            CheckEmiOutOfRange(emiNumber);
            var newEmiSchedule = (decimal[])this.GetApplicableEmiSchedule(emiNumber).Clone();
            newEmiSchedule[emiNumber - 1] += lumpSumAmount;
            var leftEmiCount = Math.Ceiling((TotalAmount - newEmiSchedule.Take(emiNumber).Sum()) / EMIAmount);
            int indexForLastEmi = (int)(leftEmiCount + emiNumber);
            newEmiSchedule = AdjustEmiSchedule(indexForLastEmi, newEmiSchedule);
            paymentHistoryAndUpdatedEMISchedule.Add(emiNumber, newEmiSchedule);
        }
        private decimal[] AdjustEmiSchedule(int lastEmiPosition, decimal[] emiSchedule)
        {
            var indexForLastEmi = lastEmiPosition - 1;
            Array.Clear(emiSchedule, indexForLastEmi, emiSchedule.Length - indexForLastEmi);
            emiSchedule[indexForLastEmi] = GetLastEmi(indexForLastEmi, emiSchedule);
            return emiSchedule;
        }

        private decimal GetLastEmi(int indexForLastEmi, decimal[] emiSchedule)
            => this.TotalAmount - emiSchedule.Take(indexForLastEmi).Sum();

        private decimal calculatePaidAmount(int emiNumber)
        {
            CheckEmiOutOfRange(emiNumber);
            return GetApplicableEmiSchedule(emiNumber).Take(emiNumber).Sum();
        }

        private int calculateLeftEmisByTenure(int emiNumber)
        {
            CheckEmiOutOfRange(emiNumber);
            return GetApplicableEmiSchedule(emiNumber)
                .Where(es => es > 0).Count() - emiNumber;
        }

        private decimal[] GetApplicableEmiSchedule(int emiNumber)
        {
            var paymentIndex = paymentHistoryAndUpdatedEMISchedule.Keys.Where(key => key <= emiNumber)
                            .OrderByDescending(k => k).FirstOrDefault();
            if (paymentIndex > 0)
            {
                return paymentHistoryAndUpdatedEMISchedule.GetValueOrDefault(paymentIndex);
            }
            return this.EmiSchedule;
        }

        private void CheckEmiOutOfRange(int emiNumber)
        {
            if (emiNumber > this.EmiSchedule.Length)
                throw new IndexOutOfRangeException("The given emi number is grater than total EMI length");
        }
    }
}
