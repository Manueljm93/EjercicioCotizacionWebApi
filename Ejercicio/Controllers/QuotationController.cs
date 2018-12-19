using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ejercicio.Models;
using Ejercicio.DTO;
using Newtonsoft.Json;

namespace Ejercicio.Controllers
{
    [RoutePrefix("api/cotizacion")]
    public class QuotationController : ApiController
    {
        static HttpClient client = new HttpClient();
        private const string apiKey = "1535|62MghU^3RFs2xVcVF95ELCn7G^49UJ^D";
        private string apiURL = "https://api.cambio.today/v1/quotes/{ISOCode}/ARS/json?key={key}";
        private Dictionary<string, string> validCurrencies = new Dictionary<string, string>()
        {
            {"euro","EUR" },
            {"dolar","USD" },
            {"libra","GBP" },
        };

        [Route("monedas")]
        public IHttpActionResult GetAllCurrencies()
        {
            var quotationResponseCollection = new QuotationResponseDTO[validCurrencies.Count];
            var i = 0;

            foreach (var key in validCurrencies.Keys)
            {
                var currentCurrencyIsoCode = validCurrencies[key];
                var url = this.GetCurrencyQuotationURLByISOCode(currentCurrencyIsoCode);
                var wc = (new WebClient());
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonData = wc.DownloadString(url);
                var quotationResponse = JsonConvert.DeserializeObject<QuotationResponse>(jsonData);
                var quotationResponseDTO = new QuotationResponseDTO() { Currency = quotationResponse.Result.Source, Price = quotationResponse.Result.Value };
                quotationResponseCollection[i] = quotationResponseDTO;
                i++;
            }
            
            return Ok(quotationResponseCollection);
        }

        [Route("{currency}")]
        public IHttpActionResult GetByISOCode(string currency)
        {
            //Tomamos por defecto que vamos a buscar dolar
            var ISOCode = validCurrencies["dolar"];

            //Intentamos encontrar la moneda que nos hayan pasado
            //como parametro por url
            if (validCurrencies.ContainsKey(currency))
            {
                ISOCode = validCurrencies[currency];
            }

            var url = this.GetCurrencyQuotationURLByISOCode(ISOCode);
            var wc = (new WebClient());
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            var jsonData = wc.DownloadString(url);
            QuotationResponse quotationResponse = JsonConvert.DeserializeObject<QuotationResponse>(jsonData);
            var quotationResponseDTO = new QuotationResponseDTO() { Currency = quotationResponse.Result.Source, Price = quotationResponse.Result.Value };

            return Ok(JsonConvert.SerializeObject(quotationResponseDTO));
        }

        private string GetCurrencyQuotationURLByISOCode(string ISOCode)
        {
            return this.GetAPIURLWithKey().Replace("{ISOCode}", ISOCode);
        }

        private string GetAPIURLWithKey()
        {
            return this.apiURL.Replace("{key}", QuotationController.apiKey);
        }
    }
}
