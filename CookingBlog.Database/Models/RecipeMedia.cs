using CookingBlog.Database.Models.Enums;
using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class RecipeMedia
{
    public Guid Id { get; set; }

    public byte[] Data { get; set; } = null!;

    public Guid RecipeId { get; set; }

    public Guid FileExtensionId { get; set; }

    public int MediaNumber { get; set; }

    public MediaCategory MediaCategory { get; set; }

    public string FileName { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual ICollection<RecipeMediaToSizedMedium> RecipeMediaToSizedMedia { get; set; } = new List<RecipeMediaToSizedMedium>();
}
