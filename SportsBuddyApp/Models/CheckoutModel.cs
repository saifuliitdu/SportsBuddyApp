using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddy.Models
{
    public class CheckoutModel
    {
        public int TotalImage { get; set; }

        public decimal PerProductProductCost { get; set; }
        public decimal TotalCost { get; set; }
        public string TotalCostAsString { get; set; }
    }
}
