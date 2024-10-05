using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class StockOrder
    {
        public string StockName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerStock    { get; set; }
        public decimal Commission { get; set; }
    }
}
