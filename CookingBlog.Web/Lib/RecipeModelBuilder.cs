using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Models.Recipe;
using System.Runtime.CompilerServices;

namespace CookingBlog.Web.Lib
{
    public class RecipeModelBuilder
    {
        private Guid? _recipeId { get; set; }

        private CookingBlogContext _ctx { get; set; }

        public RecipeModelBuilder(Guid? recipeId, CookingBlogContext ctx)
        {
            _recipeId = recipeId;
            _ctx = ctx;
        }

        public RecipeFormData BuildRecipe()
        {
            var recipeDataModel = ConstructDefaultRecipeObject();

            if (_recipeId != null)
            {
                var recipe = GetRecipe();

                recipeDataModel.Name = recipe.Name;
                recipeDataModel.Description = recipe.Description;
                recipeDataModel.PrepTime = recipe.PrepTime;
                recipeDataModel.CookTime = recipe.CookTime;
                recipeDataModel.NumberServings = recipe.NumberServings;

                recipeDataModel.RecipeGroups = GetRecipeGroups();
                recipeDataModel.Tags = GetTags();
                recipeDataModel.Notes = GetNotes();
                recipeDataModel.Story = GetStory();
            }

            return recipeDataModel;
        }

        private Recipe GetRecipe()
        {
            var recipe = _ctx
                .Recipes
                .Where(r => r.Id == _recipeId.Value)
                .First();

            return recipe;
        }

        private List<RecipeNoteData> GetNotes()
        {
            return _ctx
                .RecipeNotes
                .Where(n => n.RecipeId == _recipeId.Value)
                .Select(n => new RecipeNoteData { NoteNumber = n.NoteNumber, NoteText = n.NoteText })
                .OrderBy(n => n.NoteNumber)
                .ToList();
        }


        private List<RecipeGroupFormData> GetRecipeGroups()
        {
            var data = new List<RecipeGroupFormData>();

            var groups = _ctx
                .RecipeGroups
                .Where(g => g.RecipeId == _recipeId.Value)
                .OrderBy(g => g.GroupNumber)
                .ToList();

            foreach (var group in groups)
            {
                data.Add(new RecipeGroupFormData
                {
                    Name = group.Name,
                    GroupNumber = group.GroupNumber,
                    Ingredients = GetIngredients(group.Id),
                    RecipeSteps = GetRecipeSteps(group.Id)
                });
            }

            return data;
        }

        private List<RecipeStepFormData> GetRecipeSteps(Guid recipeGroupId)
        {
            return _ctx
                .RecipeGroupSteps
                .Where(r => r.RecipeGroupId == recipeGroupId)
                .OrderBy(r => r.StepNumber)
                .ToList()
                .Select(r => new RecipeStepFormData
                {
                    StepNumber = r.StepNumber,
                    StepText = r.StepText
                })
                .ToList();
        }

        private List<IngredientFormData> GetIngredients(Guid recipeGroupId)
        {
            return (
                from recipeToIngredient in _ctx.RecipeGroupIngredients
                join ingredient in _ctx.Ingredients
                on recipeToIngredient.IngredientId equals ingredient.Id
                where recipeToIngredient.RecipeGroupId == recipeGroupId
                select new IngredientFormData
                {
                    Name = ingredient.Name,
                    Id = ingredient.Id,
                    Amount = recipeToIngredient.MeasurementAmount,
                    AmountNumerator = recipeToIngredient.MeasurementAmountNumerator,
                    AmountDenominator = recipeToIngredient.MeasurementAmountDenominator,
                    MeasurementId = recipeToIngredient.MeasurementId
                }
            )
            .ToList();
        }

        private List<TagFormData> GetTags()
        {
            return (
                from recipeToTag in _ctx.RecipeToTags
                join tag in _ctx.Tags
                on recipeToTag.TagId equals tag.Id
                where recipeToTag.RecipeId == _recipeId.Value
                select new TagFormData
                {
                    Id = tag.Id,
                    Name = tag.Name
                }
            ).ToList();
        }

        private List<RecipeStoryFormData> GetStory()
        {
            return _ctx
                .RecipeStories
                .Where(s => s.RecipeId == _recipeId.Value)
                .OrderBy(s => s.ParagraphNumber)
                .Select(s => new RecipeStoryFormData
                {
                    ParagraphNumber = s.ParagraphNumber,
                    StoryText = s.StoryText
                })
                .ToList();
        }

        private RecipeFormData ConstructDefaultRecipeObject()
        {
            return new RecipeFormData
            {
                Name = "",
                PrepTime = 0,
                CookTime = 0,
                Description = "",
                RecipeGroups = new List<RecipeGroupFormData>(),
                Tags = new List<TagFormData>(),
                Notes = new List<RecipeNoteData>(),
                Story = new List<RecipeStoryFormData>(),
                IsRecipeVisible = true,
            };
        }
    }
}
