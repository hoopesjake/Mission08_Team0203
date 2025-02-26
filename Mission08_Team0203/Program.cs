using Microsoft.EntityFrameworkCore;
using Mission08_Team0203.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("TaskConnection"));
    // Or if using SQL Server:
    // options.UseSqlServer(builder.Configuration.GetConnectionString("TaskConnection"));
});

// Register the repository
builder.Services.AddScoped<ITaskRepository, EFTaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Quadrant}/{id?}");

app.Run();
