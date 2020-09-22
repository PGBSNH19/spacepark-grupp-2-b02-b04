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
            Customer.Spaceships = AddSpaceshipsToPerson(person);

            if(Customer.Name == null)
            {
                return;
            }
            else
            {
                SelectedList = new SelectList(Customer.Spaceships.Select(x => x.Name));
            }

            

            
        }
        public async Task<Spaceship> GetSpaceshipData(string input)
        {
            var client = new RestClient(input);
            var request = new RestRequest("", DataFormat.Json);
            var apiResponse = await client.ExecuteAsync<Spaceship>(request);

            return apiResponse.Data;
        }

        private List<Spaceship> AddSpaceshipsToPerson(Person person)
        {
            person.Spaceships = new List<Spaceship>();
            foreach (var spaceshipUrl in person.Starships)
            {
                person.Spaceships.Add(GetSpaceshipData(spaceshipUrl).Result);
            }
            return person.Spaceships;
        }
    }
    
}
