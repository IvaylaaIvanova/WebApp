using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public Customer Customer { get; set; }

        [TempData]
        public string Message { get; set; }

        private ILogger<CreateModel> _log;

        public CreateModel(AppDbContext db, ILogger<CreateModel> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<IActionResult> OnPostAsync()   // tozi method obrabotva metoda post
        {
            if (!ModelState.IsValid)         // ako izpratenite danni sa korektni
            {
                return Page();
            }
            _db.Customers.Add(Customer);     // dobavq customer samo v pametta na aplikaciqta, ako izpratenite danni sa korektni

            await _db.SaveChangesAsync();    // zapisva dannite realno v bazata danni, a ne samo v pametta na aplikaciqta, kakto gorniq red

            var msg = $"Customer { Customer.Name} added."; 
            Message = msg; 
            _log.LogCritical(msg);

            return RedirectToPage("/Index");
        }
    }
}