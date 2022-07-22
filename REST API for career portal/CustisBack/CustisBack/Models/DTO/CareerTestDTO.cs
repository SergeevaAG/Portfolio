using CustisBack.Models.Database;

namespace CustisBack.Models.DTO;

    public partial class CareerTestDTO
    {
        public IEnumerable<Direction> Directions { get; set; }
        public IEnumerable<CareerTestQuestion> Questions { get; set; }
        
    }
