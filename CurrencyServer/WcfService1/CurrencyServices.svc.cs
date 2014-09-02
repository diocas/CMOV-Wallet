using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace CurrencyService
{
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CurrencyServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CurrencyServices.svc or CurrencyServices.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyServices : ICurrencyServices
    {

        public List<Currency> GetCurrencyListing()
        {
            Console.WriteLine(GetCurrencyByCode("EUR").Code);
                return new CurEntityModel().Currencies.ToList();
        }

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


        public double ConversionRate(string code1, string code2)
        {
            Currency currency1 = GetCurrencyByCode(code1);
            Currency currency2 = GetCurrencyByCode(code2);

            return currency1.Value / currency2.Value;
        }

    }
}
