using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Employee
    {
        public Guid Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
