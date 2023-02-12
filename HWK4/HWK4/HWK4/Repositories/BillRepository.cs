
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;


namespace HWK4.Repositories
{
    /// <summary>
	/// instead of performing the operations directly,
	/// it uses an instance of the BillRepository interface to perform the operations(read,delete,edit)
	/// </summary>
    public class BillRepository : IBillRepository
    {
        private DataContext _context;

        public BillRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
		/// The getItems method reads all the contents of a text file 
		/// </summary>
		/// <returns>returns it in the response.</returns>
        public ICollection<MonthlyBill> getItems()
        {
            return _context.MonthlyBill.ToList();
        }
        /// <summary>
        /// The GetItem method reads all the contents of a text file 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns response based on the id</returns>
        public MonthlyBill GetItem(int id)
        {
            return _context.MonthlyBill.Where(bill => bill.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// The MonthlyBillExists method checks if bill exists or not 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if bill exists</returns>
        public bool MonthlyBillExists(int id)
        {
            return _context.MonthlyBill.Any(bill => bill.Id == id); 
        }

        /// <summary>
		/// The addItem method adds to the response
		/// </summary>
		/// <param name="bill"></param>
		/// <returns>returns true if successfully added</returns>
        public bool addItem(MonthlyBill bill)
        {
            _context.Add(bill);
            return Save();
        }

        /// <summary>
		/// The editItem method updated the content based on id with specified content
		/// </summary>
		/// <param name="bill"></param>
		/// <returns>returns true if updated successfully</returns>
        /// <summary>
        public bool editItem(MonthlyBill bill)
        {
            _context.Update(bill);
            return Save();

        }
        /// <summary>
		/// The deleteItem method deletes request based on the specified id
		/// </summary>
		/// <param name="bill"></param>
		/// <returns>returns item deleted and null if not found </returns>
        public bool deleteItem(MonthlyBill bill)
        {
            _context.Remove(bill);
            return Save();
        }


        /// <summary>
        /// Data Analysis method to calculate min,max and average of the amount
        /// </summary>
        /// <returns>Returns min, max,average of the amount </returns>
        public DataAnalysis DataAnalysis()
        {
            //System.Diagnostics.Debug.WriteLine("Context:"+_context);

            var result = new List<double>();
            foreach (MonthlyBill i in _context.MonthlyBill)
            {
                result.Add(i.Amount);
                //System.Diagnostics.Debug.WriteLine("Monthly bill" + i);
                //if (i.Amount >= max)
                //{
                //    bill = i;
                //    max = i.Amount;
                //}
            }

            DataAnalysis dataAnalysis = new DataAnalysis();

            dataAnalysis.min = result.Min();
            dataAnalysis.max = result.Max();
            dataAnalysis.average = result.Average();

            return dataAnalysis;
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        /// <returns>Store in database</returns>
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved == 1;

        }

        
    }

}

