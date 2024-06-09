using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class RecipeGroup
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int GroupNumber { get; set; }

        public Guid RecipeId { get; set; }
    }
}
