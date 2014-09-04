using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WalletApp.DataModel;
using System.Diagnostics;

namespace WalletApp
{
    public partial class NewMoneyEntry : PhoneApplicationPage
    {
        private Money currentMoney;

        public NewMoneyEntry()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                int passedId = Int32.Parse(NavigationContext.QueryString["id"]);
                currentMoney = App.ViewModel.getMoney(passedId);

                quantityTextBox.Text = currentMoney.Quantity.ToString();
                currencyListPicker.ItemsSource = App.ViewModel.CurrencyList;
                currencyListPicker.SelectedItem = currentMoney.Currency;
                currencyListPicker.IsEnabled = false;

                //ApplicationBar.IsMenuEnabled = true;
                ApplicationBarMenuItem deleteButton = new ApplicationBarMenuItem("delete");
                ApplicationBar.MenuItems.Add(deleteButton);
                deleteButton.Click += deleteButton_Click;
            }
            catch (Exception) {
            }
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            if (quantityTextBox.Text.Length > 0 && (Currency)currencyListPicker.SelectedItem != null)
            {
                if (currentMoney != null)
                {
                    App.ViewModel.updateQuantity(currentMoney.MoneyId, Double.Parse(quantityTextBox.Text));
                }
                else
                {
                    Money newMoney = new Money
                    {
                        Quantity = Double.Parse(quantityTextBox.Text),
                        Currency = (Currency)currencyListPicker.SelectedItem
                    };
                    App.ViewModel.AddMoney(newMoney);
                }


                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Are your sure you want to delete?", "Delete", MessageBoxButton.OKCancel);

            if (m == MessageBoxResult.OK)
            {
                if (currentMoney != null)
                    App.ViewModel.DeleteMoney(currentMoney);

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
           
        }
    }
}