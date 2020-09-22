using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceParkWeb.Models;

namespace SpaceParkWeb.Pages
{
    public class CustomerPageModel : PageModel
    {
        [BindProperty]
        public Person Customer { get; set; }
        public bool ParkedSpaceship { get; set; }
        public List<Spaceship> SpaceShips { get; set; }
        public SelectList selectedList;

        public void OnGet(Person person)
        {
            Customer = person;

            if(Customer.Name == null)
            {
                return;
            }
            else
            {
                SpaceShips = new List<Spaceship>();
                SpaceShips.Add(new Spaceship("X-Wing", "200", 5));
                SpaceShips.Add(new Spaceship("X-Wing2", "200", 5));
                SpaceShips.Add(new Spaceship("X-Wing3", "200", 5));
                selectedList = new SelectList(SpaceShips.Select(x => x.Name));
            }

            
        }
    }
}
