using Bank.Models;
using Bank.Services;
using Bank.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank
{
    public class Program
    {
        static void Main(string[] args)
        {
            BankService bankService = new BankService();
            bool running = true;

            while (running)
            {
                DisplayLogo();
                Console.WriteLine("1. Dodaj klienta");
                Console.WriteLine("2. Stwórz konto oszczędnościowe");
                Console.WriteLine("3. Stwórz konto inwestycyjne");
                Console.WriteLine("4. Zakup akcje");
                Console.WriteLine("5. Wyświetl dane klienta");
                Console.WriteLine("6. Wyjdź");
                Console.Write("Wybierz opcję: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer(bankService);
                        break;
                    case "2":
                        CreateSavingsAccount(bankService);
                        break;
                    case "3":
                        CreateInvestmentAccount(bankService);
                        break;
                    case "4":
                        PurchaseStock(bankService);
                        break;
                    case "5":
                        ShowCustomerDetails(bankService);
                        break;
                    case "6":
                        running = ConfirmExit();
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }

        static void AddCustomer(BankService bankService)
        {
            Console.Clear();
            string fullName = "";
            string address = "";

            // Walidacja imienia i nazwiska
            while (string.IsNullOrWhiteSpace(fullName))
            {
                Console.Write("Podaj imię i nazwisko klienta: ");
                fullName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    Console.WriteLine("Imię i nazwisko nie może być puste. Spróbuj ponownie.");
                }
            }

            // Walidacja adresu
            while (string.IsNullOrWhiteSpace(address))
            {
                Console.Write("Podaj adres klienta: ");
                address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Adres nie może być pusty. Spróbuj ponownie.");
                }
            }

            var customer = new Customer { FullName = fullName, Address = address };
            bankService.AddCustomer(customer);
            Console.WriteLine("Klient dodany pomyślnie.");
        }


        static void CreateSavingsAccount(BankService bankService)
        {
            Console.Clear();
            var customer = SelectCustomer(bankService);
            if (customer != null)
            {
                decimal balance = GetValidDecimal("Podaj początkowe saldo: ");
                decimal interestRate = GetValidDecimal("Podaj oprocentowanie: ");

                bankService.CreateSavingsAccount(customer, balance, interestRate);
                Console.WriteLine("Konto oszczędnościowe utworzone pomyślnie.");
            }
        }
        static void CreateInvestmentAccount(BankService bankService)
        {
            Console.Clear();
            var customer = SelectCustomer(bankService);
            if (customer != null)
            {
                decimal balance = GetValidDecimal("Podaj początkowe saldo: ");
                bankService.CreateInvestmentAccount(customer, balance);
                Console.WriteLine("Konto inwestycyjne utworzone pomyślnie.");
                Console.WriteLine("Teraz możesz Kupić akcję => Wybierz opcję nr 4.");
            }
        }
        static decimal GetValidDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out value) && value >= 0)
                {
                    return value; // Zwracamy prawidłową wartość
                }
                else
                {
                    Console.WriteLine("Nieprawidłowa wartość. Podaj poprawną liczbę większą lub równą 0.");
                }
            }
        }


        static void PurchaseStock(BankService bankService)
        {
            Console.Clear();
            var customer = SelectCustomer(bankService);
            if (customer != null)
            {
                var investmentAccount = customer.Accounts.OfType<InvestmentAccount>().FirstOrDefault();

                if (investmentAccount != null)
                {
                    Console.Write("Podaj nazwę akcji: ");
                    string stockName = Console.ReadLine();
                    Console.Write("Podaj ilość akcji: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Podaj cenę za jedną akcję: ");
                    decimal pricePerStock = decimal.Parse(Console.ReadLine());

                    // Obliczamy całkowity koszt zakupu (ilość * cena za jedną akcję + prowizja)
                    decimal totalCost = quantity * pricePerStock;
                    decimal commission = CommissionCalculator.CalculateCommission(totalCost);
                    decimal totalAmount = totalCost + commission;

                    // Wyświetlenie informacji o prowizji
                    Console.WriteLine($"Prowizja za zakup: {commission:C}");
                    Console.WriteLine($"Całkowity koszt zakupu (w tym prowizja): {totalAmount:C}");

                    // Pytanie użytkownika o kontynuację
                    Console.Write("Czy chcesz kontynuować? (tak/nie): ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "tak")
                    {
                        // Sprawdzenie, czy saldo konta inwestycyjnego jest wystarczające
                        if (investmentAccount.Balance >= totalAmount)
                        {
                            var order = new StockOrder
                            {
                                StockName = stockName,
                                Quantity = quantity,
                                PricePerStock = pricePerStock,
                                Commission = commission
                            };

                            investmentAccount.StockOrders.Add(order);
                            investmentAccount.Balance -= totalAmount; // Zmniejszamy saldo konta inwestycyjnego
                            Console.WriteLine($"Zlecenie zakupu akcji zostało dodane pomyślnie. Całkowity koszt: {totalAmount:C}, pozostałe saldo: {investmentAccount.Balance:C}.");
                        }
                        else
                        {
                            Console.WriteLine($"Brak wystarczających środków na koncie inwestycyjnym. Wymagana kwota: {totalAmount:C}, dostępne saldo: {investmentAccount.Balance:C}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Anulowano zlecenie zakupu akcji. Powrót do menu głównego.");
                    }
                }
                else
                {
                    Console.WriteLine("Klient nie posiada konta inwestycyjnego.");
                }
            }
        }

        static void ShowCustomerDetails(BankService bankService)
        {
            Console.Clear();
            var customers = bankService.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("Brak klientów do wyświetlenia.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Lista klientów:");
                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {customers[i].FullName} - {customers[i].Address}");
                }

                Console.WriteLine();

                Console.Write("Wybierz numer klienta lub 0, aby wrócić: ");

                if (int.TryParse(Console.ReadLine(), out int customerNumber))
                {
                    Console.Clear();
                    if (customerNumber == 0)
                    {
                        return; // Powrót do menu głównego

                    }

                    else if (customerNumber > 0 && customerNumber <= customers.Count)
                    {
                        var customer = customers[customerNumber - 1];
                        Console.WriteLine($"Imię i nazwisko: {customer.FullName}");
                        Console.WriteLine($"Adres: {customer.Address}");
                        Console.WriteLine("Konta:");

                        foreach (var account in customer.Accounts)
                        {
                            if (account is SavingsAccount savingsAccount)
                            {
                                Console.WriteLine($"Konto oszczędnościowe - Saldo: {savingsAccount.Balance}, Oprocentowanie: {savingsAccount.InterestRate} %");
                            }
                            else if (account is InvestmentAccount investmentAccount)
                            {
                                Console.WriteLine($"Konto inwestycyjne - Saldo: {investmentAccount.Balance}");
                                Console.WriteLine("Zlecenia zakupu akcji:");
                                foreach (var order in investmentAccount.StockOrders)
                                {
                                    Console.WriteLine($"Akcje: {order.StockName}, Ilość: {order.Quantity}, Cena za akcję: {order.PricePerStock}, Prowizja: {order.Commission}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                }
            }

        }


        static Customer SelectCustomer(BankService bankService)
        {
            Console.Clear();
            var customers = bankService.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("Brak klientów do wyświetlenia.");
                return null;
            }

            Console.WriteLine("Lista klientów:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].FullName} - {customers[i].Address}");
            }

            Console.Write("Wybierz numer klienta: ");
            if (int.TryParse(Console.ReadLine(), out int customerNumber) && customerNumber > 0 && customerNumber <= customers.Count)
            {
                return customers[customerNumber - 1];
            }

            Console.WriteLine("Nieprawidłowy wybór.");
            return null;
        }
        static void DisplayLogo()
        {
            Console.WriteLine();

            Console.WriteLine("BBBB   AAAAA  N   N  K   K");
            Console.WriteLine("B   B  A   A  NN  N  K  K ");
            Console.WriteLine("BBBB   AAAAA  N N N  KKK  ");
            Console.WriteLine("B   B  A   A  N  NN  K  K ");
            Console.WriteLine("BBBB   A   A  N   N  K   K");

            Console.WriteLine();
        }

        // Funkcja pomocnicza do potwierdzenia wyjścia
        static bool ConfirmExit()
        {
            Console.Write("Opuszczasz system bankowy, czy na pewno chcesz wyjść? (tak/nie): ");
            string response = Console.ReadLine().ToLower();

            if (response == "tak")
            {
                return false; // Zatrzymujemy program (wyjście z pętli)
            }
            else
            {
                Console.WriteLine("Pozostajesz w systemie.");
                Console.WriteLine();
                return true; // Powrót do menu głównego (kontynuacja pętli)
                
            }
        }
    }
}