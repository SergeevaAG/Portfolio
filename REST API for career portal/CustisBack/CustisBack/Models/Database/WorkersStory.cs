using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class WorkersStory
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Contribution { get; set; }
        public string? Description { get; set; }

        public virtual Image? Image { get; set; }
    }
}
