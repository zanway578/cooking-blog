using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class MediaSizeType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RecipeMediaToSizedMedium> RecipeMediaToSizedMedia { get; set; } = new List<RecipeMediaToSizedMedium>();
}
