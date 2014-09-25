using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WalletApp.Resources;
using WalletApp.DataAccess;
using System.Diagnostics;
using WalletApp.DataModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AmCharts.Windows.QuickCharts;

namespace WalletApp
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        /// <summary>
        /// When oppening the app, try to update the currencies with the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
            UpdateCurrencies();
        }

        /// <summary>
        /// When returning to page, sync the data with the database.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.ViewModel.SaveChangesToDB();
        }

        /// <summary>
        /// Add new currency button click action. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewMoneyEntry.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Sync/update button click action. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateAppBarButton_Click(object sender, EventArgs e)
        {
            UpdateCurrencies();
        }

        /// <summary>
        /// Sync the currencies with the server, while showing the progress bar.
        /// </summary>
        private async void UpdateCurrencies()
        {
            try
            {
                SystemTray.ProgressIndicator.IsIndeterminate = true;
                SystemTray.ProgressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator.Text = "Updating currencies";
                App.ViewModel.updateCurrencies(await ServerConnection.FetchCurrencies());
            }
            catch (Exception )
            {
                MessageBox.Show("Server not available");
            }

            SystemTray.ProgressIndicator.IsVisible = false;
           
        }

        /// <summary>
        /// Money list click action. Open the newMoney page to edit or delete the ammount of money for that currency.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoneyItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int id = ((Money)MoneyItemsListBox.SelectedValue).MoneyId;
                NavigationService.Navigate(new Uri("/NewMoneyEntry.xaml?id=" + id.ToString(), UriKind.Relative));
                MoneyItemsListBox.SelectedValue = -1;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Settings button click action. Open the settings page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
       
    }
}