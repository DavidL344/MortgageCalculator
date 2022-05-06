namespace MortgageCalculator.Core.Exceptions
{
    public class NegativePrecisionException : Exception
    {
        public NegativePrecisionException() { }
        public NegativePrecisionException(string message) : base(message) { }
        public NegativePrecisionException(string message, Exception innerException) : base(message, innerException) { }
    }
}