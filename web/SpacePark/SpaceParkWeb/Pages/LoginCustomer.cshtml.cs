using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using SpaceParkWeb.Models;
using SpaceParkWeb.Serializer;

namespace SpaceParkWeb.Pages
{
    public class LoginCustomer : PageModel
    {
        public Person Person { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string input = Request.Form["name"];
            
            if(input != null)
            {
                var customer = GetCustomer(input);
                Person = new Person(customer.Result.PersonID, customer.Result.Name, customer.Result.SpaceshipID ?? default(int), customer.Result.Spaceships, customer.Result.Spaceship);
            }
            
            return new RedirectToPageResult("CustomerPage", Person);
        }

        public async Task<Person> GetCustomer(string input)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);
            
            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person/searchname?name={input}"
            };

            var result = await client.GetAsync<Person>(request);
            return result;
        }

    }
}
