using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Text
    {
        public Guid Id { get; set; }
        public string? Page { get; set; }
        public string? Description { get; set; }
    }
}
