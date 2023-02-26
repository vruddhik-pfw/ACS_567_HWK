using HWK4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NumSharp;
using NumSharp.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MonthlyBillWebApp.Pages.Bills
{
    public class AnalyseModel : PageModel
    {
        // Create a new instance of the MonthlyBill class

        public HWK4.Models.MonthlyBill monthlyBill = new();
        public void OnGet()
        {
            // This method is called when the page is requested via HTTP GET request


        }
        public async void OnPost()
        {
            var amounts = new List<float>();
            
            // Calculate the maximum value
            var max = amounts.Max();

            // Pass the result to the view
            ViewData["Max"] = max;

        }
    }
}
