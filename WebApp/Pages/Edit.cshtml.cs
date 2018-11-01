using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Customer Customer { get; set; } 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _db.Customers.FindAsync(id); // namirame customer v bazata s danni

            if (Customer == null)   // ako customer ne e nameren
            {
                return RedirectToPage("/Index"); // vrushta kum nachalnata stranica
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            _db.Attach(Customer).State = EntityState.Modified; 

            try  
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) // dobavqme e - suobshteneto
            {
                throw new Exception($"Customer {Customer.Id} not found!", e); // dobavqme e - suobshteneto
            }

            return RedirectToAction("./Index");         
        }
    }
}