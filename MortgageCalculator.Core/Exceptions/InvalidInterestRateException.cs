namespace MortgageCalculator.Core.Exceptions
{
    public class InvalidInterestRateException : Exception
    {
        public InvalidInterestRateException() { }
        public InvalidInterestRateException(string message) : base(message) { }
        public InvalidInterestRateException(string message, Exception innerException) : base(message, innerException) { }
    }
}