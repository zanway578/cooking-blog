using CookingBlog.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBlog.Web.Pages
{
    public class RecipePrintModel(CookingBlogContext ctx) : PageModel
    {
        private CookingBlogContext _ctx = ctx;

        public Guid RecipeId { get; set; }

        public IActionResult OnGet([FromRoute] string? recipeName = null, [FromRoute] string? urlId = null)
        {

            try
            {
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
