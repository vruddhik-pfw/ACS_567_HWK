using HWK4.Data;
using HWK4.Models;
using System;

public class Seed
{
    /// <summary>
    /// 
    ///  Seed method in C# is typically used as a method to populate data when the database is first created. 
	///  Method is called in the context of a database migration or setup process.
    /// </summary>
    private readonly DataContext dataContext;
	public Seed(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}
	public void SeedDataContext()
	{
		if(!dataContext.MonthlyBill.Any())
		{
			//Monthly bill data
			List<MonthlyBill> bill = new()
			{
				new MonthlyBill {Id = 9, Provider = "Provider9", Bill = "Water", Amount = 80, IsCompleted = false},
				new MonthlyBill {Id = 10, Provider = "Provider10", Bill = "Gas", Amount = 60, IsCompleted = false}
			};
			dataContext.MonthlyBill.AddRange(bill);
			dataContext.SaveChanges();
		}
	}

}
	
