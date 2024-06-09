using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CookingBlog.Web.Models.Recipe
{
    public class IngredientFormData
    {
        public string Name { get; set; } = null!;

        public Guid? Id { get; set; }

        public int Amount { get; set; }

        public int? AmountNumerator { get; set; } 

        public int? AmountDenominator { get; set; }

        public Guid? MeasurementId { get; set; }
    }
}
