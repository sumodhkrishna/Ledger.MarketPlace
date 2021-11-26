using Ledger.MarketPlace.Domain;
using System;
using Xunit;

namespace Ledger.MarketPlace.Test.Domain
{
    [Collection("Ledger.MarketPlace.Test")]
    public class LoanTest
    {
        [Theory]
        [InlineData(10000,5,4,60,200)]
        [InlineData(5000, 1, 6, 12, 442)]
        public void TestLoanEmiAndTenureCalculation(decimal amount, int years, decimal rateOfInterest, int tenure, int emiAmount)
        {
            var loan = new Loan(amount, years, rateOfInterest);

            Assert.Equal(tenure, loan.Tenure);
            Assert.Equal(emiAmount, loan.EMIAmount);
        }
        [Fact]
        public void TestLoan_GetBalanceAndLeftEmis()
        {
            var loan = new Loan(10000, 5, 4);
            const int expectedLeftEmiCount = 55;
            const int expectedPaidAmount = 1000;
            
            var actual = loan.GetPaidAmountAndLeftEmis(5);

            Assert.Equal(expectedPaidAmount, actual.paidAmount);
            Assert.Equal(expectedLeftEmiCount, actual.leftEmiCount);
        }
        [Fact]
        public void TestLoan_GetBalanceAndLeftEmis_OnLastEmi()
        {
            var loan = new Loan(10000, 5, 4);
            const int expectedLeftEmiCount = 0;
            decimal expectedPaidAmount = loan.TotalAmount;

            var actual = loan.GetPaidAmountAndLeftEmis(60);

            Assert.Equal(expectedPaidAmount, actual.paidAmount);
            Assert.Equal(expectedLeftEmiCount, actual.leftEmiCount);
        }

        [Fact]
        public void TestLoan_GetBalanceAndLeftEmis_ThrowsExceptionWhenEmiNumberIsIncorrect()
        {
            var loan = new Loan(5000, 1, 6);

            Assert.Throws<IndexOutOfRangeException>(() => loan.GetPaidAmountAndLeftEmis(13));
        }

        [Fact]
        public void TestLoan_LastEmiAdjusted()
        {
            var loan = new Loan(5000, 1, 6);
            var expectedEmiSchedule = new decimal[]{
                0,442,442,442,442,442,442,442,442,442,442,442,438
            };

            var actualEmiSchedule = loan.EmiSchedule;

            Assert.Equal(expectedEmiSchedule, actualEmiSchedule);
        }

        [Fact]
        public void TestLoan_GetBalanceAndLeftEmisWithPayments()
        {
            var loan = new Loan(5000, 1, 6);
            loan.MakePayment(1000, 5);
            const int expectedAmountBeforePayment = 1326;
            const int expectedAmountAfterPayment = 3652;
            const int expectedLeftEMICountBeforePayment = 9;
            const int expectedLeftEmiCountAfterPayment = 4;

            var actualBeforePayment = loan.GetPaidAmountAndLeftEmis(3);
            var actualAfterBulkPayment = loan.GetPaidAmountAndLeftEmis(6);

            Assert.Equal(expectedAmountBeforePayment, actualBeforePayment.paidAmount);
            Assert.Equal(expectedAmountAfterPayment, actualAfterBulkPayment.paidAmount);
            Assert.Equal(expectedLeftEMICountBeforePayment, actualBeforePayment.leftEmiCount);
            Assert.Equal(expectedLeftEmiCountAfterPayment, actualAfterBulkPayment.leftEmiCount);
        }

        [Fact]
        public void TestLoan_MakePamentWithWrongEMINumber_ThrowsException()
        {
            var loan = new Loan(5000, 1, 6);
            Assert.Throws<IndexOutOfRangeException>(() => loan.GetPaidAmountAndLeftEmis(13));
        }


        [Fact]
        public void TestLoan_PaymentAt0()
        {
            // PAYMENT MBI Dale 1000 0
            var loan = new Loan(5000, 4, 5);
            loan.MakePayment(1000, 0);

            var actualBeforePayment = loan.GetPaidAmountAndLeftEmis(0);

            Assert.Equal(1000, actualBeforePayment.paidAmount);
            Assert.Equal(40, actualBeforePayment.leftEmiCount);
        }

        [Fact]
        public void MultiplePayments_ShouldShowCorrectEMICount()
        {
            var loan = new Loan(5000, 1, 6);
            loan.MakePayment(1000, 5);
            loan.MakePayment(500, 7);

            const int expectedAmountBeforePayment = 1326;
            const int expectedAmountAfterFirstPayment = 3652;
            const int expectedLeftEMICountBeforePayment = 9;
            const int expectedLeftEmiCountAfterFirstPayment = 4;

            var actualBeforePayment = loan.GetPaidAmountAndLeftEmis(3);
            var actualAfterFirstPayment = loan.GetPaidAmountAndLeftEmis(6);
            var actualAfterSecondPayment = loan.GetPaidAmountAndLeftEmis(8); 

            Assert.Equal(expectedAmountBeforePayment, actualBeforePayment.paidAmount);
            Assert.Equal(expectedAmountAfterFirstPayment, actualAfterFirstPayment.paidAmount);
            Assert.Equal(expectedLeftEMICountBeforePayment, actualBeforePayment.leftEmiCount);
            Assert.Equal(expectedLeftEmiCountAfterFirstPayment, actualAfterFirstPayment.leftEmiCount);
            Assert.Equal(1, actualAfterSecondPayment.leftEmiCount);
            Assert.Equal(5036, actualAfterSecondPayment.paidAmount);
        }
    }
}
