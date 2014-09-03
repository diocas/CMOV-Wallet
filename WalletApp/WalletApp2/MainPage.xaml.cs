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
        // Data context for the local database
        private MoneyDataContext moneyDB;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<Money> _moneyItems;
        public ObservableCollection<Money> MoneyItems
        {
            get
            {
                return _moneyItems;
            }
            set
            {
                if (_moneyItems != value)
                {
                    _moneyItems = value;
                    NotifyPropertyChanged("MoneyItems");
                }
            }
        }


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            moneyDB = new MoneyDataContext(MoneyDataContext.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

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

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!App.ViewModel.IsDataLoaded)
            //{
            //    App.ViewModel.LoadData();
            //}

            // Define the query to gather all of the to-do items.
            var moneyItemsInDB = from Money money in moneyDB.MoneyItems
                                select money;

            // Execute the query and place the results into a collection.
            MoneyItems = new ObservableCollection<Money>(moneyItemsInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
        }


        private void AddAppBarButton_Click(object sender, EventArgs e)
        {
            //string groupName = this.pivot.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
            //var group = this.DefaultViewModel[groupName] as SampleDataGroup;
            //var nextItemId = group.Items.Count + 1;
            //var newItem = new SampleDataItem(
            //    string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", this.pivot.SelectedIndex + 1, nextItemId),
            //    string.Format(CultureInfo.CurrentCulture, this.resourceLoader.GetString("NewItemTitle"), nextItemId),
            //    string.Empty,
            //    string.Empty,
            //    this.resourceLoader.GetString("NewItemDescription"),
            //    string.Empty);

            //group.Items.Add(newItem);

            //// Scroll the new item into view.
            //var container = this.pivot.ContainerFromIndex(this.pivot.SelectedIndex) as ContentControl;
            //var listView = container.ContentTemplateRoot as ListView;
            //listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);

            // Create a new to-do item based on the text box.
            Money money = new Money { Code = "EUR", Quantity = 2 };

            // Add a to-do item to the observable collection.
            MoneyItems.Add(money);

            // Add a to-do item to the local database.
            moneyDB.MoneyItems.InsertOnSubmit(money);  
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            moneyDB.SubmitChanges();
        }

        private void UpdateAppBarButton_Click(object sender, EventArgs e)
        {
            UpdateCurrencies();
        }

        private async void UpdateCurrencies()
        {
            try
            {
                var currencies = await ServerConnection.FetchCurrencies();

                Debug.WriteLine("Esperou1????");
                foreach (Currency cur in currencies)
                {
                    Debug.WriteLine(cur.Code);
                }
                Debug.WriteLine("Esperou2????");
            }
            catch (Exception e)
            {
                MessageBox.Show("Server not available");
            }
           
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}