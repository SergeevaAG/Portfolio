using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class VacancyController : ControllerBase
{
    [HttpPost(Name = "List")]
    public List<VacancyDTO> List(
        [FromForm] string? q = null, 
        [FromForm] int? geo = null,
        [FromForm] int? exp = null,
        [FromForm] int? employment = null,
        [FromForm] int? schedule = null,
        [FromForm] string? direction = null,
        [FromForm] string? stacks = null
        )
    {
        using var db = new MyDbContext();
        IQueryable<Vacancy> dbVacancies = db.Vacancies
            .Include(x=>x.Direction)
            .Include(x=>x.ListStackVacancies)
            .ThenInclude(x=>x.Stack)
            .Where(x=>x.Show == true);
        
        if (!string.IsNullOrEmpty(q)) dbVacancies = dbVacancies
            .Where(x => x.Name.Contains(q));

        if (geo.HasValue)
        {
            dbVacancies = geo switch
            {
                0 => dbVacancies.Where(x => x.Geo == 0), // remote
                1 => dbVacancies.Where(x => x.Geo == 1), // moscow
                _ => dbVacancies
            };
        }
        
        if (exp.HasValue)
        {
            dbVacancies = exp switch
            {
                0 => dbVacancies,
                1 => dbVacancies.Where(x => x.Experience > 1),
                2 => dbVacancies.Where(x => x.Experience > 3),
                _ => dbVacancies
            };
        }

        if (employment.HasValue)
        {
            dbVacancies = employment switch
            {
                0 => dbVacancies.Where(x=>x.WorkingConditions == "полная занятость"),
                1 => dbVacancies.Where(x=>x.WorkingConditions == "частичная занятость"),
                _ => dbVacancies
            };
        }
        
        if (schedule.HasValue)
        {
            dbVacancies = schedule switch
            {
                0 => dbVacancies.Where(x=>x.Schedule == "полный день"),
                1 => dbVacancies.Where(x=>x.Schedule != "полный день"),
                _ => dbVacancies
            };
        }

        if (!string.IsNullOrEmpty(direction))
            dbVacancies = dbVacancies.Where(x => x.Direction.Name == direction);
        
        if(!string.IsNullOrEmpty(stacks))
            foreach (var stack in stacks.Split(','))
            {
                dbVacancies = dbVacancies.Where(x => x.ListStackVacancies.Any(x => x.Stack.Name == stack));
            }
            
        
        var vacancies = dbVacancies
            .Select(x=> new VacancyDTO()
            {
                id = x.Id,
                name = x.Name,
                description = x.Description,
                experience = x.Experience,
                schedule = x.Schedule,
                workingConditions = x.WorkingConditions,
                showVacancy = x.Show.HasValue && x.Show.Value,
                stackList = x.ListStackVacancies.Select(x=> new StackDTO()
                {
                    id = x.StackId,
                    color = x.Stack.Color,
                    name = x.Stack.Name
                }),
                direction = x.Direction.Name,
                geo = x.Geo
            }).ToList();

        return vacancies;
    }
    
    [HttpPost(Name = "SingleFull")]
    public ActionResult SingleFull([FromForm] Guid vacancyId)
    {
        using var db = new MyDbContext();
        var vacancy = db.Vacancies
                .Include(x=>x.Direction)
                .Include(x=>x.ListStackVacancies)
                .ThenInclude(x=>x.Stack)
                .Where(x=>x.Show == true)
                .FirstOrDefault(x => x.Id == vacancyId);

        if (vacancy == null) return NotFound();
        
        return new JsonResult(new VacancyDTO()
        {
            id = vacancy.Id,
            name = vacancy.Name,
            description = vacancy.Description,
            experience = vacancy.Experience,
            schedule = vacancy.Schedule,
            workingConditions = vacancy.WorkingConditions,
            showVacancy = vacancy.Show.HasValue && vacancy.Show.Value,
            stackList = vacancy.ListStackVacancies.Select(x => new StackDTO()
            {
                id = x.Stack.Id,
                name = x.Stack.Name,
                color = x.Stack.Color
            }),
            direction = vacancy.Direction.Name,
            geo = vacancy.Geo
        });
        
    }
    [HttpPost(Name = "StackList")]
    public List<StackVacanciesDTO> StackList()
    {
        using var db = new MyDbContext();
        var listStackVacancies = db.ListStackVacancies
            .Select(x => new StackVacanciesDTO()
            {
                id = x.Id,
                stackId = x.Stack.Id,
                name = x.Stack.Name,
                color = x.Stack.Color,
                vacancyId = x.VacancyId
            }).ToList();

        return listStackVacancies;
    }
}