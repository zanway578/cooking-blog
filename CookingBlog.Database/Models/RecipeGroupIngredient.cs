using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class RecipeGroupIngredient
{
    public Guid Id { get; set; }

    public Guid RecipeGroupId { get; set; }

    public Guid IngredientId { get; set; }

    public Guid MeasurementId { get; set; }

    public int MeasurementAmount { get; set; }

    public int? MeasurementAmountNumerator { get; set; }

    public int? MeasurementAmountDenominator { get; set; }

    public int IngredientNumber { get; set; }
}
