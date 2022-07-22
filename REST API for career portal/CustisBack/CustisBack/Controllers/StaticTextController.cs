using Microsoft.AspNetCore.Mvc;
using CustisBack.Models.Database;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class StaticTextController : ControllerBase
{
    [HttpPost(Name = "Selected")]
    public List<Text> ListSelected([FromForm] string? position = null)
    {
        using var db = new MyDbContext();
        IQueryable<Text> dbTexts = db.Texts;
        if (position != null)
        {
            dbTexts = dbTexts.Where(x => x.Page.Equals(position));
            var listTexts = dbTexts.ToList();
            return listTexts;
        }
        else
        {
            return dbTexts.ToList();
        }
    }
}
