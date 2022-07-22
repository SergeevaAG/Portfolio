using CustisBack.Models.Database;
using CustisBack.Utils;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MailController : ControllerBase
{
    [HttpGet(Name = "SendText")]
    public string SendText(string bodyText, string emailSubject)
    {
        try
        {
            using var db = new MyDbContext();
            var email = db.Teches.First(x => x.Key == "email").Value;
            return MailFunctions.SendEmail(email, bodyText, emailSubject);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    [HttpPost(Name = "SendResume")]
    public ActionResult SendResume([FromForm] IFormFile? file)
    {
        try
        {
            if (file != null)
            {
                using var db = new MyDbContext();
                var email = db.Teches.First(x => x.Key == "email").Value;
                var builder = new BodyBuilder();
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    builder.Attachments.Add(file.Name, ms, ContentType.Parse(file.ContentType),
                        CancellationToken.None);
                }

                var subject = "Резюме";
                if (HttpContext.Request.Form.ContainsKey("vacancyName"))
                    subject += " " + HttpContext.Request.Form["vacancyName"];
                
                MailFunctions.SendEmail(email, subject, builder.ToMessageBody());
                return Redirect("http://web.std-1924.ist.mospolytech.ru/resume/?message=" +
                                Uri.EscapeDataString("Резюме успешно отправлено"));
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return Redirect("http://web.std-1924.ist.mospolytech.ru/resume/?message=" +
                        Uri.EscapeDataString("Ошибка отправки резюме"));
    }
}