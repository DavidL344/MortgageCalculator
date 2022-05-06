namespace MortgageCalculator.Core.Exceptions
{
    public class InvalidHomePriceException : Exception
    {
        public InvalidHomePriceException() { }
        public InvalidHomePriceException(string message) : base(message) { }
        public InvalidHomePriceException(string message, Exception innerException) : base(message, innerException) { }
    }
}