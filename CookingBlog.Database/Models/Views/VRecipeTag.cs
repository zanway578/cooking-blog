using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VRecipeTag
    {
        public Guid RecipeId { get; set; }

        public Guid TagId { get; set; }

        public string TagName { get; set; } = null!;
    }
}
