using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VRecipeToRecipeTagMatchCount
    {
        public Guid RecipeId { get; set; }

        public Guid ComparedRecipeId { get; set; }

        public int TagsInCommon { get; set; }
    }
}
