using AcademyApp.data;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages();
//builder.Services.AddScoped<Studentrepos>();
//builder.Services.AddScoped<GroupRepos>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/start", () =>
{
    List<String> forecast = new List<string>() {"All Students"};
    return forecast;
})
.WithName("GetStart")
.WithOpenApi();

app.Run();


static List<String> StartUp()
{
    List<String> list = new List<string>() {"All Students"};
    return list;
}
