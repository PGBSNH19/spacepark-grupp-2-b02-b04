using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using SpaceParkWeb.Models;
using SpaceParkWeb.Serializer;

namespace SpaceParkWeb.Pages
{
    public class CustomerPageModel : PageModel
    {
        [BindProperty]
        public Person Customer { get; set; }
        public bool ParkedSpaceship { get; set; }
        public SelectList SelectedList { get; set; }

        public void OnGet(Person person)
        {
            Customer = person;
            Customer.Spaceships = GetSpaceships(person.Name).Result;

            if(Customer.Name == null)
            {
                return;
            }
            else
            {
                SelectedList = new SelectList(Customer.Spaceships.Select(x => x.Name));
            } 
        }

        public IActionResult OnPost(string name)
        {
            return null;
        }
        public async Task<List<Spaceship>> GetSpaceships(string input)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/GetSpaceShips?name={input}"
            };
            var result = await client.GetAsync<List<Spaceship>>(request);
            return result;
        }
    }
    
}
