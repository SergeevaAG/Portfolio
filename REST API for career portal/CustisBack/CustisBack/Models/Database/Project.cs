using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Project
    {
        public Guid Id { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
    }
}
