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

app.MapPut("/students/{id:int}", async (int id, Student student, CollegeDbContext db) =>
{
    var existingStudent = await db.Students.FindAsync(id);
    existingStudent.FirstName = student.FirstName;
    existingStudent.LastName = student.LastName;
    existingStudent.Age = student.Age;
    existingStudent.GroupId = student.GroupId;

    await db.SaveChangesAsync();
    return Results.Ok(existingStudent);
});

app.MapDelete("/students/{id:int}", async (int id, CollegeDbContext db) =>
{
    var student = await db.Students.FindAsync(id);

    db.Students.Remove(student);
    await db.SaveChangesAsync();

    return Results.Ok();
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
