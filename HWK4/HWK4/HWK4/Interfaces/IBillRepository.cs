using System;
using HWK4.Models;
namespace HWK4.Interfaces
{
    public interface IBillRepository
    {
        ICollection<MonthlyBill> getItems();

        MonthlyBill GetItem(int id);

        bool MonthlyBillExists(int id);

        bool addItem(MonthlyBill bill);

        bool editItem(MonthlyBill bill);

        bool deleteItem(int id);

        DataAnalysis DataAnalysis();
        bool Save();
    }
}
