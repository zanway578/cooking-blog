using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Models.Ingredient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CookingBlog.Web.Lib
{
    public class IngredientSaver(Guid? IngredientId, IngredientFormData data, CookingBlogContext ctx)
    {
        private Guid? _ingredientId = IngredientId;
        private readonly IngredientFormData _ingredientDataModel = data;
        private readonly CookingBlogContext _ctx = ctx;

        public void SaveIngredient()
        {
            _ctx.Database.BeginTransaction();

            try
            {
                SaveUnknownNutrients();

                if (_ingredientId != null)
                {
                    DeleteExistingData();

                    var oldIngredient = _ctx
                        .Ingredients
                        .Where(i => i.Id == _ingredientId.Value)
                        .First();

                    oldIngredient.Name = _ingredientDataModel.Name;
                    oldIngredient.NumberGramsInACup = _ingredientDataModel.NumberGramsInACup;
                    oldIngredient.CaloriesInOneHundredGrams = _ingredientDataModel.CaloriesInOneHundredGrams;

                    _ctx.Ingredients.Update(oldIngredient);
                }
                else
                {
                    var newIngredient = new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Name = _ingredientDataModel.Name,
                        CaloriesInOneHundredGrams = _ingredientDataModel.CaloriesInOneHundredGrams,
                        NumberGramsInACup = _ingredientDataModel.NumberGramsInACup
                    };

                    _ingredientId = newIngredient.Id;

                    _ctx.Ingredients.Add(newIngredient);
                }

                _ctx.SaveChanges();

                var ingredientNutrients = (
                    from nutrientInfo in _ingredientDataModel.NutrientFormData
                    join dbNutrient in _ctx.Nutrients
                    on nutrientInfo.NutrientName equals dbNutrient.Name
                    select new IngredientToNutrient
                    {
                        Id = Guid.NewGuid(),
                        IngredientId = _ingredientId.Value,
                        NutrientId = dbNutrient.Id,
                        NutrientAmount = nutrientInfo.Amount,
                        NutrientMeasurement = nutrientInfo.UnitName
                    }
                )
                .ToList();

                _ctx.IngredientToNutrients
                    .AddRange(ingredientNutrients);

                _ctx.SaveChanges();

                _ctx.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _ctx.Database.RollbackTransaction();
                throw new Exception($"Save failed: {ex.Message}: {ex.InnerException?.Message ?? "No inner ex"}");
            }
        }

        private void DeleteExistingData()
        {
            _ctx.IngredientToNutrients
                .Where(i => i.IngredientId == _ingredientId.Value)
                .ExecuteDelete();

            _ctx.SaveChanges();
        }

        private void SaveUnknownNutrients()
        {
            var foundNutrientNames = _ingredientDataModel
                .NutrientFormData
                .Select(n => n.NutrientName);

            var existingNutrientNames = _ctx
                .Nutrients
                .Where(n => foundNutrientNames.Contains(n.Name))
                .Select(n => n.Name)
                .ToList();

            var newNutrients = foundNutrientNames
                .Where(fn => !existingNutrientNames.Contains(fn))
                .Distinct()
                .Select(n =>
                    new Nutrient
                    {
                        Id = Guid.NewGuid(),
                        Name = n
                    }
                );

            _ctx.Nutrients
                .AddRange(newNutrients);

            _ctx.SaveChanges();
                
        }
    }
}
