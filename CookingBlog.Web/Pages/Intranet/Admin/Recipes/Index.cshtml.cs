using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBlog.Database;
using CookingBlog.Database.Models;

namespace CookingBlog.Web.Pages.Intranet.Admin.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly CookingBlog.Database.CookingBlogContext _context;

        public IndexModel(CookingBlog.Database.CookingBlogContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Recipe = await _context.Recipes.Take(15).ToListAsync();
        }

        public IActionResult OnPost()
        {
            var searchText = "%" + Request.Form["search-text"].First() + "%";

            Recipe = _context
                .Recipes
                .Where(i => EF.Functions.Like(i.Name, searchText))
                .ToList();

            return Page();
        }
    }
}
