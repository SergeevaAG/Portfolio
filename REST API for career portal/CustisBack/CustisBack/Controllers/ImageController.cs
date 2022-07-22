using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ImageController : ControllerBase
{
    [HttpGet(Name = "GetImage")]
    public ActionResult Get(Guid id)
    {
        using var db = new MyDbContext();
        var image = db.Images.FirstOrDefault(x => x.Id == id);
        if (image == null || image.Image1 == null) return NotFound();

        return File(image.Image1, image.ContentType ?? "image/jpeg");
    }
    [HttpGet(Name = "GetImages")]
    public IEnumerable<ImageDTO> List()
    {
        using var db = new MyDbContext();
        var images = db.Images.ToList();
        return images.Select(x => new ImageDTO()
        {
            id = x.Id,
            desc = x.Desc
        });
    }
}

