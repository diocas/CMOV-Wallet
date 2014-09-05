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

            if (settings.Contains("graphType"))
            {
                string graph = settings["graphType"] as string;
                foreach (ListPickerItem pick in defaultGraphListPicker.Items)
                {
                    if (pick.Content.ToString() == graph)
                    {
                        defaultGraphListPicker.SelectedItem = pick;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Currency chosenCurrency = (Currency)defaultCurrencyListPicker.SelectedItem;
            Currency.CurrentCurrencyValue = chosenCurrency.Value;
            String graph = ((ListPickerItem)defaultGraphListPicker.SelectedItem).Content.ToString();
            if (!settings.Contains("currencyCode"))
            {
                settings.Add("currencyCode", chosenCurrency.Code);
            }
            else
            {
                settings["currencyCode"] = chosenCurrency.Code;
            }

            if (!settings.Contains("graphType"))
            {
                settings.Add("graphType", graph);
            }
            else
            {
                settings["graphType"] = graph;
            }
            App.ViewModel.GraphToShow = graph;
            settings.Save();
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}