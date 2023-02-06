using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MonthlyBillRestAPI
{

	/// <summary>
	/// instead of performing the operations directly,
	/// it uses an instance of the FileRepository interface to perform the operations(read,delete,edit)
	/// </summary>
	public class BillRepository
	{
		private static BillRepository instance;
		private List<MonthlyBill> items;

		public BillRepository()
		{
			///Reads text file 
			String filepath = "C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK2\\HWK2\\HWK2\\bills.txt";
			items = new();
			if (File.Exists(filepath))
			{
				string[] lines = File.ReadAllLines(filepath);
				foreach (string line in lines)
				{
					System.Diagnostics.Debug.WriteLine("Hello");
					if (line != null)
					{
						var values = line.Split(',');
						string provider = values[0];
						string bill = values[1];
						double amount = double.Parse(values[2]);
						items.Add(new MonthlyBill(provider, bill, amount));
					}
				}
			}

		}

		public static BillRepository getInstance()
		{
			{
				if (instance == null)
					instance = new();
			}
			return instance;
		}

		/// <summary>
		/// The getItems method reads all the contents of a text file 
		/// </summary>
		/// <returns>returns it in the response.</returns>
		public List<MonthlyBill> getItems()
		{
			return items;
		}

		/// <summary>
		/// The GetItem method reads all the contents of a text file 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>returns response based on the id</returns>
		public MonthlyBill GetItem(int id)
		{
            MonthlyBill bill = null;

			foreach (MonthlyBill t in items)
			{
				if (id == t.Id)
				{
					bill = t;
					break;
				}
			}
			return bill;
		}
		/// <summary>
		/// The addItem method adds to the response
		/// </summary>
		/// <param name="todo"></param>
		/// <returns>returns true if successfully added</returns>
		public bool addItem(MonthlyBill bill)
		{
			bool isAdded = true;
			foreach (MonthlyBill t in items)
			{
				if (t.Id == bill.Id)
				{
					isAdded = false;
					break;
				}
			}
			if (isAdded)
			{
				items.Add(bill);
			}
			return isAdded;

		}
		/// <summary>
		/// The editItem method updated the content based on id with specified content
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updated"></param>
		/// <returns>returns true if updated successfully</returns>
		public bool editItem(int id, MonthlyBill updated)
		{
			bool isEdited = false;

			foreach (MonthlyBill t in items)
			{
				if (t.Id == id)
				{
					t.Provider = updated.Provider;
					t.Bill = updated.Bill;
					t.Amount = updated.Amount;
					t.IsCompleted = updated.IsCompleted;
					isEdited = true;
					break;
				}
			}
			return isEdited;
		}
		/// <summary>
		/// The deleteItem method deletes request based on the specified id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>returns item deleted and null if not found </returns>
		public bool deleteItem(int id)
		{
            MonthlyBill delete = null;

			foreach (MonthlyBill t in items)
			{
				if (id == t.Id)
				{
					delete = t;
					break;
				}
			}
			if (delete != null)
			{
				items.Remove(delete);
			}
			return delete == null;
		}

		/// <summary>
		/// The GetMinMaxMean method calculate min,max and mean of the amount
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns>return min,max,mean of the amount</returns>
        public static (double min, double max, double mean) GetMinMaxMean(string filePath)
        {

            string[] lines = File.ReadAllLines("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK2\\HWK2\\HWK2\\bills.txt");
            var result = new List<double>();
            int columnIndex = 2;
			foreach (string line in lines)
			{
                var parts = line.Split(',');
                result.Add(double.Parse(parts[2]));
            }
            return (result.Min(), result.Max(), result.Average());
        }



    }
}