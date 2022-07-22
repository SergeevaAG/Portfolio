using Microsoft.AspNetCore.Mvc;
using CustisBack.Models.Database;
using CustisBack.Models.DTO;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CareerTestController : ControllerBase
{
    [HttpGet(Name = "GetTest")]
    public CareerTestDTO Get()
    {
        using var db = new MyDbContext();
        var result = new CareerTestDTO()
        {
            Directions = db.Directions.ToList(),
            Questions = db.CareerTestQuestions.ToList()
        };

        return result;
    }
    
    [HttpGet(Name = "GetTestQuestions")]
    public IEnumerable<CareerTestQuestion> GetQuestions()
    {
        using var db = new MyDbContext();
        var result =  db.CareerTestQuestions.ToList();
        return result;
    }
    [HttpPost(Name = "AddTestQuestion")]
    public string AddQuestion(CareerTestQuestion testQuestion)
    {
        try
        {
            using var db = new MyDbContext();
            db.CareerTestQuestions.Add(testQuestion);
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpPost(Name = "UpdateTestQuestion")]
    public string UpdateQuestion(CareerTestQuestion question)
    {
        try
        {
            using var db = new MyDbContext();
            var questionToUpdate = db.CareerTestQuestions.FirstOrDefault(x => x.Id == question.Id);
            if (questionToUpdate != null)
            {
                questionToUpdate.Case1 = question.Case1;
                questionToUpdate.Case2 = question.Case2;
                questionToUpdate.Position = question.Position;
                questionToUpdate.WayId1 = question.WayId1;
                questionToUpdate.WayId2 = question.WayId2;
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

    [HttpPost(Name = "RemoveTestQuestion")]
    public string RemoveQuestion(Guid questionId)
    {
        try
        {
            using var db = new MyDbContext();
            var dbDirection = db.CareerTestQuestions.First(x => x.Id == questionId);
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