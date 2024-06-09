using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class IdentityApplicationUser : IdentityUser<Guid>
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; } = null!;

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "nvarchar(4000)")]
        public string? Bio { get; set; }
    }
}
