using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ClientController : ControllerBase
{
    [HttpGet(Name = "GetClient")]
    public List<Client> Get()
    {
        using var db = new MyDbContext();
        var listClient = db.Clients.ToList();
        return listClient;
    }
    [HttpPost(Name = "AddClient")]
    public string Add(ClientDTO client)
    {
        try
        {
            using var db = new MyDbContext();
            db.Clients.Add(new Client()
            {
                Id = Guid.NewGuid(),
                CompanyName = client.companyname,
                
            });
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [HttpPost(Name = "UpdateClient")]
    public string Update(ClientDTO client)
    {
        try
        {
            using var db = new MyDbContext();
            var clientToUpdate = db.Clients.FirstOrDefault(x => x.Id == client.id);
            if (clientToUpdate != null)
            {
                clientToUpdate.CompanyName = client.companyname;
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

    [HttpPost(Name = "Client")]
    public string Remove(Guid clientId)
    {
        try
        {
            using var db = new MyDbContext();
            var dbClient = db.Clients.First(x => x.Id == clientId);
            db.Remove(dbClient);
            db.SaveChanges();
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
