using MortgageCalculator.Core;
using MortgageCalculator.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MortgageCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly bool Ready = false;
        private Mortgage Mortgage
        {
            get
            {
                string tag = ((ComboBoxItem)cb_paymentInterval.SelectedItem).Tag.ToString() ?? "";
                bool tagParsed = int.TryParse(tag, out int paymentInterval);
                if (!tagParsed) throw new Exception("The payment interval couldn't be parsed!");

                Mortgage updatedMortgage = new()
                {
                    HomePrice = (decimal)nb_homePrice.Value,
                    InterestPaymentInterval = paymentInterval,
                    InterestRate = (decimal)nb_interestRate.Value,
                    LoanTermYears = (int)nb_loanTerm.Value,
                    Precision = 2
                };

                bool isUninitialized = _mortgage.Equals(default(Mortgage));
                bool hasSameValues = _mortgage.Equals(updatedMortgage);
                if (isUninitialized || !hasSameValues) _mortgage = updatedMortgage;
                return _mortgage;
            }
        }
        private Mortgage _mortgage;
        private List<InstallmentPlanEntry> _installmentPlan = new();
        public MainWindow()
        {
            InitializeComponent();
            SetLanguageDictionary();
            Ready = true;
            Update();
        }

        public void Calculate()
        {
            if (!Ready) return;

            decimal recurringAmount = Payment.GetRecurringAmount(Mortgage);
            decimal interestAmount = Payment.GetInterestAmount(Mortgage);
            decimal totalAmount = Mortgage.HomePrice + interestAmount;

            tb_recurringAmount.Text = $"{recurringAmount:G29}";
            tb_interestAmount.Text = $"{interestAmount:G29}";
            tb_totalAmount.Text = $"{totalAmount:G29}";
        }

        private void SetLanguageDictionary()
        {
            string culture = Thread.CurrentThread.CurrentCulture.ToString();
            ResourceDictionary dict = new()
            {
                Source = culture switch
                {
                    "en-US" => new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative),
                    "cs-CZ" => new Uri("..\\Resources\\StringResources.cs-CZ.xaml", UriKind.Relative),
                    _ => new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative),
                }
            };
            this.Resources.MergedDictionaries.Add(dict);
            ComboBoxItem? selectedItem = cb_language.Items.Cast<ComboBoxItem>().FirstOrDefault(e => e.Tag.ToString() == culture);
            cb_language.SelectedValue = selectedItem != null ? culture : "en-US";
        }

        private void SetLanguage(string isoLanguageCode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(isoLanguageCode);
            SetLanguageDictionary();
        }

        private void ChangeLanguage(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox comboBox) return;
            string locale = (string)((ComboBoxItem)comboBox.SelectedItem).Tag ?? "en-US";
            SetLanguage(locale);
        }

        private void Update(object? sender = null, object? e = null)
        {
            try
            {
                Calculate();
                if (tb_error != null) tb_error.Text = string.Empty;
                if (btn_calculate != null) btn_calculate.IsEnabled = true;
            }
            catch (Exception ex)
            {
                tb_error.Text = ex.Message;
                btn_calculate.IsEnabled = false;
            }
        }

        private void ShowInstallmentPlan(object sender, RoutedEventArgs e)
        {
            Mortgage mortgage = Mortgage;
            mortgage.Precision = 0;
            _installmentPlan = Payment.GetInstallmentPlanEntries(mortgage);
            new InstallmentPlanWindow(_installmentPlan, CultureInfo.CurrentCulture).Show();
        }
    }
}
