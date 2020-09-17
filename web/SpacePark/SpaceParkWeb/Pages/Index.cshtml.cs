using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SpaceParkWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string newCustomer = Request.Form["newcustomerchoice"];
            string oldCustomer = Request.Form["oldcustomerchoice"];
            if (newCustomer != null)
            {
                return new RedirectToPageResult("NewCustomer");
            }
            else if(oldCustomer != null)
            {
                return new RedirectToPageResult("OldCustomer");
            }
            else
            {
                return new RedirectToPageResult("Index");
            } 
        }
    }
}
