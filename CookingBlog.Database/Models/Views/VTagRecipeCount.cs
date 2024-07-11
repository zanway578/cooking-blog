using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models.Views
{
    public class VTagRecipeCount
    {
        public Guid TagId { get; set; }

        public string TagName { get; set; } = null!;

        public long NumberRecipes { get; set; }
    }
}
