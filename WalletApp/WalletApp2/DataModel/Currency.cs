
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace WalletApp.DataModel
{
    [Table]
    public class Currency : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _code;

        [Column]
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    NotifyPropertyChanging("Code");
                    _code = value;
                    NotifyPropertyChanged("Code");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private double _value;

        [Column]
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    NotifyPropertyChanging("Value");
                    _value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }


        // Define the entity set for the collection side of the relationship.
        private EntitySet<Money> _money;

        [Association(Storage = "_money", OtherKey = "_currencyId", ThisKey = "Id")]
        public EntitySet<Money> Money
        {
            get { return this._money; }
            set { this._money.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public Currency()
        {
            _money = new EntitySet<Money>(
                new Action<Money>(this.attach_Money),
                new Action<Money>(this.detach_Money)
                );
        }

        // Called during an add operation
        private void attach_Money(Money money)
        {
            NotifyPropertyChanging("Money");
            money.Currency = this;
        }

        // Called during a remove operation
        private void detach_Money(Money money)
        {
            NotifyPropertyChanging("Money");
            money.Currency = null;
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
