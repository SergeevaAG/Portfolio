using CustisBack.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]")]
public class FaqController : ControllerBase
{
    [HttpGet(Name = "GetFaq")]
    public List<Faq> Get()
    {
        using var db = new MyDbContext();
        var listFaq = db.Faqs.ToList();
        return listFaq;
    }
}