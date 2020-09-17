using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SpaceParkWeb.Pages
{
    public class NewCustomerModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string customer = Request.Form["name"];
            string spaceShip = Request.Form["spaceship"];


            return new OkResult();
        }
    }
}
