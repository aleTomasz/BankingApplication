using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public abstract class Account
    {
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}
