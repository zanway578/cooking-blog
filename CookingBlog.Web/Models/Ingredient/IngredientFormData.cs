namespace CookingBlog.Web.Models.Ingredient
{
    public class IngredientFormData
    {
        public string Name { get; set; }

        public int CaloriesInOneHundredGrams { get; set; }

        public int? HowManyGramsInACup { get; set; }

        public List<IngredientNutrientFormData> NutrientFormData { get; set; } = null!;
    }
}
