using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustisBack.Models.Database
{
    public partial class Direction
    {
        public Direction()
        {
            CareerTestQuestionWayId1Navigations = new HashSet<CareerTestQuestion>();
            CareerTestQuestionWayId2Navigations = new HashSet<CareerTestQuestion>();
            Vacancies = new HashSet<Vacancy>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<CareerTestQuestion> CareerTestQuestionWayId1Navigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<CareerTestQuestion> CareerTestQuestionWayId2Navigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
