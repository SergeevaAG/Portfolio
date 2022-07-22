using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            ListStackVacancies = new HashSet<ListStackVacancy>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Experience { get; set; }
        public string? Schedule { get; set; }
        public string? Requirements { get; set; }
        public string? Pluses { get; set; }
        public string? Tasks { get; set; }
        public string? Comments { get; set; }
        public string? WorkingConditions { get; set; }
        public bool? Show { get; set; }
        public Guid? DirectionId { get; set; }
        public int Geo { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Direction? Direction { get; set; }
        public virtual ICollection<ListStackVacancy> ListStackVacancies { get; set; }
    }
}
