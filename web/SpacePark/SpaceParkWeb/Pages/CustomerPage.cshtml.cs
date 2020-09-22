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

        public IActionResult OnPost(Person person)
        {
            string spaceshipName = Request.Form["spaceships"];
            var customer = person;
            //Spaceship spaceship = Customer.Spaceships.Where(x => x.Name == spaceshipName).FirstOrDefault();
            //var space = await PostSpaceship(spaceship);
            //Person person = PutPerson(Customer).Result;


            return null;
        }

        public async Task<Spaceship> PostSpaceship(Spaceship spaceship)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/parkship?name={spaceship.Name}&length?={spaceship.Length}"
            };
            var result = await client.PostAsync<Spaceship>(request);

            return result;     
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

        public async Task<Person> PutPerson(Person person)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person"
            };
            var result = await client.PutAsync<Person>(request);
            return result;
        }
    }
    
}
