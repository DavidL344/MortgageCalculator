using System;
using MortgageCalculator.Core.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using CsvSharp;
using Microsoft.Win32;

namespace MortgageCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for InstallmentPlanWindow.xaml
    /// </summary>
    public partial class InstallmentPlanWindow : Window
    {
        private readonly CultureInfo _cultureInfo;
        private readonly List<InstallmentPlanEntry> _installmentPlanEntries;

        public InstallmentPlanWindow(List<InstallmentPlanEntry> installmentPlanEntries, CultureInfo cultureInfo)
        {
            InitializeComponent();
            dg_main.ItemsSource = _installmentPlanEntries = installmentPlanEntries;
            _cultureInfo = cultureInfo;
            
            this.KeyDown += OnKeyDown;
        }

        private void SaveToFile()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = "Save As...",
                Filter = "All Supported Files|*.csv;*.json",
                CheckPathExists = true,
                DereferenceLinks = true
            };

            if (saveFileDialog.ShowDialog() != true) return;

            var filePath = saveFileDialog.FileName;
            string serializedData;

            switch (Path.GetExtension(filePath))
            {
                case ".csv":
                    var classProperties = typeof(InstallmentPlanEntry).GetProperties();
                    var csvHeader = string.Join(';', classProperties.Select(x => x.Name));
                    serializedData = $"{csvHeader}\r\n{CsvConvert.Serialize(_installmentPlanEntries, _cultureInfo)}";
                    break;
                case ".json":
                    serializedData = JsonSerializer.Serialize(_installmentPlanEntries, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    break;
                default:
                    MessageBox.Show("File format is not supported!", "MortgageCalculator", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
            }

            try
            {
                File.WriteAllText(filePath, serializedData);
                
                MessageBox.Show($"File saved to \"{filePath}\"!", "MortgageCalculator", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error has occured: {e.Message}", "MortgageCalculator", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))) return;
            if (e.Key != Key.S) return;

            SaveToFile();
        }

        private void SaveToFileButtonClicked(object sender, RoutedEventArgs e)
        {
            SaveToFile();
        }
    }
}
