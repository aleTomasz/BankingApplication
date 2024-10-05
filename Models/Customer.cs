using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Customer
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
