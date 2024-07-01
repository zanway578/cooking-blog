using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VIngredientNutrient
    {
        public Guid Id { get; set; }

        public Guid IngredientId { get; set; }

        public Guid NutrientId { get; set; }

        public string NutrientName { get; set; } = null!;

        public float NutrientAmount {  get; set; }

        public string NutrientMeasurement { get; set; } = null!;
        
    }
}
