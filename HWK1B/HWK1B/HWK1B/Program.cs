using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace HWK1B
{

    /// <summary>
    /// C# console application that uses a menu-driven approach to read data
    /// from a text file, add data to the file, and filter the data and do
    /// some data analysis
    /// </summary>

    class Bill
    {
        public string Provider { get; set; }
        public string BillType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
    internal class Program
    {
        static List<Bill> bills = new List<Bill>();
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\bills.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    var bill = new Bill
                    {
                        Provider = parts[0],
                        BillType = parts[1],
                        Amount = decimal.Parse(parts[2]),
                        Date = DateTime.Parse(parts[3])
                    };
                    bills.Add(bill);
                }
            }

            bool run = true;
            while (run)
            {
                // Display menu
                Console.WriteLine("1. Read bills from the file");
                Console.WriteLine("2. Add bill to the file");
                Console.WriteLine("3. Filter bills by provider");
                Console.WriteLine("4. Filter bills by type");
                Console.WriteLine("5. Calculate Mean and Median");
                Console.WriteLine("6. Show bills text file");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                // Get user input
                var choice = int.Parse(Console.ReadLine());

                /// <summary>
                /// Switch case for read data to file, add data to the file,
                /// filter data from the file and perform analysis 
                /// </summary>

                switch (choice)
                {
                    case 1:
                        ReadBills();
                        break;
                    case 2:
                        AddBill();
                        break;
                    case 3:
                        FilterBillsMethod1();
                        break;
                    case 4:
                        FilterBillsMethod2();
                        break;
                    case 5:
                        CalculateMeanMedian();
                        break;
                    case 6:
                        // Show text file
                        string filePath = @"C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\bills.txt";
                        // using Process from System.Diagnostics
                        // The method allows to open the file with the default text editor, which allows the user to view and edit the contents of the file.
                        Process.Start("notepad.exe", filePath);
                        break;
                    case 7:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again");
                        break;
                }
            }
        }

        /// <summary>
        /// Function for reading the bills from file
        /// </summary>
        static void ReadBills()
        {

            /// <summary>
            /// try-catch blocks to handle any exceptions that might occur 
            /// when reading data from the text file
            /// </summary>
            try
            {
                foreach (var bill in bills)
                {
                    Console.WriteLine($"{bill.Provider} - {bill.BillType} - {bill.Amount:C} - {bill.Date:d}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }

        /// <summary>
        /// Function for adding the bill data to the file
        /// </summary>

        static void AddBill()
        {
            try
            {
                Console.WriteLine("Enter bill provider:");
                var provider = Console.ReadLine();
                Console.WriteLine("Enter bill type:");
                var type = Console.ReadLine();
                Console.WriteLine("Enter bill amount:");
                var amount = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter bill date (MM/dd/yyyy):");
                var date = DateTime.Parse(Console.ReadLine());

                var bill = new Bill { Provider = provider, BillType = type, Amount = amount, Date = date };
                bills.Add(bill);
            }
            catch (IOException)
            {
                Console.WriteLine("Error occured while adding bills data to file!");
            }
        }
        /// <summary>
        /// Method 1 :Function for searching/filtering the data from file
        /// </summary>
        static void FilterBillsMethod1()
        {
            try
            {
                Console.WriteLine("Enter provider to filter by:");
                var provider = Console.ReadLine();
                var filteredBills = bills.Where(b => b.Provider == provider);
                foreach (var bill in filteredBills)
                {
                    Console.WriteLine($"{bill.Provider} - {bill.Amount:C} - {bill.Date:d}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }

        /// <summary>
        /// Method 2: Function for searching/filtering the data from file
        /// </summary>
        static void FilterBillsMethod2()
        {
            try
            {
                Console.WriteLine("Enter type to filter by:");
                var billType = Console.ReadLine();
                var filteredBills = bills.Where(b => b.BillType == billType);
                foreach (var bill in filteredBills)
                {
                    Console.WriteLine($"{bill.Provider} - {bill.BillType} - {bill.Amount:C} - {bill.Date:d}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }
        /// <summary>
        /// Calculates and prints the mean and median to the console based on the amount.
        /// </summary>
        static void CalculateMeanMedian()
        {
            var billAmounts = new List<decimal>();

            using (var reader = new StreamReader("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\bills.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    billAmounts.Add(decimal.Parse(parts[2]));
                }
                //Program uses the Average() method from the System.Linq namespace to calculate the mean of the bill amounts.
                var mean = billAmounts.Average();
                Console.WriteLine("Mean: " + mean);


                billAmounts.Sort();
                var mid = billAmounts.Count / 2;
                var median = billAmounts.Count % 2 == 0 ? (billAmounts[mid - 1] + billAmounts[mid]) / 2 : billAmounts[mid];
                Console.WriteLine("Median: " + median);
            }
        }
    }

}