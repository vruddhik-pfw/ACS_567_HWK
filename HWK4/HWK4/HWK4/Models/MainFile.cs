
using System;
using System.Runtime.CompilerServices;

namespace HWK4.Models
{
    public class MonthlyBill
    {
        /// <summary>
		/// Added getters and setters for all the input var
		/// </summary>
        /// 
        public int Id { get; set; }

        public string Provider { get; set; } = String.Empty;

        public string Bill { get; set; } = String.Empty;

        public double Amount { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}

