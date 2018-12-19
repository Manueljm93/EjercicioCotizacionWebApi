using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ejercicio.Controllers
{
    [RoutePrefix("api/currency")]
    public class CurrencyController : ApiController
    {
        static HttpClient client = new HttpClient();
        private const string apiKey = "1535|62MghU^3RFs2xVcVF95ELCn7G^49UJ^D";
        private string apiURL = "https://api.cambio.today/v1/quotes/{ISOCode}/ARS/json?key={key}";
        private string[] availableCurrencies = new string[] { "USD", "EUR", "GBP" };

        [Route("")]
        public IHttpActionResult GetAllCurrencies()
        {
            var url = this.GetAPIURLWithKey();
            //TODO: Implementar logica para devolver coleccion de todas las 
            //monedas DEFINIDAS
            return Ok("");
        }

        [Route("{ISOCode}")]
        public IHttpActionResult GetByISOCode(string ISOCode)
        {
            var url = this.GetCurrencyQuotationURLByISOCode(ISOCode);
            var wc = (new WebClient());
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = wc.DownloadString(url);


            return Ok(response);
        }

        private string GetAPIURLWithKey()
        {
            return this.apiURL.Replace("{key}", CurrencyController.apiKey);
        }

        private string GetCurrencyQuotationURLByISOCode(string ISOCode)
        {
            return this.GetAPIURLWithKey().Replace("{ISOCode}", ISOCode);
        }
    }
}
