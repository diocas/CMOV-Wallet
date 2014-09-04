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

namespace WalletApp
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
       

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
            UpdateCurrencies();
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

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }


        private void AddAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewMoneyEntry.xaml", UriKind.Relative));
        }


        private void UpdateAppBarButton_Click(object sender, EventArgs e)
        {
            UpdateCurrencies();
        }

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

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

       
    }
}