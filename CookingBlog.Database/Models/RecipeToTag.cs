using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class RecipeToTag
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public Guid TagId { get; set; }
}
