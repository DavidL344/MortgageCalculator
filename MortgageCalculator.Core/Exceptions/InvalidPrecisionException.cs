namespace MortgageCalculator.Core.Exceptions
{
    public class InvalidPrecisionException : Exception
    {
        public InvalidPrecisionException() { }
        public InvalidPrecisionException(string message) : base(message) { }
        public InvalidPrecisionException(string message, Exception innerException) : base(message, innerException) { }
    }
}