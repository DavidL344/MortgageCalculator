namespace MortgageCalculator.Core.Model
{
    public struct Mortgage
    {
        /// <summary>
        /// The borrowed amount of money
        /// </summary>
        /// <example>100 000 CZK</example>
        public decimal HomePrice { get; set; }

        /// <summary>
        /// The percentage of how much more will the person have to pay extra
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Indicates how many years it will take to pay the debt
        /// </summary>
        /// <example>25 years</example>
        public int LoanTermYears { get; set; }

        /// <summary>
        /// Indicates how often will a part of the debt be repaid
        /// </summary>
        /// <example>12 - monthly</example>
        public int InterestPaymentInterval { get; set; }

        /// <summary>
        /// The result's float precision
        /// </summary>
        public int Precision { get; set; }
    }
}
