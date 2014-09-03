﻿using System;
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
                    _quantity = value;
                    NotifyPropertyChanged("Quantity");
                }
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
