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
         // Specify the connection string as a static, used in main page and app.xaml.
    public static string DBConnectionString = "Data Source=isostore:/Wallet.sdf";

    // Pass the connection string to the base class.
    public MoneyDataContext(string connectionString)
        : base(connectionString)
    { }

    // Specify a single table for the to-do items.
    public Table<Money> MoneyItems;
    }
}