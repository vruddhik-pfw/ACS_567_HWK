using System;
using System.Runtime.CompilerServices;

namespace TodoRestAPI
{
	public class Todo
	{
		private static int nextId = 1;
				
		/// <summary>
		/// Taking inputs 
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="bill"></param>
		/// <param name="amount"></param>
		public Todo(String provider,String bill, double amount)
		{
			Provider = provider;
			Bill = bill;	
			Amount = amount;
			IsCompleted = false;
			Id = nextId++;

		}
		/// <summary>
		/// Added getters and setters for all the input var
		/// </summary>
		public int Id { get; set; }

		public string Provider { get; set; }

        public string Bill { get; set; }

        public double Amount { get; set; }

        public bool IsCompleted { get; set; }
	}
}