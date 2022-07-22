using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WorkersStoryController : ControllerBase
{
    [HttpGet(Name = "GetWorkersStory")]
    public List<WorkersStory> Get()
    {
        using var db = new MyDbContext();
        var listWorkersStory = db.WorkersStories.ToList();
        return listWorkersStory;
    }
    [HttpPost(Name = "AddWorkersStory")]
    public string Add(WorkersStoryDTO workersstory)
    {
        try
        {
            using var db = new MyDbContext();
            db.WorkersStories.Add(new WorkersStory()
            {
                Id = Guid.NewGuid(),
                Name = workersstory.name,
                Position = workersstory.position,
                Contribution = workersstory.contribution,
                Description = workersstory.description,
            });
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpPost(Name = "UpdateWorkersStory")]
    public string Update(WorkersStoryDTO workersstory)
    {
        try
        {
            using var db = new MyDbContext();
            var workersstoryToUpdate = db.WorkersStories.FirstOrDefault(x => x.Id == workersstory.id);
            if (workersstoryToUpdate != null)
            {
                workersstoryToUpdate.Name = workersstory.name;
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

    [HttpPost(Name = "RemoveWorkerStory")]
    public string Remove(Guid workersstoryId)
    {
        try
        {
            using var db = new MyDbContext();
            var dbWorkersStory = db.WorkersStories.First(x => x.Id == workersstoryId);
            db.Attach(dbWorkersStory);
            db.Remove(dbWorkersStory);
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
