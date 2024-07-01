namespace CookingBlog.Web.Models.Ingredient
{
    public class IngredientNutrientFormData
    {
        public string NutrientName { get; set; } = null!;

        public float Amount { get; set; }

        public string UnitName { get; set; } = null!;
    }
}
