using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Repositories;

namespace MonthlyBillWebApp.Pages.Bills
{
    /// <summary>
    /// The DeleteModel class inherits from the 
    /// PageModel class and contains three public fields: monthlyBill, errorMessage, and successMessage.
    /// </summary>

    public class DeleteModel : PageModel
    {
        public MonthlyBill monthlyBill = new();
        public string errorMessage = "";
        public string successMessage = "";

        /// <summary>
        /// The OnGet method is called when the page is loaded and 
        /// uses the Request.Query property to get the ID of the MonthlyBill object to delete.
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
        /// The OnPost method is called when the user submits the form to delete the MonthlyBill object.
        /// </summary>
        public async void OnPost()
        {
            bool isDeleted = false;
            int id = int.Parse(Request.Form["id"]);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");

                var response = await client.DeleteAsync("/MonthlyBill/" + id);

                if (response.IsSuccessStatusCode)
                {
                    isDeleted = true;
                }
            }
            if (isDeleted)
            {
                successMessage = "Successfully deleted";
            }
            else
            {
                errorMessage = "Error deleting";
            }

        }
    }
}
