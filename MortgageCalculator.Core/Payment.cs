using DecimalMath;
using MortgageCalculator.Core.Exceptions;
using MortgageCalculator.Core.Model;

namespace MortgageCalculator.Core
{
    public class Payment
    {
        public static decimal GetRecurringAmount(Mortgage mortgage)
        {
            Verify(mortgage);

            decimal interestRate = mortgage.InterestRate / 100;
            decimal i_m = interestRate / mortgage.InterestPaymentInterval;
            int n_m = mortgage.LoanTermYears * mortgage.InterestPaymentInterval;

            decimal x = 1 + i_m;
            decimal i_m_one_pow_n_m = DecimalEx.Pow(x, n_m);

            return Math.Round(mortgage.HomePrice * i_m * i_m_one_pow_n_m / (i_m_one_pow_n_m - 1), mortgage.Precision);
        }

        public static decimal GetInterestAmount(Mortgage mortgage)
        {
            Verify(mortgage);
            int payThisManyTimes = mortgage.LoanTermYears * mortgage.InterestPaymentInterval;
            return GetRecurringAmount(mortgage) * payThisManyTimes - mortgage.HomePrice;
        }

        public static void Verify(Mortgage mortgage)
        {
            if (mortgage.HomePrice < 0 || mortgage.HomePrice > 100000000) throw new InvalidHomePriceException("Invalid home price!");
            if (mortgage.InterestRate <= 0 || mortgage.InterestRate > 100) throw new InvalidInterestRateException("Invalid interest rate!");
            if (mortgage.InterestPaymentInterval <= 0 || mortgage.InterestPaymentInterval > 730) throw new InvalidInterestPaymentIntervalException("Invalid interest payment interval!");
            if (mortgage.LoanTermYears <= 0 || mortgage.LoanTermYears > 100) throw new InvalidLoanTermYearsException("Invalid loan term!");
            if (mortgage.Precision < 0 || mortgage.Precision > 28) throw new InvalidPrecisionException("The precision must be between 0 and 28!");
        }
    }
}