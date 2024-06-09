using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Database.Models.Enums;
using CookingBlog.Database.Models.Views;
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

        public Recipe Recipe { get; set; }

        public Dictionary<RecipeGroup, List<VRecipeGroupIngredient>> IngredientGroups { get; set; }

        public Dictionary<RecipeGroup, List<RecipeGroupStep>> StepGroups { get; set; }

        public List<VRecipeTag> Tags { get; set; }

        public List<RecipeNote> Notes { get; set; }

        public List<RecipeStory> Story { get; set; }

        public IdentityApplicationUser Author { get; set; }

        public DateTime WrittenDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? MainImgUrl { get; set; }

        public byte[] Image { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                RecipeId = new Guid(HttpContext.Request.Query["id"]);

                Recipe = _ctx
                    .Recipes
                    .Where(r => r.Id == RecipeId)
                    .First();

                BindSteps();

                BindIngredients();

                Tags = _ctx
                    .VRecipeTags
                    .Where(t => t.RecipeId == RecipeId)
                    .ToList();

                Notes = _ctx
                    .RecipeNotes
                    .Where(n => n.RecipeId == RecipeId)
                    .OrderBy(n => n.NoteNumber)
                    .ToList();

                Story = _ctx
                    .RecipeStories
                    .Where(s => s.RecipeId == RecipeId)
                    .OrderBy(s => s.ParagraphNumber)
                    .ToList();

                Author = _ctx
                    .Users
                    .Where(u => u.Id == Recipe.CreatedById)
                    .First();

                var fileName = _ctx
                    .RecipeMedia
                    .Where(r => r.RecipeId == RecipeId && r.MediaCategory == MediaCategory.RecipeHeaderGallery)
                    .Select(r => r.FileName)
                    .FirstOrDefault();

                if (fileName != null)
                {
                    MainImgUrl = $"/api/media/recipe-header-gallery/{fileName}";
                }

                return Page();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        private void BindSteps()
        {
            var ungroupedSteps = (
                from recipeGroup in _ctx.RecipeGroups
                join recipeGroupStep in _ctx.RecipeGroupSteps
                on recipeGroup.Id equals recipeGroupStep.RecipeGroupId
                where recipeGroup.RecipeId == RecipeId
                orderby recipeGroup.GroupNumber ascending, recipeGroupStep.StepNumber ascending
                select new { recipeGroup, recipeGroupStep}
            )
            .ToList();

            // init dictionaries
            StepGroups = ungroupedSteps
                .Select(s => s.recipeGroup)
                .Distinct()
                .ToDictionary(g => g, g => new List<RecipeGroupStep>());

            foreach (var ungroupedStep in ungroupedSteps)
            {
                StepGroups[ungroupedStep.recipeGroup].Add(ungroupedStep.recipeGroupStep);
            }
        }

        private void BindIngredients()
        {
            var ungroupedIngredients = (
                from recipeGroup in _ctx.RecipeGroups
                join recipeGroupIngredient in _ctx.VRecipeGroupIngredients
                on recipeGroup.Id equals recipeGroupIngredient.RecipeGroupId
                where recipeGroup.RecipeId == RecipeId
                select new { recipeGroup, recipeGroupIngredient }
            )
            .ToList();

            // init dictionaries
            IngredientGroups = ungroupedIngredients
                .Select(s => s.recipeGroup)
                .Distinct()
                .ToDictionary(g => g, g => new List<VRecipeGroupIngredient>());

            foreach (var ungroupedIngredient in ungroupedIngredients)
            {
                IngredientGroups[ungroupedIngredient.recipeGroup].Add(ungroupedIngredient.recipeGroupIngredient);
            }
        }
    }
}
