using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Repositories;

namespace MonthlyBillWebApp.Pages.Bills
{
    public class IndexModel : PageModel
    {
        public List<MonthlyBill> monthlyBills = new();
        /// <summary>
        /// The OnGet method is responsible for getting the data for this property 
        /// from the API using HTTP GET request
        /// </summary>
        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");
                //HTTP GET
                var responseTask = client.GetAsync("/MonthlyBill");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    monthlyBills = JsonConvert.DeserializeObject<List<MonthlyBill>>(readTask);
                }
            }
        }
    }
}
