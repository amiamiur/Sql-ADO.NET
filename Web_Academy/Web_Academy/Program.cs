using AcademyApp.data;
using AcademyApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var conn_str = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<CollegeDbContext>(options =>
    options.UseSqlServer(conn_str));

builder.Services.AddSingleton(new Studentrepos(conn_str));
builder.Services.AddSingleton(new GroupRepos(conn_str));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("/students/all", async (CollegeDbContext db) =>
{
    var students = await db.Students.ToListAsync();
    return Results.Ok(students);
});

app.MapGet("/students/{id:int}", (int id, Studentrepos student_repo) =>
{
    var student = student_repo.FindStudentById(id);
    return Results.Ok(student);
});

app.MapPost("/students/add", async (Student student, CollegeDbContext db) =>
{
    db.Students.Add(student);
    await db.SaveChangesAsync();
    return Results.Ok(student);
});

app.MapPut("/students/{id:int}", (int id, Student student, Studentrepos student_repo) =>
{
    student.StudentId = id;
    var rows = student_repo.UpdateStudent(student);
    return rows > 0 ? Results.Ok(student) : Results.NotFound();
});

app.MapDelete("/students/{id:int}", (int id, Studentrepos student_repo) =>
{
    var rows = student_repo.DeleteStudent(id);
    return rows > 0 ? Results.Ok() : Results.NotFound();
});


app.MapGet("/groups/all", async (CollegeDbContext db) =>
{
    var groups = await db.Groups.ToListAsync();
    return Results.Ok(groups);
});

app.MapPost("/groups/add", async (Group group, CollegeDbContext db) =>
{
    db.Groups.Add(group);
    await db.SaveChangesAsync();
    return Results.Ok(group);
});

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
