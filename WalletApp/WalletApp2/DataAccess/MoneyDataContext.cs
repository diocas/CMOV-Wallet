using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DataModel;

namespace WalletApp.DataAccess
{
    public class MoneyDataContext : DataContext
    {

        public MoneyDataContext(string connectionString)
            : base(connectionString)
        { }


        public Table<Money> MoneyItems;

        public Table<Currency> CurrencyItems;


    }
}