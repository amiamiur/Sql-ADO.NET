using AcademyApp.data;

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
