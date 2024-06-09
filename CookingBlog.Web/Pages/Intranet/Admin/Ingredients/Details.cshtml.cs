using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBlog.Database;
using CookingBlog.Database.Models;

namespace CookingBlog.Web.Pages.Intranet.Admin.Ingredients
{
    public class DetailsModel : PageModel
    {
        private readonly CookingBlog.Database.CookingBlogContext _context;

        public DetailsModel(CookingBlog.Database.CookingBlogContext context)
        {
            _context = context;
        }

        public CookingBlog.Database.Models.Ingredient Ingredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }
            else
            {
                Ingredient = ingredient;
            }
            return Page();
        }
    }
}
