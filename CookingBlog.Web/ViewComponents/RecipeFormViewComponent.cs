using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Lib;
using CookingBlog.Web.Models.Recipe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace CookingBlog.Web.ViewComponents
{
    public class RecipeFormViewComponent(CookingBlogContext context, UserManager<IdentityApplicationUser> userManager) : ViewComponent
    {
        private readonly CookingBlogContext Context = context;
        public readonly UserManager<IdentityApplicationUser> UserManager = userManager;

        public async Task<IViewComponentResult> InvokeAsync(Guid? recipeId)
        {
            var model = new RecipeForm();

            model.AvailableFormats = (
                from mediaType in Context.MediaTypes
                join fileExtension in Context.FileExtensions on mediaType.Id equals fileExtension.MediaTypeId
                select fileExtension.Extension)
            .ToList();

            model.AvailableMeasurements = Context
                .Measurements
                .ToList();

            model.RecipeDataModel = new RecipeModelBuilder(recipeId, Context).BuildRecipe();
            model.RecipeId = recipeId;

            return View(model);
        }

        public static RecipeFormData BuildFormModel(IFormCollection form, CookingBlogContext ctx)
        {
            try
            {
                var recipeDataModel = new RecipeFormData();

                var name = form["recipe-name"].First();
                var description = form["recipe-description"].First();
                var prepTime = int.Parse(form["prep-time"].First() ?? "0");
                var cookTime = int.Parse(form["cook-time"].First() ?? "0");
                var numberServings = int.Parse(form["number-servings"].First() ?? "1");
                // TODO: reinsert form flag
                var recipeAvailable = false;//form["recipe-available"].First();

                recipeDataModel.Name = name;
                recipeDataModel.Description = description;
                recipeDataModel.PrepTime = prepTime;
                recipeDataModel.CookTime = cookTime;
                recipeDataModel.NumberServings = numberServings;
                recipeDataModel.IsRecipeVisible = recipeAvailable; 

                var tags = new List<TagFormData>();
                var story = new List<RecipeStoryFormData>();

                try
                {
                    var img = form.Files
                        .Where(f => f.Name == "main-img")
                        .First();

                    var data = new byte[img.Length];

                    using var stream = img.OpenReadStream();

                    stream.Read(data);

                    recipeDataModel.MainImageData = new Models.ImageData
                    {
                        Id = Guid.NewGuid(),
                        Extension = "." + img.FileName.Split(".").Last().ToLower(),
                        Data = data
                    };
                }

                catch {
                    recipeDataModel.MainImageData = null;
                }

                recipeDataModel.RecipeGroups = GetRecipeGroupData(form, ctx);

                try
                {
                    var index = 1;

                    for (var curStory = 1; true; curStory++)
                    {
                        var storyText = form[$"recipe-story-{curStory}"].First();

                        if (storyText.IsNullOrEmpty() )
                        {
                            continue;
                        }

                        story.Add(new RecipeStoryFormData
                        {
                            ParagraphNumber = index++,
                            StoryText = storyText
                        });
                    }
                }
                catch {}

                recipeDataModel.Story = story;

                var tagNames = new List<string>();
                try
                {
                    for (var curTag = 1; true; curTag++)
                    {
                        var tagName = form[$"tag-{curTag}"].First();

                        if (tagName == "")
                        {
                            continue;
                        }

                        tagNames.Add(tagName);
                    }
                }
                catch { }

                tags = ctx
                    .Tags
                    .Where(t => tagNames.Contains(t.Name))
                    .Select(t => new TagFormData
                    {
                        Name = t.Name,
                        Id = t.Id
                    })
                    .ToList();

                recipeDataModel.Tags = tags;

                var notes = new List<RecipeNoteData>();

                try
                {
                    for (var curNote = 1; true; curNote++)
                    {
                        var noteText = form[$"recipe-note-{curNote}"].First();

                        if (noteText == "")
                        {
                            continue;
                        }

                        notes.Add(new RecipeNoteData
                        {
                            NoteNumber = curNote,
                            NoteText = noteText
                        });
                    }
                }
                catch { }

                recipeDataModel.Notes = notes;

                return recipeDataModel;

            }
            catch (Exception ex)
            {
                throw new Exception($"Building recipe form model failed: {ex.Message}: {ex.InnerException?.Message ?? "No inner ex"}");
            }
        }

        private static List<RecipeGroupFormData> GetRecipeGroupData(IFormCollection form, CookingBlogContext ctx)
        {
            var data = new List<RecipeGroupFormData>();

            try
            {
                var assignGroupId = 1;

                for (var curGroup = 1; true; curGroup++)
                {
                    var name = form[$"group-name-{curGroup}"].First();

                    if (name != "")
                    {
                        data.Add(new RecipeGroupFormData
                        {
                            Name = name,
                            GroupNumber = assignGroupId++,
                            Ingredients = GetIngredientFormData(form, ctx, curGroup),
                            RecipeSteps = GetRecipeStepFormData(form, ctx, curGroup)
                        });
                    }
                }
            }
            catch
            {
                return data;
            }
                    
        }

        private static List<IngredientFormData> GetIngredientFormData(IFormCollection form, CookingBlogContext ctx, int groupNumber)
        {
            var ingredients = new List<IngredientFormData>();

            try
            {
                for (var curIngredient = 1; true; curIngredient++)
                {
                    var ingName = form[$"ingredient-{groupNumber}-{curIngredient}"].First();
                    var ingAmount = form[$"ingredient-amount-{groupNumber}-{curIngredient}"].First();
                    var ingNum = form[$"ingredient-amount-numerator-{groupNumber}-{curIngredient}"].First();
                    var ingDenom = form[$"ingredient-amount-denominator-{groupNumber}-{curIngredient}"].First();
                    var ingMeasurementId = form[$"ingredient-measurement-{groupNumber}-{curIngredient}"].First();

                    ingredients.Add(new IngredientFormData
                    {
                        Name = ingName,
                        Amount = int.Parse(ingAmount),
                        AmountNumerator = ingNum.IsNullOrEmpty() ? null : int.Parse(ingNum),
                        AmountDenominator = ingDenom.IsNullOrEmpty() ? null : int.Parse(ingDenom),
                        MeasurementId = Guid.Parse(ingMeasurementId),
                    });
                }
            }
            catch { }

            ingredients = (from ingredient in ingredients
                           join db_ingredient in ctx.Ingredients
                           on ingredient.Name equals db_ingredient.Name
                           select new IngredientFormData
                           {
                               Name = db_ingredient.Name,
                               Id = db_ingredient.Id,
                               Amount = ingredient.Amount,
                               AmountNumerator = ingredient.AmountNumerator,
                               AmountDenominator = ingredient.AmountDenominator,
                               MeasurementId = ingredient.MeasurementId,
                           })
                          .ToList();

            return ingredients;
        }

        private static List<RecipeStepFormData> GetRecipeStepFormData(IFormCollection form, CookingBlogContext ctx, int groupNumber)
        {
            var recipeSteps = new List<RecipeStepFormData>();

            try
            {
                for (var curStep = 1; true; curStep++)
                {
                    var stepText = form[$"recipe-step-{groupNumber}-{curStep}"].First();

                    if (stepText == null)
                    {
                        break;
                    }
                    else if (stepText == "")
                    {
                        continue;
                    }

                    recipeSteps.Add(new RecipeStepFormData
                    {
                        StepNumber = curStep,
                        StepText = stepText,

                    });
                }

            }
            catch { }

            return recipeSteps;
        }
    }
}
