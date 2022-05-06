using DecimalMath;
using MortgageCalculator.Core.Exceptions;
using MortgageCalculator.Core.Model;

namespace MortgageCalculator.Core
{
    public class Payment
    {
        public static decimal CalculateOne(Mortgage mortgage)
        {
            Verify(mortgage);

            decimal interestRate = mortgage.InterestRate / 100;
            decimal i_m = interestRate / mortgage.InterestPaymentInterval;
            int n_m = mortgage.LoanTermYears * mortgage.InterestPaymentInterval;

            decimal x = 1 + i_m;
            decimal i_m_one_pow_n_m = DecimalEx.Pow(x, n_m);

            return Math.Round(mortgage.HomePrice * i_m * i_m_one_pow_n_m / (i_m_one_pow_n_m - 1), mortgage.Precision);
        }

        public static void Verify(Mortgage mortgage)
        {
            if (mortgage.InterestRate <= 0) throw new InvalidInterestRateException("Invalid interest rate!");
            if (mortgage.InterestPaymentInterval <= 0) throw new InvalidInterestPaymentIntervalException("Invalid interest payment interval!");
            if (mortgage.LoanTermYears <= 0) throw new InvalidLoanTermYearsException("Invalid loan term!");
            if (mortgage.Precision < 0) throw new NegativePrecisionException("The precision cannot be negative!");
        }
    }
}