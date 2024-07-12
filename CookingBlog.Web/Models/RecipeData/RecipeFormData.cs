namespace CookingBlog.Web.Models.RecipeData
{
    public class RecipeFormData
    {
        public string Name { get; set; } = null!;

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public int NumberServings { get; set; }

        public string Description { get; set; } = null!;

        public List<RecipeGroupFormData> RecipeGroups { get; set; } = null!;

        public List<RecipeNoteData> Notes { get; set; } = null!;

        public List<TagFormData> Tags { get; set; } = null!;

        public List<RecipeStoryFormData> Story { get; set; } = null!;

        public ImageData? MainImageData { get; set; }

        public bool IsRecipeVisible { get; set; }
    }
}
