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

        private RestSharpCaller restSharpCaller;

        public void OnGet(Person person)
        {
            restSharpCaller = new RestSharpCaller();
            if (person.SpaceshipID == null)
            {
                Customer = person;
                Customer.Spaceships = restSharpCaller.GetSpaceships(person.Name).Result;
                SelectedList = new SelectList(Customer.Spaceships.Select(x => x.Name));
            }
            else
            {
                Customer = person;
                Customer.Spaceships = new List<Spaceship>();
                Customer.Spaceship = restSharpCaller.GetSpaceshipById(person.SpaceshipID ?? default(int)).Result;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            restSharpCaller = new RestSharpCaller();
            if (int.TryParse(Request.Form["spaceshipid"], out int SpaceshipId))
            {
                await restSharpCaller.CheckoutShip(SpaceshipId);
            }
            else
            {
                string customerName = Request.Form["customer"];
                string spaceshipName = Request.Form["spaceships"];
                Person person = restSharpCaller.GetPerson(customerName).Result;
                person.Spaceships = restSharpCaller.GetSpaceships(customerName).Result;
                Spaceship spaceship = restSharpCaller.PostSpaceship(person.Spaceships.Where(x => x.Name == spaceshipName).FirstOrDefault()).Result;
                person.SpaceshipID = spaceship.SpaceshipID;
                person.Spaceship = spaceship;
                Person updatedPerson = restSharpCaller.PutPerson(person).Result;
                Spaceship parkedSpaceship = restSharpCaller.ParkSpaceship(spaceship).Result;
            }

            return new RedirectToPageResult("Index");
        }

    }

}
