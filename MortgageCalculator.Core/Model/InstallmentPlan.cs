namespace MortgageCalculator.Core.Model
{
    public struct InstallmentPlan
    {
        public decimal RecurringAmount { get; internal set; }
        public decimal[] Interest { get; internal set; }
        public decimal[] Depreciation { get; internal set; }
        public decimal[] ToRepay { get; internal set; }
        public int Length => Interest.Length;
    }
}
