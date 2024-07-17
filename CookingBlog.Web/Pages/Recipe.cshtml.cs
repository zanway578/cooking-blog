using CookingBlog.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Identity.Client;

namespace CookingBlog.Web.Pages
{
    public class RecipeModel(CookingBlogContext ctx) : PageModel
    {
        private CookingBlogContext _ctx = ctx;

        public Guid RecipeId { get; set; }

        public string RecipeName { get; set; }

        public string UrlId { get; set; }

        public IActionResult OnGet([FromRoute] string recipeName, [FromRoute] string urlId)
        {

            try
            {
                RecipeName = recipeName;
                UrlId = urlId;

                var recipe = _ctx
                    .Recipes
                    .Where(r => r.UrlId == urlId)
                    .First();

                RecipeId = recipe.Id;

                if (!recipe.IsPublished)
                {
                    return NotFound();
                }
                else
                {
                    return Page();
                }
                
            }
            catch
            {
                return NotFound();
            }
        }
    }
}