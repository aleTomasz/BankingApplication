using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class BankService
    {
        private List<Customer> _customers = new List<Customer>();

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public Customer GetCustomer(string fullName)
        {
            return _customers.FirstOrDefault(c => c.FullName == fullName);
        }

        public void CreateSavingsAccount(Customer customer, decimal initialBalance, decimal interestRate)
        {
            var account = new SavingsAccount
            {
                Balance = initialBalance,
                InterestRate = interestRate
            };
            customer.Accounts.Add(account);
        }

        public void CreateInvestmentAccount(Customer customer, decimal initialBalance)
        {
            var account = new InvestmentAccount
            {
                Balance = initialBalance
            };
            customer.Accounts.Add(account);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

    }
}