﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBlog.Database.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string UnitName { get; set; } = null!;
    }
}
