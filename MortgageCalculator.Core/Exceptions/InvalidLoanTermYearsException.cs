namespace MortgageCalculator.Core.Exceptions
{
    public class InvalidLoanTermYearsException : Exception
    {
        public InvalidLoanTermYearsException() { }
        public InvalidLoanTermYearsException(string message) : base(message) { }
        public InvalidLoanTermYearsException(string message, Exception innerException) : base(message, innerException) { }
    }
}