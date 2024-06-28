using CookingBlog.Database;

namespace CookingBlog.Web.Lib
{
    public class NutritionInformationCompiler(Guid recipeId, CookingBlogContext ctx)
    {
        private readonly Guid _recipeId = recipeId;
        private readonly CookingBlogContext _ctx = ctx;

        // TODO: return actual data type
        public object? CompileNutritionFacts()
        {
            /* Algorithm:
             * 1. Verify all ingredients have a nutrient list - if not, we can't compute.
             * 2. Produce condensed nutrient list using SQL view.
             * 3. Pull out important macro nutrient and return as model.
             */

            return null;
        }
    }
}
