using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class Recipe
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int PrepTime { get; set; }

    public int CookTime { get; set; }

    public string? JsonData { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedTime { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public int NumberServings { get; set; }

    public string UrlId { get; set; } = null!;

    public virtual ICollection<RecipeMedia> RecipeToMedia { get; set; } = new List<RecipeMedia>();
}
