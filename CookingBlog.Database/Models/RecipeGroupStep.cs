using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class RecipeGroupStep
{
    public Guid Id { get; set; }

    public Guid RecipeGroupId { get; set; }

    public int StepNumber { get; set; }

    public string? StepText { get; set; }
}
