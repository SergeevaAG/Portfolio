using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustisBack.Models.Database
{
    public partial class CareerTestQuestion
    {
        public Guid Id { get; set; }
        public Guid WayId1 { get; set; }
        public Guid WayId2 { get; set; }
        public string Case1 { get; set; } = null!;
        public string Case2 { get; set; } = null!;
        public int Position { get; set; }
        [JsonIgnore]
        public virtual Direction WayId1Navigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Direction WayId2Navigation { get; set; } = null!;
    }
}
