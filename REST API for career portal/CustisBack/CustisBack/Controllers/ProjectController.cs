using CustisBack.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    [HttpGet(Name = "GetProjects")]
    public List<Project> Get()
    {
        using var db = new MyDbContext();
        var listProjects = db.Projects.ToList();
        return listProjects;
    }
}

