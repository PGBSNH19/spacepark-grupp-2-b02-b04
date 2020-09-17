using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace SpaceParkWeb.Pages
{
    public class CreateCustomerModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string customer = Request.Form["name"];
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var request = new RestRequest($"person?name={customer}", Method.POST);
            var apiResponse = client.ExecuteAsync(request);
            apiResponse.Wait();

            return new OkResult();
        }

    }
}
