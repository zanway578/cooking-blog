using CookingBlog.Database.Models;

namespace CookingBlog.Web.Models.RecipeData
{
    public class RecipeForm
    {
        public Guid? RecipeId { get; set; }

        public List<string> AvailableFormats { get; set; }

        public List<Measurement> AvailableMeasurements { get; set; }

        public RecipeFormData RecipeDataModel;
    }
}
