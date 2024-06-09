using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class IngredientToNutrient
{
    public Guid Id { get; set; }

    public Guid IngredientId { get; set; }

    public Guid NutrientId { get; set; }

    public bool IsEssential { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Nutrient Nutrient { get; set; } = null!;
}
