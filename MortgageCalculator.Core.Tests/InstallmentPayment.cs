using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Core.Model;

namespace MortgageCalculator.Core.Tests
{
    [TestClass]
    public class InstallmentPayment
    {
        [TestMethod]
        [DataRow(300000, 10, 10, 2, 2, 20)]
        [DataRow(800000, 8, 6.9, 12, 2, 97)]
        public void CorrectNumberOfEntries(int homePrice, int loanTermYears, double interestRate,
            int interestPaymentInterval, int decimalPrecision, int expectedResult)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = (decimal)interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            var installmentPlan = Payment.GetInstallmentPlan(mortgage);
            Assert.IsTrue(expectedResult == installmentPlan.Length, $"{expectedResult} != {installmentPlan.Length}");
        }
    }
}
