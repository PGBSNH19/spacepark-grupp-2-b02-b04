using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using SpaceParkWeb.Models;
using SpaceParkWeb.Serializer;

namespace SpaceParkWeb.Pages
{
    public class CreateCustomerModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string input = Request.Form["name"];
            var customer = PostPerson(input);
            //var client = new RestClient($"https://localhost:44386/api/v1.0/");
            //var request = new RestRequest($"person?name={input}", Method.POST);
            //IRestResponse apiResponse = client.Execute(request);
            if (customer != null)
            {
                return new RedirectToPageResult("LoginCustomer");
            }
            else
            {
                return new RedirectToPageResult("CreateCustomer");
            }

        }

        public async Task<IRestResponse> PostPerson(string input)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var request = new RestRequest($"person?name={input}", Method.POST);
            var apiResponse = client.ExecuteAsync(request);

            return await apiResponse;
        }

      
    }
}
