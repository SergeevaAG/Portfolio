using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class User
    {
        public User()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public int? OfficeId { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Active { get; set; }

        public virtual Office? Office { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
