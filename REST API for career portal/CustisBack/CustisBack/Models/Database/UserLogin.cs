using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? DateLogin { get; set; }
        public string? DateLogout { get; set; }
        public bool IsSuccessfull { get; set; }
        public string? Reason { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
