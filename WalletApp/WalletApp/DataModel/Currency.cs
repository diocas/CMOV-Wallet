using System;
using System.Collections.Generic;

namespace WalletApp.DataAccess
{
    public partial class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }

        public Currency()
        {
        }

    }
}
