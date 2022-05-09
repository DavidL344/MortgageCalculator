using MortgageCalculator.Core.Model;
using System.Collections.Generic;
using System.Windows;

namespace MortgageCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for InstallmentPlanWindow.xaml
    /// </summary>
    public partial class InstallmentPlanWindow : Window
    {
        public InstallmentPlanWindow(List<InstallmentPlanEntry> installmentPlanList)
        {
            InitializeComponent();
            dg_main.ItemsSource = installmentPlanList;
        }
    }
}
