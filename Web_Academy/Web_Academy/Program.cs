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
    var student = student_repo.FindStudentById(id);
    return Results.Ok(student);
});

app.MapPost("/students/add", (Student student, Studentrepos student_repo) =>
{
    var newId = student_repo.AddStudent(student);
    student.StudentId = newId;
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
