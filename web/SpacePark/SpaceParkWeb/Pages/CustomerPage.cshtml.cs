using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpaceParkWeb.Models;

namespace SpaceParkWeb.Pages
{
    public class CustomerPageModel : PageModel
    {
        [BindProperty]
        public Person customer { get; set; }
        public bool parkedSpaceship { get; private set; }

        public void OnGet(Person person)
        {
            customer = person;

            if(customer.SpaceshipID != 0)
            {
                parkedSpaceship = true;
            }
            
        }
    }
}
