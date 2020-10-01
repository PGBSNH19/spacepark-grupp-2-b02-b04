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
        private RestSharpCaller restSharpCaller;
        public Person Person { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            string input = Request.Form["name"];
            restSharpCaller = new RestSharpCaller();
            var customer = await restSharpCaller.GetCustomer(input);
            if (customer.Name != null)
            {
                Person = new Person(customer.PersonID, customer.Name, customer.SpaceshipID ?? default(int), customer.Spaceships, customer.Spaceship);
                return new RedirectToPageResult("CustomerPage", Person);
            }
            else
            {
                return new RedirectToPageResult("LoginCustomer");
            }

            
        }
    }
}
