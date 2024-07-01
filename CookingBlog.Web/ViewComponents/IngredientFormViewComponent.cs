using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Models.Ingredient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CookingBlog.Web.ViewComponents
{
    public class IngredientFormViewComponent(CookingBlogContext context, UserManager<IdentityApplicationUser> userManager) : ViewComponent
    {
        private readonly CookingBlogContext _ctx = context;
        public readonly UserManager<IdentityApplicationUser> UserManager = userManager;

        public async Task<IViewComponentResult> InvokeAsync(Guid? ingredientId)
        {
            var model = new IngredientFormData();

            if (ingredientId != null)
            {
                var ingredient = _ctx
                    .Ingredients
                    .Where(i => i.Id == ingredientId.Value)
                    .First();


                var nutrients = _ctx
                    .VIngredientNutrients
                    .Where(n => n.IngredientId == ingredientId.Value)
                    .Select(n => new IngredientNutrientFormData
                    {
                        NutrientName = n.NutrientName,
                        Amount = n.NutrientAmount,
                        UnitName = n.NutrientMeasurement
                    })
                    .ToList();

                model = new IngredientFormData
                {
                    Name = ingredient.Name,
                    CaloriesInOneHundredGrams = ingredient.CaloriesInOneHundredGrams,
                    NumberGramsInACup = ingredient.NumberGramsInACup,
                    NutrientFormData = nutrients
                };
            }

            return View(model);
        }

        public static IngredientFormData BuildFormModel(IFormCollection form)
        {
            try
            {
                var name = form["ingredient-name"].First();
                int? gramsInCup = null; 
                var nutrients = new List<IngredientNutrientFormData>();
                int calInHundredGrams = int.Parse(form["calories-in-hundred-grams"].First());

                try
                {
                    gramsInCup = int.Parse(form["grams-in-cup"].First());
                    
                }
                catch { }
                

                try
                {
                    for (var i = 1; true; i++)
                    {
                        nutrients.Add(new IngredientNutrientFormData
                        {
                            NutrientName = form[$"nutrient-name-{i}"].First(),
                            UnitName = form[$"nutrient-unit-{i}"].First(),
                            Amount = float.Parse(form[$"nutrient-amount-{i}"].First())
                        });
                    }
                }
                catch { }

                return new IngredientFormData
                {
                    Name = name,
                    CaloriesInOneHundredGrams = calInHundredGrams,
                    NumberGramsInACup = gramsInCup,
                    NutrientFormData = nutrients
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Building ingredient form model failed: {ex.Message}: {ex.InnerException?.Message ?? "No inner ex"}");
            }
        }
    }
}
