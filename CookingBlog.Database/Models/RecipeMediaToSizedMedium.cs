using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class RecipeMediaToSizedMedium
{
    public Guid Id { get; set; }

    public Guid RecipeMediaId { get; set; }

    public Guid MediaSizeTypeId { get; set; }

    public byte[] Data { get; set; } = null!;

    public virtual MediaSizeType MediaSizeType { get; set; } = null!;

    public virtual RecipeMedia RecipeMedia { get; set; } = null!;
}
