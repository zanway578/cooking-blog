using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class Ingredient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int? NumberGramsInACup { get; set; }

    public int CaloriesInOneHundredGrams { get; set; }

    public virtual ICollection<IngredientToNutrient> IngredientToNutrients { get; set; } = new List<IngredientToNutrient>();
}
