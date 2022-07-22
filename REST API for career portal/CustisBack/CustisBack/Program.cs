using System.Configuration;
using System.Net;
using System.Security.Claims;
using AspNetCoreRateLimit;
using CustisBack.APIs.HeadHunter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args,
});

if (File.Exists("secret.json"))
{
    var config = JObject.Parse(File.ReadAllText("secret.json"));
    var hhOptions = config.Value<JObject>("HeadHunterOptions");
    HeadHunterOptions.ClientId = hhOptions.Value<string>("ClientId");
    HeadHunterOptions.Secret = hhOptions.Value<string>("Secret");
    HeadHunterOptions.Code = hhOptions.Value<string>("Code");
    
    var hhApi = new HeadHunterApi();

    if (string.IsNullOrEmpty(HeadHunterOptions.AccessToken))
    {
        try
        {
            hhApi.GenerateAccessToken();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    builder.Services.AddSingleton(hhApi);
}

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/";
    });

builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("admin", policy => {
        policy.RequireClaim(ClaimTypes.Role, "admin");
    });
});

#region Rate limiting by IP

builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>()
    {
        new()
        {
            Endpoint = "*",
            Period = "2s",
            Limit = 50
        }
    };
});
builder.Services.Configure<IpRateLimitPolicies>(options =>
{
    
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

#endregion


builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//  Rate limiting by IP
app.UseIpRateLimiting();

app.UseStaticFiles();
app.MapFallbackToFile("./index.html");



app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
