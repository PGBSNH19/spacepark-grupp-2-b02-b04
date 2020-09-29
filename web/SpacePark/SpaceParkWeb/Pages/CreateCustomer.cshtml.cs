using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using SpaceParkWeb.Models;
using SpaceParkWeb.Serializer;

namespace SpaceParkWeb.Pages
{
    public class CreateCustomerModel : PageModel
    {
        private RestSharpCaller restSharpCaller;
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            restSharpCaller = new RestSharpCaller();
            string input = Request.Form["name"];
            var customer = restSharpCaller.PostPerson(input).Result;
            if (customer != null)
            {
                return new RedirectToPageResult("CustomerPage", customer);
            }
            else
            {
                return new RedirectToPageResult("CreateCustomer");
            }

        }  
    }
}
