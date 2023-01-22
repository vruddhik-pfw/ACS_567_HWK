using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace HWK1B
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             *  C# console application that uses a menu-driven approach to read data
             *  from a text file, add data to the file, and filter the data and doing
             *  some data analysis
             */
            while (true)
            {
                Console.WriteLine("1. Read data from file");
                Console.WriteLine("2. Add data to the file");
                Console.WriteLine("3. Filter data method 1");
                Console.WriteLine("4. Filter data method 2");
                Console.WriteLine("5. Data Analysis");
                Console.WriteLine("6: Exit");
                Console.Write("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                /*
                 * Switch case for read data to file, add data to the file,
                 * filter data from the file and perform analysis 
                 */
                switch (choice)
                {
                    case 1:
                        ReadDataFromFile();
                        break;
                    case 2:
                        AddDataToFile();
                        break;
                    case 3:
                        FilterDataMethod1();
                        break;
                    case 4:
                        FilterDataMethod2();
                        break;
                    case 5:
                        DataAnalysis();
                        break;
                    case 6:
                        return;

                }
            }
        }

        /*
         * Function for reading the data from file
         */
        static void ReadDataFromFile()
        {
            /*
             * try-catch blocks to handle any exceptions that might occur 
             * when reading data from the text file
             */
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Vruddhi\Desktop\ACS_567_HWK\HWK1B\HWK1B\HWK1B\demo.txt");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }

        /*
         * Function for adding the data to the file
         */
        static void AddDataToFile()
        {
            Console.Write("Enter data to add: ");
            string data = Console.ReadLine();

            /*
             * try-catch blocks to handle any exceptions that might occur 
             * when adding data to the text file
             */
            try
            {
                File.AppendAllText("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\demo.txt", Environment.NewLine + data);
                Console.WriteLine("Data added successfully!");
            }
            catch (IOException)
            {
                Console.WriteLine("Error occured while adding data to file!");
            }
        }

        /*
         * Function for searching/filtering the data from file
         */
        static void FilterDataMethod1()
        {
            Console.Write("Enter filter keyword: ");
            string keyword = Console.ReadLine();
            /*
             * try-catch blocks to handle any exceptions that might occur 
             * when searching data from the text file
             */
            try
            {
                string[] lines = File.ReadAllLines("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\demo.txt");
                var filteredLines = from line in lines
                                    where line.Contains(keyword)
                                    select line;

                foreach (string line in filteredLines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }

        /*
         * Function for searching/filtering the data from file
         * This function takes input and displays firstname and lastname
         */
        static void FilterDataMethod2()
        {
            Console.Write("Enter filter keyword: ");
            string keyword = Console.ReadLine();
                /*
                 * try-catch blocks to handle any exceptions that might occur 
                 * when searching data from the text file
                 */
                try
            {
                string[] lines = File.ReadAllLines("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\demo.txt");
                var filteredLines = lines.Where(x => x.Contains(keyword));
                int firstNameIndex = 0;
                int lastNameIndex = 1;
                foreach (string line in filteredLines)
                {
                    string[] columns = line.Split(',');
                    Console.WriteLine(columns[firstNameIndex]+columns[lastNameIndex]);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
        }

        /*
         * Function for data analysis on the data like mean, max, median
         */
        static void DataAnalysis()
        {

            try
            {
                // Assume the file is a text file with columns separated by ','
                string[] lines = File.ReadAllLines("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK1B\\HWK1B\\HWK1B\\demo.txt");
                int columnIndex = 3; // The column index to read, 0-based
                float sum = 0;
                int count = 0;
                float max = 0;
                float[] arr = new float[30];
                foreach (string line in lines)
                {
                    string[] columns = line.Split(',');
                    float result = float.Parse(columns[columnIndex]);
                    /*
                     * Max calculation
                     */
                    if (result > 0)
                    {
                        if (result > max)
                        {
                            max = result;
                        }
                        sum = sum + result;
                        arr[count] = result;
                        count++;
                    }
                }
                arr = arr.Where(x => x > 0).ToArray();
                /*
                 * Median Calculation
                 */
                float median;
                Array.Sort(arr);
                if (arr.Length % 2 == 0)
                {
                    median = (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
                }
                else
                {
                    median = arr[arr.Length / 2];
                }

                /*
                 * Mean calculation
                 */
                float mean = sum / count;

                Console.WriteLine("Maximum: {0}", max);
                Console.WriteLine("Mean: {0}", mean);
                Console.WriteLine("Median: {0}", median);


            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Data in file is not in the correct format.");
            }
        }
    }
}