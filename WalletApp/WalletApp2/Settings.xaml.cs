using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using WalletApp.DataModel;
using System.Diagnostics;

namespace WalletApp
{
    public partial class Settings : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings;

        public Settings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
            InitializeComponent();
            this.DataContext = App.ViewModel;

            if (settings.Contains("currencyCode"))
            {
                string code = settings["currencyCode"] as string;
                defaultCurrencyListPicker.SelectedItem = App.ViewModel.CurrencyList.First(x => x.Code == code);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            String code = ((Currency)defaultCurrencyListPicker.SelectedItem).Code;
            if (!settings.Contains("currencyCode"))
            {
                settings.Add("currencyCode", code);
            }
            else
            {
                settings["currencyCode"] = code;
            }
            settings.Save();
        }
    }
}