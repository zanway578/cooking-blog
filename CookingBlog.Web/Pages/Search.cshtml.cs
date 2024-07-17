using CookingBlog.Database;
using CookingBlog.Database.Models.Views;
using CookingBlog.Database.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CookingBlog.Web.Pages
{
    public class SearchModel(CookingBlogContext ctx) : PageModel
    {
        private const int NUM_TAGS = 10;

        private CookingBlogContext _ctx = ctx;

        public VTagRecipeCount[] TopTags { get; set; }

        public List<VRecipePreview> Previews { get; set; }

        public void OnGet()
        {
            TopTags = _ctx
                .VTagRecipeCounts
                .OrderByDescending(t => t.NumberRecipes)
                .Take(10)
                .ToArray();

            Previews = new();
        }

        public void OnPost()
        {
            TopTags = _ctx
                .VTagRecipeCounts
                .OrderByDescending(t => t.NumberRecipes)
                .Take(NUM_TAGS)
                .ToArray();

            Previews = GetPreviewsFromForm(Request.Form);
        }

        private List<VRecipePreview> GetPreviewsFromForm(IFormCollection form)
        {
            var tagsToSearch = new List<string>();

            var recipeName = "%" + form["searchText"] + "%";

            var results = _ctx
                .VRecipePreviews
                .Where(r => r.IsPublished && EF.Functions.Like(r.Name, recipeName));

            // add where clauses
            for (var i = 0; i < NUM_TAGS; i++)
            {
                try
                {
                    var tag = form[$"tag-{i}"].First();

                    tagsToSearch.Add(tag);
                }
                catch
                {
                    continue;
                }
            }

            tagsToSearch = tagsToSearch
                .OrderBy(t => t)
                .ToList();

            // can pull recipeIDs
            if (tagsToSearch.Count > 0)
            {
                var searchString = string.Join(" | ", tagsToSearch).ToLower();

                var matchingRecipeIds = _ctx
                    .VRecipeAggregatedTags
                    .Where(t => t.AggregatedTags.Contains(searchString))
                    .Select(t => t.RecipeId)
                    .ToList();

                results = results
                    .Where(r => matchingRecipeIds.Contains(r.RecipeId));
            }

            return results
                .Take(20)
                .ToList();
        }
    }
}
