using CookingBlog.Database;
using CookingBlog.Database.Models.Enums;
using CookingBlog.Database.Models.Views;
using CookingBlog.Database.Models;
using CookingBlog.Web.Lib;
using CookingBlog.Web.Models.RecipeData;
using Microsoft.AspNetCore.Mvc;

namespace CookingBlog.Web.ViewComponents
{
    public class RecipeDisplayPrintViewComponent(CookingBlogContext ctx) : ViewComponent
    {
        private readonly CookingBlogContext _ctx = ctx;
        private RecipeDisplay result = new RecipeDisplay();

        public async Task<IViewComponentResult> InvokeAsync(Guid recipeId)
        {
            result = new RecipeDisplay();

            result.RecipeId = recipeId;

            result.Recipe = _ctx
                    .Recipes
                    .Where(r => r.Id == recipeId)
            .First();
            BindSteps();

            BindIngredients();
            result.Tags = _ctx
                .VRecipeTags
            .Where(t => t.RecipeId == recipeId)
                .ToList();

            result.Notes = _ctx
                .RecipeNotes
                .Where(n => n.RecipeId == recipeId)
            .OrderBy(n => n.NoteNumber)
                .ToList();

            result.Story = _ctx
                .RecipeStories
                .Where(s => s.RecipeId == recipeId)
                .OrderBy(s => s.ParagraphNumber)
                .ToList();

            result.Author = _ctx
                .Users
                .Where(u => u.Id == result.Recipe.CreatedById)
            .First();
            var fileName = _ctx
                .RecipeMedia
                .Where(r => r.RecipeId == recipeId && r.MediaCategory == MediaCategory.RecipeHeaderGallery)
                .Select(r => r.FileName)
                .FirstOrDefault();

            try
            {
                result.NutritionInfo = new NutritionInformationCompiler(recipeId, _ctx).CompileNutritionFacts();
            }
            catch
            {
                result.NutritionInfo = null;
            }

            if (fileName != null)
            {
                result.MainImgUrl = $"/api/media/recipe-header-gallery/{fileName}";
            }

            var previewIds = _ctx
                .VRecipeToRecipeTagMatchCounts
                .Where(r => r.RecipeId == recipeId)
                .OrderByDescending(r => r.TagsInCommon)
                .Select(r => r.ComparedRecipeId)
                .Take(3)
                .ToList();

            result.Previews = _ctx
                .VRecipePreviews
                .Where(p => previewIds.Contains(p.RecipeId))
                .ToList();

            return View("~/Pages/Components/RecipeDisplayPrint.cshtml", result);
        }

        private void BindSteps()
        {
            var ungroupedSteps = (
                from recipeGroup in _ctx.RecipeGroups
                join recipeGroupStep in _ctx.RecipeGroupSteps
                on recipeGroup.Id equals recipeGroupStep.RecipeGroupId
                where recipeGroup.RecipeId == result.RecipeId
                orderby recipeGroup.GroupNumber ascending, recipeGroupStep.StepNumber ascending
                select new { recipeGroup, recipeGroupStep }
            )
            .ToList();

            // init dictionaries
            result.StepGroups = ungroupedSteps
                .Select(s => s.recipeGroup)
                .Distinct()
                .ToDictionary(g => g, g => new List<RecipeGroupStep>());

            foreach (var ungroupedStep in ungroupedSteps)
            {
                result.StepGroups[ungroupedStep.recipeGroup].Add(ungroupedStep.recipeGroupStep);
            }
        }

        private void BindIngredients()
        {
            var ungroupedIngredients = (
                from recipeGroup in _ctx.RecipeGroups
                join recipeGroupIngredient in _ctx.VRecipeGroupIngredients
                on recipeGroup.Id equals recipeGroupIngredient.RecipeGroupId
                where recipeGroup.RecipeId == result.RecipeId
                select new { recipeGroup, recipeGroupIngredient }
            )
            .ToList();

            // init dictionaries
            result.IngredientGroups = ungroupedIngredients
                .Select(s => s.recipeGroup)
                .Distinct()
                .ToDictionary(g => g, g => new List<VRecipeGroupIngredient>());

            foreach (var ungroupedIngredient in ungroupedIngredients)
            {
                result.IngredientGroups[ungroupedIngredient.recipeGroup].Add(ungroupedIngredient.recipeGroupIngredient);
            }
        }
    }
}
