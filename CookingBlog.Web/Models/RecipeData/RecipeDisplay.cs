using CookingBlog.Database.Models.Views;
using CookingBlog.Database.Models;
using CookingBlog.Database.Views;

namespace CookingBlog.Web.Models.RecipeData
{
    public class RecipeDisplay
    {
        public Guid RecipeId { get; set; }

        public Recipe Recipe { get; set; } = new();

        public Dictionary<RecipeGroup, List<VRecipeGroupIngredient>> IngredientGroups { get; set; } = new();

        public Dictionary<RecipeGroup, List<RecipeGroupStep>> StepGroups { get; set; } = new();

        public List<VRecipeTag> Tags { get; set; } = new();

        public List<RecipeNote> Notes { get; set; } = new();

        public List<RecipeStory> Story { get; set; } = new();

        public IdentityApplicationUser Author { get; set; } = new();

        public NutritionInfoPerServing NutritionInfo { get; set; } = new();

        public DateTime WrittenDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? MainImgUrl { get; set; }

        public byte[] Image { get; set; } = new byte[0];

        public List<VRecipePreview> Previews { get; set; } = new();
    }
}
