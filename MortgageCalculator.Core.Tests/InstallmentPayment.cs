using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Core.Model;

namespace MortgageCalculator.Core.Tests
{
    [TestClass]
    public class InstallmentPayment
    {
        private readonly int _homePrice = 300000;
        private readonly int _loanTermYears = 10;
        private readonly int _interestRate = 10;
        private readonly int _interestPaymentInterval = 2;
        private readonly int _decimalPrecision = 2;

        [TestMethod]
        public void CorrectNumberOfEntries()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = _decimalPrecision
            };
            InstallmentPlan installmentPlan = Payment.GetInstallmentPlan(mortgage);
            Assert.AreEqual(20, installmentPlan.Length);
        }
    }
}
