using MortgageCalculator.Core;
using MortgageCalculator.Core.Model;
using System;
using System.Globalization;
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
        public MainWindow()
        {
            InitializeComponent();
            SetLanguageDictionary();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((ComboBoxItem)cb_paymentInterval.SelectedItem).Tag.ToString() ?? "";
            bool tagParsed = int.TryParse(tag, out int paymentInterval);
            if (!tagParsed)
            {
                tb_result.Text = string.Empty;
                return;
            }

            Mortgage mortgage = new()
            {
                HomePrice = (decimal)nb_homePrice.Value,
                InterestPaymentInterval = paymentInterval,
                InterestRate = (decimal)nb_interestRate.Value,
                LoanTermYears = (int)nb_loanTerm.Value,
                Precision = 2
            };
            decimal result = Payment.CalculateOne(mortgage);
            tb_result.Text = $"{result}";
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
            cb_language.SelectedValue = culture;
        }

        private void SetLanguage(string isoLanguageCode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(isoLanguageCode);
        }

        private void ChangeLanguage(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox comboBox) return;
            string locale = (string)((ComboBoxItem)comboBox.SelectedItem).Tag ?? "en-US";
            SetLanguage(locale);
            SetLanguageDictionary();
        }

        private void SymbolIcon_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("click click");
            new WPFUI.Controls.MessageBox(); // temp test
        }
    }
}
