using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Social
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public string? Link { get; set; }
        public string? ShowSocial { get; set; }

        public virtual Image? Image { get; set; }
    }
}
