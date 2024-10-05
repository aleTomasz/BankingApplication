using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Utilities
{
    public static class CommissionCalculator
    {
        // Metoda do obliczania prowizji od zlecenia zakupu akcji
        public static decimal CalculateCommission(decimal totalAmount)
        {
            // Zakładamy, że prowizja to 2% od całkowitej wartości transakcji
            return totalAmount * 0.02m;
        }
    }
}
