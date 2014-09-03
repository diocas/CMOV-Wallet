using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace CurrencyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICurrencyServices" in both code and config file together.
    [ServiceContract]
    public interface ICurrencyServices
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CurrencyList")]
        List<Currency> GetCurrencyListing();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetCurrency/{code}")]
        Currency GetCurrencyByCode(string code);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "conversion/{code1}/{code2}")]
        double ConversionRate(string code1, string code2);

    }
}
