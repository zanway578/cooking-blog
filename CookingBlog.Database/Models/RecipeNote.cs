using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class RecipeNote
    {
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }

        public int NoteNumber { get; set; }

        public string NoteText { get; set; } = null!;
    }
}
