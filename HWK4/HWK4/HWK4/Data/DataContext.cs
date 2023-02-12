using Microsoft.EntityFrameworkCore;
using HWK4.Models;

namespace HWK4.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// 
        /// DataContext instance, you can execute SQL 
        ///  commands to insert, update, and delete data in the database.
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}
        public DbSet<MonthlyBill> MonthlyBill{ get; set; }
    }
}

