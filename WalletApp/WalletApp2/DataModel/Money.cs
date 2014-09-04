using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;


namespace WalletApp.DataModel
{
    [Table]
    public class Money : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _moneyId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int MoneyId
        {
            get
            {
                return _moneyId;
            }
            set
            {
                if (_moneyId != value)
                {
                    NotifyPropertyChanging("MoneyId");
                    _moneyId = value;
                    NotifyPropertyChanged("MoneyId");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private double _quantity;

        [Column]
        public double Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    NotifyPropertyChanging("Quantity");
                    NotifyPropertyChanging("ConvertedValue");
                    NotifyPropertyChanging("ConvertedValueDouble");
                    _quantity = value;
                    NotifyPropertyChanged("Quantity");
                    NotifyPropertyChanged("ConvertedValue");
                    NotifyPropertyChanged("ConvertedValueDouble");
                }
            }
        }

        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _currencyId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Currency> _currency;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_currency", ThisKey = "_currencyId", OtherKey = "Id", IsForeignKey = true)]
        public Currency Currency
        {
            get { return _currency.Entity; }
            set
            {
                NotifyPropertyChanging("Currency");
                NotifyPropertyChanging("ConvertedValue");
                NotifyPropertyChanging("ConvertedValueDouble");
                _currency.Entity = value;

                if (value != null)
                {
                    _currencyId = value.Id;
                }

                NotifyPropertyChanged("Currency");
                NotifyPropertyChanged("ConvertedValue");
                NotifyPropertyChanged("ConvertedValueDouble");
            }
        }

        public string ConvertedValue
        {
            get
            {
                return ConvertedValueDouble.ToString("0.00") + IsolatedStorageSettings.ApplicationSettings["currencyCode"] as string;
            }
        }

        public double ConvertedValueDouble
        {
            get
            {
                return ( Quantity * Currency.Value);
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
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
