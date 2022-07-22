using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class LoginsArchive
    {
        public int Id { get; set; }
        public string? ArchiveDate { get; set; }
        public int? AdminCount { get; set; }
        public int? UserCount { get; set; }
    }
}
