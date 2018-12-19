using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejercicio.Models
{
    public class QuotationResponse
    {
        public Result Result { get; set; }
        public string Status { get; set; }
    }

    public class Result
    {
        public DateTime Updated { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public double Value { get; set; }
        public double Quantity { get; set; }
        public double Amount { get; set; }
    }
}