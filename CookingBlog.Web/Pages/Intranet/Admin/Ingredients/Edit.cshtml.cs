using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.ViewComponents;
using CookingBlog.Web.Lib;

namespace CookingBlog.Web.Pages.Intranet.Admin.Ingredients
{
    public class EditModel(CookingBlogContext ctx) : PageModel
    {
        public Guid IngredientId { get; set; }

        public string StatusMessage { get; set; }

        private readonly CookingBlogContext _context = ctx;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            IngredientId = new Guid(HttpContext.Request.Query["id"]);

            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid model state.";
                return Page();
            }

            try
            {
                var model = IngredientFormViewComponent.BuildFormModel(Request.Form);

                new IngredientSaver(IngredientId, model, _context);
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;

                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool IngredientExists(Guid id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
