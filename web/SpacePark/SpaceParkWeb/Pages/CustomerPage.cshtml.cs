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
            if (person.SpaceshipID == null)
            {
                Customer = person;
                Customer.Spaceships = GetSpaceships(person.Name).Result;
                SelectedList = new SelectList(Customer.Spaceships.Select(x => x.Name));
            }
            else
            {
                Customer = person;
                Customer.Spaceships = new List<Spaceship>();
                Customer.Spaceship = GetSpaceshipById(person.SpaceshipID ?? default(int)).Result;
            }
        }

        public IActionResult OnPost()
        {
            
            if(int.TryParse(Request.Form["spaceshipid"], out int SpaceshipId))
            {
                CheckoutShip(SpaceshipId);
            }
            else
            {
                string customerName = Request.Form["customer"];
                string spaceshipName = Request.Form["spaceships"];
                Person person = GetPerson(customerName).Result;
                person.Spaceships = GetSpaceships(customerName).Result;
                Spaceship spaceship = PostSpaceship(person.Spaceships.Where(x => x.Name == spaceshipName).FirstOrDefault()).Result;
                person.SpaceshipID = spaceship.SpaceshipID;
                person.Spaceship = spaceship;
                Person updatedPerson = PutPerson(person).Result;
                Spaceship parkedSpaceship = ParkSpaceship(spaceship).Result;
            }
            
            return new RedirectToPageResult("Index");
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
                Resource = $"spaceship/postspaceship"
            };
            request.AddJsonBody(spaceship);
            var result = await client.PostAsync<Spaceship>(request);

            return result;
        }

        public async Task<Spaceship> ParkSpaceship(Spaceship spaceship)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);
            client.AddHandler("text/json", jsonSerializer);
            client.AddHandler("text/x-json", jsonSerializer);
            client.AddHandler("text/javascript", jsonSerializer);
            client.AddHandler("*+json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/parkspaceship"
            };
            request.AddJsonBody(spaceship);
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

        public async Task<Spaceship> GetSpaceshipById(int id)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/{id}"
            };
            var result = await client.GetAsync<Spaceship>(request);
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
            request.AddJsonBody(person);
            var result = await client.PutAsync<Person>(request);
            return result;
        }

        public async Task<Person> GetPerson(string input)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person?name={input}"
            };

            var result = await client.GetAsync<Person>(request);
            return result;
        }

        public async void CheckoutShip(int shipId)
        {
            var client = new RestClient($"https://localhost:44386/api/v1.0/");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/{shipId}"
            };

            await client.DeleteAsync<Spaceship>(request);
        }
    }

}
