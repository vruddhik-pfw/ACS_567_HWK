using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Repositories;

// The class is defined as a public class within the MonthlyBillWebApp.Pages.Bills namespace
namespace MonthlyBillWebApp.Pages.Bills
{
    /// <summary>
    /// // The public class is defined as a PageModel
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        ///  A public MonthlyBill object is created
        /// </summary>
        public HWK4.Models.MonthlyBill monthlyBill = new();
        public string errorMessage = "";
        public string successMessage = "";

        /// <summary>
        /// 
        ///  The OnPost method is defined as an async void method
        /// </summary>
        public async void OnPost()
        {
            //The values from the form are set to the monthlyBill object
            monthlyBill.Id = int.Parse(Request.Form["id"]);
            monthlyBill.Provider = Request.Form["provider"];
            monthlyBill.Bill = Request.Form["bill"];
            monthlyBill.Amount = double.Parse(Request.Form["amount"]);

            if (monthlyBill.Provider.Length == 0)
            {
                errorMessage = "Provider is required";
            }
            else
            {
                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string json = System.Text.Json.JsonSerializer.Serialize<MonthlyBill>(monthlyBill, opt);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5120");

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var result = await client.PostAsync("MonthlyBill", content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);
                    // If the server returns an error, an error message is displayed
                    if (!result.IsSuccessStatusCode)
                    {
                        errorMessage = "Error adding";
                    }
                    else
                    {
                        // If the server returns a success message, a success message is displayed
                        successMessage = "Successfully added";
                    }

                }
            }
        }
    }
}
