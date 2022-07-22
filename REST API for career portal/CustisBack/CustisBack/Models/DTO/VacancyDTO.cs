using CustisBack.Models.Database;

namespace CustisBack.Models.DTO;

public class VacancyDTO
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int experience { get; set; }
    public string schedule { get; set; }
    public string workingConditions { get; set; }
    public bool showVacancy { get; set; }
    public int geo { get; set; }
    public IEnumerable<StackDTO> stackList { get; set; }
    public string direction { get; set; }

}