using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Controls;
using WalletApp.DataAccess;
using WalletApp.DataModel;


namespace WalletApp.ViewModels
{
    /// <summary>
    /// Money viewmodel, used to pass the values to the GUI
    /// </summary>
    public class MoneyViewModel : INotifyPropertyChanged
    {
        private MoneyDataContext moneyDB;

        // Class constructor, create the data context object.
        public MoneyViewModel(string moneyDBConnectionString)
        {
            moneyDB = new MoneyDataContext(moneyDBConnectionString);
        }

        private ObservableCollection<Money> _moneyItems;
        public ObservableCollection<Money> MoneyItems
        {
            get { return _moneyItems; }
            set
            {
                _moneyItems = value;
                NotifyPropertyChanged("TotalMoney");
                NotifyPropertyChanged("MoneyItems");
            }
        }

        private List<Currency> _currencyList;
        public List<Currency> CurrencyList
        {
            get { return _currencyList; }
            set
            {
                _currencyList = value;

                try
                {
                    Currency.CurrentCurrencyValue = _currencyList.First(x => x.Code == IsolatedStorageSettings.ApplicationSettings["currencyCode"] as string).Value;
                }
                catch (Exception) { }
                NotifyPropertyChanged("CurrencyList");
                NotifyPropertyChanged("TotalMoney");
            }
        }

        public List<Currency> AvailableCurrencyList
        {
            get
            {
                List<Currency> usedCurrencies = new List<Currency>();
                foreach (Money money in MoneyItems)
                {
                    usedCurrencies.Add(money.Currency);
                }
                return CurrencyList.Except(usedCurrencies).ToList(); ;
            }
        }

        public string TotalMoney
        {
            get
            {
                double total = 0;
                foreach (Money money in MoneyItems)
                {
                    total += money.ConvertedValueDouble;
                }
                return total.ToString("0.00") + " " + IsolatedStorageSettings.ApplicationSettings["currencyCode"] as string;
            }
        }

        private string _graphToShow;
        public string GraphToShow
        {
            set
            {
                _graphToShow = value;
                NotifyPropertyChanged("GraphBarVisible");
                NotifyPropertyChanged("GraphCircularVisible");
            }
        }

        public string GraphBarVisible
        {
            get
            {
                return _graphToShow == "Bars" ? "visible" : "Collapsed";
            }
        }

        public string GraphCircularVisible
        {
            get
            {
                return _graphToShow == "Bars" ? "Collapsed" : "visible";
            }
        }

        public Money getMoney(int id)
        {
            return MoneyItems.First(x => x.MoneyId == id);
        }

        public void SaveChangesToDB()
        {
            moneyDB.SubmitChanges();
        }

        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var moneyInDB = from Money money in moneyDB.MoneyItems
                                select money;

            // Query the database and load all to-do items.
            MoneyItems = new ObservableCollection<Money>(moneyInDB);

            // Specify the query for all categories in the database.
            var currencyInDB = from Currency currency in moneyDB.CurrencyItems
                                     select currency;

            // Load a list of all categories.
            CurrencyList = moneyDB.CurrencyItems.ToList();

            NotifyPropertyChanged("TotalMoney");

        }

        public void AddMoney(Money newMoney)
        {
            moneyDB.MoneyItems.InsertOnSubmit(newMoney);
            moneyDB.SubmitChanges();
            MoneyItems.Add(newMoney);
        }

        public void DeleteMoney(Money moneyForDelete)
        {
            MoneyItems.Remove(moneyForDelete);
            moneyDB.MoneyItems.DeleteOnSubmit(moneyForDelete);
            moneyDB.SubmitChanges();
        }

        public void updateQuantity(int id, double quantity)
        {
            MoneyItems.First(x => x.MoneyId == id).Quantity = quantity;
            moneyDB.MoneyItems.First(x => x.MoneyId == id).Quantity = quantity;
            moneyDB.SubmitChanges();
        }

        public void updateCurrencies(ObservableCollection<Currency> currencies)
        {
            foreach (Currency currency in currencies)
            {
                moneyDB.CurrencyItems.First(x => x.Code == currency.Code).Value = currency.Value;
            }
            moneyDB.SubmitChanges();
            LoadCollectionsFromDatabase();
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


        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
