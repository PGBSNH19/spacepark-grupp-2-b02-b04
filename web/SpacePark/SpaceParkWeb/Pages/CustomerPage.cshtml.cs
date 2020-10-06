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

        public async Task OnGet(Person person)
        {
            restSharpCaller = new RestSharpCaller();
            if (person.SpaceshipID == null)
            {
                Customer = person;
                Customer.Spaceships = await restSharpCaller.GetSpaceships(person.Name);
                SelectedList = new SelectList(Customer.Spaceships.Select(x => x.Name));

            }
            else
            {
                Customer = person;
                Customer.Spaceships = new List<Spaceship>();
                Customer.Spaceship = await restSharpCaller.GetSpaceshipById(person.SpaceshipID ?? default(int));
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
                Person person = await restSharpCaller.GetPerson(customerName);
                person.Spaceships = await restSharpCaller.GetSpaceships(customerName);
                Spaceship spaceship = await restSharpCaller.PostSpaceship(person.Spaceships.Where(x => x.Name == spaceshipName).FirstOrDefault());
                person.SpaceshipID = spaceship.SpaceshipID;
                person.Spaceship = spaceship;
                Person updatedPerson = await restSharpCaller.PutPerson(person);
                Spaceship parkedSpaceship = await restSharpCaller.ParkSpaceship(spaceship);
            }

            return new RedirectToPageResult("Index");
        }

    }

}
