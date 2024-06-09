using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class FileExtension
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public Guid MediaTypeId { get; set; }
    }
}
