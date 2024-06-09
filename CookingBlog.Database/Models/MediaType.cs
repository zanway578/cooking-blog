using System;
using System.Collections.Generic;

namespace CookingBlog.Database.Models;

public partial class MediaType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}
