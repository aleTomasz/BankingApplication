using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class InvestmentAccount : Account
    {
        public List<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    }
}
