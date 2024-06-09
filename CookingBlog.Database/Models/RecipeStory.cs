using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class RecipeStory
    {
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }

        public int ParagraphNumber { get; set; }

        public string StoryText { get; set; } = null!;
    }
}
