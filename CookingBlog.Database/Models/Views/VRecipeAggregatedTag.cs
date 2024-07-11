using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VRecipeAggregatedTag
    {
        public Guid RecipeId { get; set; }

        public string AggregatedTags { get; set; } = null!;
    }
}
