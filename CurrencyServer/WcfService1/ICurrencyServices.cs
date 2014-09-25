using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace CurrencyService
{
    
    /// <summary>
    /// Conversion rates REST service
    /// </summary>
    [ServiceContract]
    public interface ICurrencyServices
    {
        /// <summary>
        /// Get the list of all currencies and corresponding rate
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CurrencyList")]
        List<Currency> GetCurrencyListing();

        /// <summary>
        /// Get a currency, with the conversion value, by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetCurrency/{code}")]
        Currency GetCurrencyByCode(string code);

        /// <summary>
        /// Get the conversion rate between two currencies
        /// </summary>
        /// <param name="code1">Code of currency that is converted</param>
        /// <param name="code2">Code of currency to convert to</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "conversion/{code1}/{code2}")]
        double ConversionRate(string code1, string code2);

    }
}
