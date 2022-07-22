using System.Configuration;
using System.Net.Http.Headers;
using CustisBack.APIs.HeadHunter;
using CustisBack.Models.Database;
using CustisBack.Utils;
using CustisCareers.APIs.HeadHunter;
using CustisCareers.APIs.HeadHunter.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustisBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HeadHunterController : ControllerBase
{
    [HttpGet(Name = "Authorize")]
    public IActionResult Authorize()
    {
        return Redirect($"https://{HeadHunterConstants.HeadHunterDomain}/oauth/authorize?" +
                        $"response_type=code" +
                        $"&client_id={HeadHunterOptions.ClientId}" +
                        $"&redirect_uri=" + Uri.EscapeDataString("http://web.std-1924.ist.mospolytech.ru/HeadHunter/Success"));
    }
    [HttpGet(Name = "Success")]
    public IActionResult Success(string code)
    { 
        if (string.IsNullOrEmpty(code)) return BadRequest();
        var token = GetHeadHunterAccessToken(code);
        if (token == null) return StatusCode(500);
        
        var userResumes = GetHeadHunterResumes(token);
        if (userResumes == null) return StatusCode(500);
        
        
        var resumes = userResumes.Value<JArray>("items")?.Select(x=>x.Value<JObject>()).ToArray();
        if (resumes == null || resumes.Length == 0)
            return Redirect("http://web.std-1924.ist.mospolytech.ru/resume/?message=" +
                            Uri.EscapeDataString("Резюме не найдено"));


        var email = new MyDbContext().Teches.First(x => x.Key == "email").Value;
        var builder = new BodyBuilder();
        for (var i = 0; i < resumes.Length; i++)
        {
            var resume = resumes[i];
            var resumeFile = GetHeadHunterResumeFile(token, resume);
            builder.Attachments.Add(
                $"{resume.Value<string>("first_name")}_{resume.Value<string>("last_name")}_{resume.Value<string>("middle_name")}_{i+1}.pdf",
                resumeFile, new ContentType("application", "pdf"));
        }

        MailFunctions.SendEmail(email, "Резюме", builder.ToMessageBody());

        return Redirect("http://web.std-1924.ist.mospolytech.ru/resume/?message=" +
                        Uri.EscapeDataString("Резюме было успешно отправлено"));
        
    }
    
    private string? GetHeadHunterAccessToken(string authorizationCode)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
        var response = httpClient.PostAsync($"https://{HeadHunterConstants.HeadHunterDomain}/oauth/token", new FormUrlEncodedContent(new []
        {
            new KeyValuePair<string, string>("client_id", HeadHunterOptions.ClientId),
            new KeyValuePair<string, string>("client_secret", HeadHunterOptions.Secret),
            new KeyValuePair<string, string>("code", authorizationCode),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("redirect_uri", "http://web.std-1924.ist.mospolytech.ru/HeadHunter/Success"),
        })).Result;
        
        var responseStr = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<JObject>(responseStr);
        return responseObj?.Value<string>("access_token");
    }
    
    private JObject? GetHeadHunterUserInfo(string accessToken)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeadHunterOptions.TokenType, accessToken);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
        var response = httpClient.GetAsync($"https://{HeadHunterConstants.HeadHunterApiDomain}/me").Result;
        
        var responseStr = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<JObject>(responseStr);
        return responseObj;
    }
    private JObject? GetHeadHunterResumes(string accessToken)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeadHunterOptions.TokenType, accessToken);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
        var response = httpClient.GetAsync($"https://{HeadHunterConstants.HeadHunterApiDomain}/resumes/mine").Result;
        
        var responseStr = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<JObject>(responseStr);
        return responseObj;
    }

    private byte[] GetHeadHunterResumeFile(string accessToken, JObject hhResume)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeadHunterOptions.TokenType, accessToken);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
        var response = httpClient.GetAsync(hhResume.Value<JObject>("download").Value<JObject>("pdf").Value<string>("url")).Result;
        
        return response.Content.ReadAsByteArrayAsync().Result;
    }
}