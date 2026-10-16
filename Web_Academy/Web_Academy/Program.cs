using AcademyApp.data;
using AcademyApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var conn_str = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new Studentrepos(conn_str));
builder.Services.AddSingleton(new GroupRepos(conn_str));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("/students/all", (Studentrepos student_repo) =>
{
    var students = student_repo.FindAllStudents();
    return Results.Ok(students);
});

app.MapGet("/students/{id:int}", (int id, Studentrepos student_repo) =>
{
    string Id = id.ToString();
    var student = student_repo.FindStudentById(Id);
    return Results.Ok(student);
});


app.MapGet("/groups/all", (GroupRepos group_repo) =>
{
    var groups = group_repo.FindAllGroups();
    return Results.Ok(groups);
});

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
