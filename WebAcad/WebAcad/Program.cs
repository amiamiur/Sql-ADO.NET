using AcademyApp.data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

string conn_str = "Data Source=COMP7A2\\SQLEXPRESS;Initial Catalog=Academy;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddSingleton(new Studentrepos(conn_str));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/student/all", (Studentrepos student_repo) =>
{
    var students = student_repo.FindAllStudents();
    return Results.Ok(students);
});
app.Run();
