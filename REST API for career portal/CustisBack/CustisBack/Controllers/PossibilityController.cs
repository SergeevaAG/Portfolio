using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PossibilityController : ControllerBase
{
    [HttpGet(Name = "ListPossibility")]
    public List<Possibility> List()
    {
        using var db = new MyDbContext();
        var listPossibility = db.Possibilities.ToList();
        return listPossibility;
    }
    [HttpPost(Name = "AddPossibility")]
    public string Add(PossibilityDTO possibility)
    {
        try
        {
            using var db = new MyDbContext();
            db.Possibilities.Add(new Possibility()
            {
                Id = Guid.NewGuid(),
                Headline = possibility.headline,
                Description = possibility.description,
            });
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpPut(Name = "UpdatePossibility")]
    public string Update(PossibilityDTO possibility)
    {
        try
        {
            using var db = new MyDbContext();
            var possibilityToUpdate = db.Possibilities.FirstOrDefault(x => x.Id == possibility.id);
            if (possibilityToUpdate != null)
            {
                possibilityToUpdate.Headline = possibility.headline;
                possibilityToUpdate.Description = possibility.description;
                db.SaveChanges();
                return "Success";
            }
            return "Not found";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpDelete(Name = "RemovePossibility")]
    public string Remove(Guid possibilityId)
    {
        try
        {
            using var db = new MyDbContext();
            var dbPossibility = db.Possibilities.First(x => x.Id == possibilityId);
            db.Remove(dbPossibility);
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
