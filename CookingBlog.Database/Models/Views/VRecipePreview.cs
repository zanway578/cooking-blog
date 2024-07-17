namespace CookingBlog.Database.Views
{
    public class VRecipePreview
    {
        public Guid RecipeId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? FileName { get; set; }

        public string UrlId { get; set; } = null!;

        public bool IsPublished { get; set; }
    }
}
