namespace MortgageCalculator.Core.Model
{
    public class InstallmentPlanEntry
    {
        public int AlreadyPaid { get; internal set; }
        public decimal RecurringAmount { get; internal set; }
        public decimal Interest { get; internal set; }
        public decimal Depreciation { get; internal set; }
        public decimal ToRepay { get; internal set; }
    }
}
