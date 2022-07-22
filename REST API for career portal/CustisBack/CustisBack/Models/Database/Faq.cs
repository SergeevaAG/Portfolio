using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Faq
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }
}
