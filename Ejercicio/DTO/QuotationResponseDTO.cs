using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ejercicio.DTO
{
    public class QuotationResponseDTO
    {
        private double _price;

        public string Currency { get; set; }
        public double Price { get => this._price; set => this._price = Math.Round(value, 2); }
    }
}