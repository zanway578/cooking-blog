using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Database.Models.Enums;
using CookingBlog.Database.Models.Views;
using CookingBlog.Database.Views;
using CookingBlog.Web.Lib;
using CookingBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CookingBlog.Web.Pages.Intranet.Admin.Recipes
{
    public class PreviewModel(CookingBlogContext ctx) : PageModel
    {
        private readonly CookingBlogContext _ctx = ctx;

        public Guid RecipeId { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                RecipeId = new Guid(HttpContext.Request.Query["id"]); 

                return Page();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        
    }
}
