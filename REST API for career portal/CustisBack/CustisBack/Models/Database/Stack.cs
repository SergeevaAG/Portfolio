using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Stack
    {
        public Stack()
        {
            ListStackVacancies = new HashSet<ListStackVacancy>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Color { get; set; } = null!;

        public virtual ICollection<ListStackVacancy> ListStackVacancies { get; set; }
    }
}
