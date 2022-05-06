using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Core.Exceptions;
using MortgageCalculator.Core.Model;
using System;

namespace MortgageCalculator.Core.Tests
{
    [TestClass]
    public class SinglePayment
    {
        private readonly int _homePrice = 300000;
        private readonly int _loanTermYears = 10;
        private readonly int _interestRate = 10;
        private readonly int _interestPaymentInterval = 2;
        private readonly int _decimalPrecision = 2;
        private readonly decimal _expectedResult = (decimal)24072.78;

        [TestMethod]
        public void CorrectValues()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = _decimalPrecision
            };
            decimal onePayment = Payment.GetRecurringAmount(mortgage);
            Assert.AreEqual(_expectedResult, onePayment);
        }

        [TestMethod]
        public void ZeroHomePrice()
        {
            Mortgage mortgage = new()
            {
                HomePrice = 0,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = _decimalPrecision
            };
            decimal onePayment = Payment.GetRecurringAmount(mortgage);
            Assert.AreEqual(0, onePayment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidLoanTermYearsException), "An invalid loan term was allowed!")]
        public void ZeroTermYears()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = 0,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = _decimalPrecision
            };
            decimal onePayment = Payment.GetRecurringAmount(mortgage);
            Assert.AreEqual(_expectedResult, onePayment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInterestRateException), "An invalid interest rate was allowed!")]
        public void ZeroInterestRate()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = 0,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = _decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInterestPaymentIntervalException), "An invalid payment interval was allowed!")]
        public void ZeroPaymentInterval()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = 0,
                Precision = _decimalPrecision
            };
            Payment.GetRecurringAmount(mortgage);
        }

        [TestMethod]
        public void ZeroPrecision()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = 0
            };
            decimal onePayment = Payment.GetRecurringAmount(mortgage);
            Assert.AreEqual(Math.Round(_expectedResult, 0), onePayment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPrecisionException), "An invalid decimal precision was allowed!")]
        public void NegativePrecision()
        {
            Mortgage mortgage = new()
            {
                HomePrice = _homePrice,
                LoanTermYears = _loanTermYears,
                InterestRate = _interestRate,
                InterestPaymentInterval = _interestPaymentInterval,
                Precision = -1
            };
            Payment.GetRecurringAmount(mortgage);
        }
    }
}