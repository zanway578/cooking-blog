using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VRecipeNutritionValue
    {
        public Guid RecipeId { get; set; }

        public Guid NutrientId { get; set; }

        public string NutrientName { get; set; } = null!;

        public string NutrientMeasurement { get; set; } = null!;

        public double Amount { get; set; }

        public bool ComputationFailed { get; set; }
    }
}
