using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using SpaceParkWeb.Models;

namespace SpaceParkWeb.Pages
{
    public class LoginCustomer : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string input = Request.Form["name"];
            
            if(input != null)
            {
                var customer = GetCustomer(input);
            }
            
            return new OkResult();
        }

        public IRestResponse<Person> GetCustomer(string input)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var request = new RestRequest($"person?name={input}", DataFormat.Json);
            //var apiResponse = client.Execute(request);
            var result = client.Get<Person>(request);

            return result;
        }

    }
}
