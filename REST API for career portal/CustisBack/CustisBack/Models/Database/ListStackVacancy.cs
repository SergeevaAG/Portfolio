using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class ListStackVacancy
    {
        public Guid Id { get; set; }
        public Guid? VacancyId { get; set; }
        public Guid? StackId { get; set; }

        public virtual Stack? Stack { get; set; }
        public virtual Vacancy? Vacancy { get; set; }
    }
}
