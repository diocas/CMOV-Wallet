using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Diagnostics;

namespace CurrencyService
{
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CurrencyServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CurrencyServices.svc or CurrencyServices.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyServices : ICurrencyServices
    {
        /// <summary>
        /// Get the list of all currencies and corresponding rate
        /// </summary>
        /// <returns></returns>
        public List<Currency> GetCurrencyListing()
        {
            Console.WriteLine(GetCurrencyByCode("EUR").Code);
                return new CurEntityModel().Currencies.ToList();
        }

        /// <summary>
        /// Get a currency, with the conversion value, by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Currency GetCurrencyByCode(string code) {
            using (CurEntityModel currencies = new CurEntityModel())
            {
                Currency curency = new Currency();
                var curencyTemp = (from c in currencies.Currencies where c.Code == code select c).First();
                curency.Id = curencyTemp.Id;
                curency.Code = curencyTemp.Code;
                curency.Value = curencyTemp.Value;
                return curency; 
            } 
        }

        /// <summary>
        /// Get the conversion rate between two currencies
        /// </summary>
        /// <param name="code1">Code of currency that is converted</param>
        /// <param name="code2">Code of currency to convert to</param>
        /// <returns></returns>
        public double ConversionRate(string code1, string code2)
        {
            Currency currency1 = GetCurrencyByCode(code1);
            Currency currency2 = GetCurrencyByCode(code2);

            return currency1.Value / currency2.Value;
        }

        public void UpdateCurrency(string code, double value)
        {
            using (CurEntityModel currencies = new CurEntityModel())
            {
                Currency cur = currencies.Currencies.SingleOrDefault(c => c.Code == code);
                cur.Value = value;
                currencies.SaveChanges();
            }
        }

    }
}
