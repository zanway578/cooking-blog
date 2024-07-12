using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Database.Models.Enums;
using CookingBlog.Web.Models.RecipeData;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Security.Claims;

namespace CookingBlog.Web.Lib
{
    public class RecipeSaver(Guid? recipeId, RecipeFormData recipeFormData, CookingBlogContext ctx, UserManager<IdentityApplicationUser> UserManager, ClaimsPrincipal User)
    {
        private Guid? _recipeId = recipeId;
        private readonly RecipeFormData _recipeDataModel = recipeFormData;
        private readonly CookingBlogContext _ctx = ctx;
        private readonly UserManager<IdentityApplicationUser> _userManager = UserManager;
        private readonly ClaimsPrincipal _user = User;


        public void SaveRecipe()
        {
            _ctx.Database.BeginTransaction();

            try
            {
                Recipe oldRecipe = null;

                if (_recipeId != null)
                {
                    DeleteExistingData();

                    oldRecipe = _ctx
                        .Recipes
                        .Where(r => r.Id == _recipeId.Value)
                        .First();

                    oldRecipe.Name = _recipeDataModel.Name;
                    oldRecipe.Description = _recipeDataModel.Description;
                    oldRecipe.PrepTime = _recipeDataModel.PrepTime;
                    oldRecipe.CookTime = _recipeDataModel.CookTime;
                    oldRecipe.IsPublished = _recipeDataModel.IsRecipeVisible;
                    oldRecipe.NumberServings = _recipeDataModel.NumberServings;
                    oldRecipe.LastModifiedTime = DateTime.UtcNow;

                    _ctx.Recipes.Update(oldRecipe);
                }
                else
                {
                    var newRecipe = new Recipe()
                    {
                        Id = _recipeId ?? Guid.NewGuid(),
                        Name = _recipeDataModel.Name,
                        Description = _recipeDataModel.Description,
                        PrepTime = _recipeDataModel.PrepTime,
                        CookTime = _recipeDataModel.CookTime,
                        IsPublished = _recipeDataModel.IsRecipeVisible,
                        CreatedById = new Guid(_userManager.GetUserId(_user)),
                        CreatedTime = DateTime.UtcNow,
                        NumberServings = _recipeDataModel.NumberServings
                    };

                    _recipeId = newRecipe.Id;

                    _ctx.Recipes.Add(newRecipe);
                }

                _ctx.SaveChanges();

                _ctx.RecipeToTags
                    .AddRange(_recipeDataModel.Tags.Select(t => new RecipeToTag
                    {
                        Id = Guid.NewGuid(),
                        RecipeId = _recipeId.Value,
                        TagId = t.Id
                    })
                    );

                var recipeSteps = new List<RecipeGroupStep>();

                // TODO: fix

                foreach (var group in _recipeDataModel.RecipeGroups)
                {
                    var groupId = Guid.NewGuid();

                    _ctx.RecipeGroups.Add(new RecipeGroup
                    {
                        Id = groupId,
                        RecipeId = _recipeId.Value,
                        GroupNumber = group.GroupNumber,
                        Name = group.Name
                    });

                    _ctx.SaveChanges();

                    var ingredientNumber = 0;

                    _ctx.RecipeGroupIngredients.AddRange(group.Ingredients.Select(i => new RecipeGroupIngredient
                    {
                        Id = Guid.NewGuid(),
                        RecipeGroupId = groupId,
                        IngredientId = i.Id.Value,
                        MeasurementId = i.MeasurementId.Value,
                        MeasurementAmount = i.Amount,
                        MeasurementAmountDenominator = i.AmountDenominator,
                        MeasurementAmountNumerator = i.AmountNumerator,
                        IngredientNumber = ingredientNumber++
                    }));

                    var stepNumber = 0;

                    _ctx.RecipeGroupSteps.AddRange(group.RecipeSteps.Select(s => new RecipeGroupStep
                    {
                        Id = Guid.NewGuid(),
                        StepNumber = stepNumber++,
                        RecipeGroupId = groupId,
                        StepText = s.StepText
                    }));

                    _ctx.SaveChanges();
                }

                _ctx.RecipeNotes
                    .AddRange(_recipeDataModel.Notes.Select(n => new RecipeNote
                    {
                        Id = Guid.NewGuid(),
                        RecipeId = _recipeId.Value,
                        NoteNumber = n.NoteNumber,
                        NoteText = n.NoteText
                    })
                );

                _ctx.RecipeStories
                    .AddRange(_recipeDataModel.Story
                    .Select(s => new RecipeStory
                    {
                        Id = Guid.NewGuid(),
                        RecipeId = _recipeId.Value,
                        ParagraphNumber = s.ParagraphNumber,
                        StoryText = s.StoryText
                    })
                );

                AddMedia();

                _ctx.SaveChanges();

                _ctx.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _ctx.Database.RollbackTransaction();
                throw new Exception($"Save failed: {ex.Message}: {ex.InnerException?.Message ?? "No inner ex"}");
            }
        }

        private void AddMedia()
        {
            if (_recipeDataModel.MainImageData != null)
            {
                var extensionId = _ctx
                    .FileExtensions
                    .Where(e => e.Extension == _recipeDataModel.MainImageData.Extension)
                    .Select(e => e.Id)
                    .First();

                _ctx.RecipeMedia.Add(
                    new RecipeMedia
                    {
                        Id = _recipeDataModel.MainImageData.Id,
                        Data = _recipeDataModel.MainImageData.Data,
                        RecipeId = _recipeId.Value,
                        FileExtensionId = extensionId,
                        MediaNumber = 1,
                        FileName = _recipeDataModel.MainImageData.Id + _recipeDataModel.MainImageData.Extension,
                        MediaCategory = MediaCategory.RecipeHeaderGallery
                    }
                );
            }
        }

        // NOTE: highly suggest starting a transaction before calling this
        private void DeleteExistingData()
        {

            var recipeGroupIds = _ctx
                .RecipeGroups
                .Where(g => g.RecipeId == recipeId)
                .Select(g => g.Id)
                .ToList();

            _ctx.RecipeGroupSteps
                        .RemoveRange(
                            _ctx.RecipeGroupSteps
                                .Where(r => recipeGroupIds.Contains(r.RecipeGroupId))
                                .ToList()
                        );

            _ctx.RecipeGroupIngredients
                .RemoveRange(
                    _ctx.RecipeGroupIngredients
                        .Where(i => recipeGroupIds.Contains(i.RecipeGroupId))
                );

            _ctx.SaveChanges();

            _ctx.RecipeGroups
                .RemoveRange(
                    _ctx.RecipeGroups
                        .Where(g => recipeGroupIds.Contains(g.Id))
                );

            _ctx.RecipeToTags
                .RemoveRange(
                    _ctx.RecipeToTags
                        .Where(t => t.RecipeId == _recipeId.Value)
                );

            _ctx.RecipeNotes
                .RemoveRange(
                    _ctx.RecipeNotes
                    .Where(n => n.RecipeId == _recipeId.Value)
                );

            _ctx.RecipeStories
                .RemoveRange(
                    _ctx.RecipeStories
                        .Where(s => s.RecipeId == _recipeId.Value)
                );


            // TODO: NOTE: REMOVES ALL IMAGES. REFINE AFTER ADDING MORE MEDIA
            if (_recipeDataModel.MainImageData != null)
            {
                _ctx.RecipeMedia
                    .RemoveRange(
                        _ctx.RecipeMedia
                            .Where(m => m.RecipeId == _recipeId.Value)
                    );
            }

            _ctx.SaveChanges();
        }
    }
}
