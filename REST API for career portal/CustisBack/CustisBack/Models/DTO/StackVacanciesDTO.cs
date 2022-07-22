namespace CustisBack.Models.DTO;

public class StackVacanciesDTO
{
    public Guid id { get; set; }
    public Guid? vacancyId { get; set; }
    public Guid? stackId { get; set; }
    public string name { get; set; }
    public string color { get; set; }
}