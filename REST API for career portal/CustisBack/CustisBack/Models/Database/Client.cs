using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Client
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public string? CompanyName { get; set; }

        public virtual Image? Image { get; set; }
    }
}
