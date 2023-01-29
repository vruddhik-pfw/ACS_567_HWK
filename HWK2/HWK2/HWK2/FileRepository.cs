using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TodoRestAPI
{

	/// <summary>
	/// instead of performing the operations directly,
	/// it uses an instance of the FileRepository interface to perform the operations(read,delete,edit)
	/// </summary>
	public class TodoRepository
	{
		private static TodoRepository instance;
		private List<Todo> items;

		public TodoRepository()
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
						items.Add(new Todo(provider, bill, amount));
					}
				}
			}

		}

		public static TodoRepository getInstance()
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
		public List<Todo> getItems()
		{
			return items;
		}

		/// <summary>
		/// The GetItem method reads all the contents of a text file 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>returns response based on the id</returns>
		public Todo GetItem(int id)
		{
			Todo todo = null;

			foreach (Todo t in items)
			{
				if (id == t.Id)
				{
					todo = t;
					break;
				}
			}
			return todo;
		}
		/// <summary>
		/// The addItem method adds to the response
		/// </summary>
		/// <param name="todo"></param>
		/// <returns>returns true if successfully added</returns>
		public bool addItem(Todo todo)
		{
			bool isAdded = true;
			foreach (Todo t in items)
			{
				if (t.Id == todo.Id)
				{
					isAdded = false;
					break;
				}
			}
			if (isAdded)
			{
				items.Add(todo);
			}
			return isAdded;

		}
		/// <summary>
		/// The editItem method updated the content based on id with specified content
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updated"></param>
		/// <returns>returns true if updated successfully</returns>
		public bool editItem(int id, Todo updated)
		{
			bool isEdited = false;

			foreach (Todo t in items)
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
		/// <returns>returns Todo deleted and null if not found </returns>
		public bool deleteItem(int id)
		{
			Todo delete = null;

			foreach (Todo t in items)
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