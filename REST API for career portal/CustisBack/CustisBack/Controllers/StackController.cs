using Microsoft.AspNetCore.Mvc;
using CustisBack.Models.Database;
using CustisBack.Models.DTO;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]")]
public class StackController : ControllerBase
{
    //Выводит стэк без привязки к вакансии
    [HttpGet(Name = "GetStacks")]
    public List<Stack> Get()
    {
        using var db = new MyDbContext();
        var listStacks = db.Stacks.ToList();
        return listStacks;
    }
}
