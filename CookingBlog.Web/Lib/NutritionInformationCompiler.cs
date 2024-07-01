using CookingBlog.Database;
using CookingBlog.Web.Models;

namespace CookingBlog.Web.Lib
{
    public class NutritionInformationCompiler(Guid recipeId, CookingBlogContext ctx)
    {
        private readonly Guid _recipeId = recipeId;
        private readonly CookingBlogContext _ctx = ctx;

        // TODO: return actual data type
        public NutritionInfoPerServing CompileNutritionFacts()
        {
            // TODO: RETURN IF ALL NUTRIENTS ASSIGNED

            var nutrients = _ctx
                .VRecipeNutritionValues
                .Where(v => v.RecipeId == _recipeId && v.NutrientMeasurement != "kJ")
                .ToDictionary(v => v.NutrientName, v => v);

            var failed = nutrients
                .Values
                .Where(n => n.ComputationFailed)
                .Count() > 0;

            if (failed)
            {
                throw new Exception("Attempt to use volumetric conversion failed. Please make sure all ingredients in recipe using voluemetric measurement have a conversion value in the ingredients UI.");
            }

            // TODO do right

            var calories = Convert.ToInt32(nutrients["Energy"].Amount);
            var protein = Convert.ToInt32(nutrients.GetValueOrDefault("Protein")?.Amount ?? 0);
            var totalFat = Convert.ToInt32(nutrients.GetValueOrDefault("Total lipid (fat)")?.Amount ?? 0);
            var totalSugars = Convert.ToInt32(nutrients.GetValueOrDefault("Total Sugars")?.Amount ?? 0);
            var sodium = Convert.ToInt32(nutrients.GetValueOrDefault("Sodium, Na")?.Amount ?? 0);
            var cholesterol = Convert.ToInt32(nutrients.GetValueOrDefault("Cholesterol")?.Amount ?? 0);
            var fiber = Convert.ToInt32(nutrients.GetValueOrDefault("Fiber, total dietary")?.Amount ?? 0);
            var carbs = Convert.ToInt32(nutrients.GetValueOrDefault("Carbohydrate, by difference")?.Amount ?? 0);

            return new NutritionInfoPerServing
            {
                Calories = calories,
                GramsTotalCarb = carbs,
                GramsProtein = protein,
                GramsTotalFat = totalFat,
                GramsTotalSugar = totalSugars,
                MilligramsSodium = sodium,
                MilligramsCholesterol = cholesterol,
                GramsFiber = fiber,
            };
        }

        private string BuildFailureString()
        {
            return "Not all ingredients for this recipe have been assigned nutrients.";
        }

        private double ConvertMeasurement(double amount, Measurement from, Measurement to)
        {
            switch (from)
            {
                case Measurement.Grams:
                    switch (to)
                    {
                        case Measurement.Grams:
                            return amount;
                        case Measurement.Milligrams:
                            return amount * 1000;
                        case Measurement.Micrograms:
                            return amount * 1000 * 1000;
                        default:
                            throw new NotSupportedException();
                    }
                case Measurement.Milligrams:
                    switch (to)
                    {
                        case Measurement.Grams:
                            return amount / 1000;
                        case Measurement.Milligrams:
                            return amount;
                        case Measurement.Micrograms:
                            return amount * 1000;
                        default:
                            throw new NotSupportedException();
                    }
                case Measurement.Micrograms:
                    switch (to)
                    {
                        case Measurement.Grams:
                            return amount / 1000 / 1000;
                        case Measurement.Milligrams:
                            return amount / 1000;
                        case Measurement.Micrograms:
                            return amount;
                        default:
                            throw new NotSupportedException();
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        private Measurement ToEnum(string value)
        {
            switch (value)
            {
                case "G":
                    return Measurement.Grams;
                case "MG":
                    return Measurement.Milligrams;
                case "UG":
                    return Measurement.Micrograms;
                default:
                    throw new NotSupportedException();
            }
        }

        protected enum Measurement
        {
            Grams,
            Milligrams,
            Micrograms
        }
    }
}
