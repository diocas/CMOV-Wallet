using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace WalletApp.DataModel
{
    class Money 
    {
    }
}


//[Table]
//public class ToDoItem : INotifyPropertyChanged, INotifyPropertyChanging
//{
//    // Define ID: private field, public property and database column.
//    private int _toDoItemId;

//    [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
//    public int ToDoItemId
//    {
//        get
//        {
//            return _toDoItemId;
//        }
//        set
//        {
//            if (_toDoItemId != value)
//            {
//                NotifyPropertyChanging("ToDoItemId");
//                _toDoItemId = value;
//                NotifyPropertyChanged("ToDoItemId");
//            }
//        }
//    }

//    // Define item name: private field, public property and database column.
//    private string _itemName;

//    [Column]
//    public string ItemName
//    {
//        get
//        {
//            return _itemName;
//        }
//        set
//        {
//            if (_itemName != value)
//            {
//                NotifyPropertyChanging("ItemName");
//                _itemName = value;
//                NotifyPropertyChanged("ItemName");
//            }
//        }
//    }

//    // Define completion value: private field, public property and database column.
//    private bool _isComplete;

//    [Column]
//    public bool IsComplete
//    {
//        get
//        {
//            return _isComplete;
//        }
//        set
//        {
//            if (_isComplete != value)
//            {
//                NotifyPropertyChanging("IsComplete");
//                _isComplete = value;
//                NotifyPropertyChanged("IsComplete");
//            }
//        }
//    }
//    // Version column aids update performance.
//    [Column(IsVersion = true)]
//    private Binary _version;

//    #region INotifyPropertyChanged Members

//    public event PropertyChangedEventHandler PropertyChanged;

//    // Used to notify the page that a data context property changed
//    private void NotifyPropertyChanged(string propertyName)
//    {
//        if (PropertyChanged != null)
//        {
//            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    #endregion

//    #region INotifyPropertyChanging Members

//    public event PropertyChangingEventHandler PropertyChanging;

//    // Used to notify the data context that a data context property is about to change
//    private void NotifyPropertyChanging(string propertyName)
//    {
//        if (PropertyChanging != null)
//        {
//            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
//        }
//    }

//    #endregion
//}
