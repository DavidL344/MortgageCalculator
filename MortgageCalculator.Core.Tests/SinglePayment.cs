using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Core.Exceptions;
using MortgageCalculator.Core.Model;

namespace MortgageCalculator.Core.Tests
{
    [TestClass]
    public class SinglePayment
    {
        [TestMethod]
        [DataRow(300000, 10, 10, 2, 2, 24072.78)]
        [DataRow(0, 10, 10, 2, 2, 0)] // No home price
        [DataRow(300000, 10, 10, 2, 0, 24073)] // Rounded to an integer
        public void CorrectValues(int homePrice, int loanTermYears, int interestRate,
            int interestPaymentInterval, int decimalPrecision, double expectedResult)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            var onePayment = Payment.GetRecurringAmount(mortgage);
            Assert.IsTrue((decimal)expectedResult == onePayment);
        }

        [TestMethod]
        [DataRow(300000, 0, 10, 2, 2)]
        [ExpectedException(typeof(InvalidLoanTermYearsException), "An invalid loan term was allowed!")]
        public void ZeroTermYears(int homePrice, int loanTermYears, int interestRate,
            int interestPaymentInterval, int decimalPrecision)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }

        [TestMethod]
        [DataRow(300000, 10, 0, 2, 2)]
        [ExpectedException(typeof(InvalidInterestRateException), "An invalid interest rate was allowed!")]
        public void ZeroInterestRate(int homePrice, int loanTermYears, int interestRate,
            int interestPaymentInterval, int decimalPrecision)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }

        [TestMethod]
        [DataRow(300000, 10, 10, 0, 2)]
        [ExpectedException(typeof(InvalidInterestPaymentIntervalException), "An invalid payment interval was allowed!")]
        public void ZeroPaymentInterval(int homePrice, int loanTermYears, int interestRate,
            int interestPaymentInterval, int decimalPrecision)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }

        [TestMethod]
        [DataRow(300000, 10, 10, 2, -1)]
        [DataRow(300000, 10, 10, 2, int.MinValue)]
        [ExpectedException(typeof(InvalidPrecisionException), "An invalid decimal precision was allowed!")]
        public void NegativePrecision(int homePrice, int loanTermYears, int interestRate,
            int interestPaymentInterval, int decimalPrecision)
        {
            Mortgage mortgage = new()
            {
                HomePrice = homePrice,
                LoanTermYears = loanTermYears,
                InterestRate = interestRate,
                InterestPaymentInterval = interestPaymentInterval,
                Precision = decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }
    }
}