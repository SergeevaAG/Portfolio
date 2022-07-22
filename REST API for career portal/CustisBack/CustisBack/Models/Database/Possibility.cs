using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Possibility
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }

        public virtual Image? Image { get; set; }
    }
}
