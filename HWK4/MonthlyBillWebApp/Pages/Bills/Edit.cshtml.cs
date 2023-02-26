using HWK4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json;
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Repositories;
using static System.Net.WebRequestMethods;

namespace MonthlyBillWebApp.Pages.Bills
{
    public class EditModel : PageModel
    {
        public MonthlyBill monthlyBill = new();
        public string errorMessage = "";
        public string successMessage = "";
        /// <summary>
        /// In the OnGet method, the code retrieves the ID of the monthly bill
        /// to be edited from the request query parameters.
        /// </summary>
        public async void OnGet()
        {
            string id = Request.Query["id"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");

                // HTTP GET
                var responseTask = client.GetAsync("/MonthlyBill/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    monthlyBill = JsonConvert.DeserializeObject<MonthlyBill>(readTask);
                }
            }
        }
        /// <summary>
        /// In the OnPost method, the code updates the properties of the 
        /// monthlyBill object using values from the request form data. 
        /// </summary>
        public async void OnPost()
        {
            monthlyBill.Id = int.Parse(Request.Form["id"]);
            monthlyBill.Provider = Request.Form["provider"];
            monthlyBill.Bill = Request.Form["bill"];
            monthlyBill.Amount = double.Parse(Request.Form["amount"]);
            monthlyBill.IsCompleted = Request.Form["isCompleted"] == "on";

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

                    var result = await client.PutAsync("/MonthlyBill", content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);

                    if (!result.IsSuccessStatusCode)
                    {
                        errorMessage = "Error editing";
                    }
                    else
                    {
                        successMessage = "Successfully editing";
                    }

                }

            }
        }

    }
}
