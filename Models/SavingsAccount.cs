using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; }
    }
}
