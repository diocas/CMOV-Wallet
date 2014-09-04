using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Controls;
using WalletApp.DataAccess;
using WalletApp.DataModel;


namespace WalletApp.ViewModels
{
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
                NotifyPropertyChanged("MoneyItems");
                NotifyPropertyChanged("TotalMoney");
            }
        }


        private List<Currency> _currencyList;
        public List<Currency> CurrencyList
        {
            get { return _currencyList; }
            set
            {
                _currencyList = value;
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

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            moneyDB.SubmitChanges();
        }

        // Query database and load the collections and list used by the pivot pages.
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


        // Remove a to-do task item from the database and collections.
        public void DeleteMoney(Money moneyForDelete)
        {
            MoneyItems.Remove(moneyForDelete);
            moneyDB.MoneyItems.DeleteOnSubmit(moneyForDelete);
            moneyDB.SubmitChanges();
        }

        public Money getMoney(int id)
        {
            return MoneyItems.First(x => x.MoneyId == id);
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

        public string TotalMoney
        {
            get
            {
                double total = 0;
                foreach (Money money in MoneyItems)
                {
                    total += money.ConvertedValueDouble;
                }
                return total.ToString("0.00") + IsolatedStorageSettings.ApplicationSettings["currencyCode"] as string;
            }
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
