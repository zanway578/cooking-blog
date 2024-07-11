using CookingBlog.Database;
using CookingBlog.Database.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBlog.Web.Pages
{
    public class SearchModel(CookingBlogContext ctx) : PageModel
    {
        private CookingBlogContext _ctx = ctx;

        public VTagRecipeCount[] TopTags { get; set; }

        public void OnGet()
        {
            TopTags = _ctx
                .VTagRecipeCounts
                .OrderByDescending(t => t.NumberRecipes)
                .Take(10)
                .ToArray();
        }
    }
}
