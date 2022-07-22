using Microsoft.AspNetCore.Mvc;
using CustisBack.Models.Database;
using CustisBack.Models.DTO;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DirectionController : ControllerBase
{
    [HttpGet(Name = "GetDirections")]
    public List<Direction> Get()
    {
        using var db = new MyDbContext();
        var listDirections = db.Directions.ToList();
        return listDirections;
    }

    [HttpPost(Name = "AddDirection")]
    public string Add(DirectionDTO direction)
    {
        try
        {
            using var db = new MyDbContext();
            db.Directions.Add(new Direction()
            {
                Id = Guid.NewGuid(),
                Name = direction.name,
            });
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpPost(Name = "UpdateDirection")]
    public string Update(DirectionDTO direction)
    {
        try
        {
            using var db = new MyDbContext();
            var directionToUpdate = db.Directions.FirstOrDefault(x => x.Id == direction.id);
            if (directionToUpdate != null)
            {
                directionToUpdate.Name = direction.name;
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

    [HttpPost(Name = "RemoveDirection")]
    public string Remove(Guid directionId)
    {
        try
        {
            using var db = new MyDbContext();
            var dbDirection = db.Directions.First(x => x.Id == directionId);
            db.Remove(dbDirection);
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}