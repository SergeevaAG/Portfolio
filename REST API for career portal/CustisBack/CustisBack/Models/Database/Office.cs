﻿using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Office
    {
        public Office()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Title { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Contact { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
