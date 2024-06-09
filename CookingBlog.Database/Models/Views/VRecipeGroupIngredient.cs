using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VRecipeGroupIngredient
    {
        public Guid RecipeId { get; set; }

        public Guid RecipeGroupId { get; set; }

        public string IngredientName { get; set; } = null!;

        public int MeasurementAmount { get; set; }

        public int? MeasurementAmountNumerator { get; set; }

        public int? MeasurementAmountDenominator { get; set; }
        
        public string MeasurementName { get; set; } = null!;

        public string MeasurementUnitName { get; set; } = null!;
    }
}
