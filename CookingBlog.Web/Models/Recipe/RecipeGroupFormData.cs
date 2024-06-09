namespace CookingBlog.Web.Models.Recipe
{
    public class RecipeGroupFormData
    {
        public string Name { get; set; } = null!;

        public int GroupNumber { get; set; }

        public List<IngredientFormData> Ingredients { get; set; } = [];

        public List<RecipeStepFormData> RecipeSteps { get; set; } = [];
    }
}
