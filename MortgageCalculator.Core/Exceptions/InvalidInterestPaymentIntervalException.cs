namespace MortgageCalculator.Core.Exceptions
{
    public class InvalidInterestPaymentIntervalException : Exception
    {
        public InvalidInterestPaymentIntervalException() { }
        public InvalidInterestPaymentIntervalException(string message) : base(message) { }
        public InvalidInterestPaymentIntervalException(string message, Exception innerException) : base(message, innerException) { }
    }
}