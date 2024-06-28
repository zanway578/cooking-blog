namespace CookingBlog.Web.Models.Ingredient
{
    public class IngredientFormData
    {
        public string Name { get; set; } = "";

        public int CaloriesInOneHundredGrams { get; set; } = 0;

        public int? NumberGramsInACup { get; set; }

        public List<IngredientNutrientFormData> NutrientFormData { get; set; } = new();
    }
}
