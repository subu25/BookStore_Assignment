using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using BookStore.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddScoped<IGenreService,GenreService>();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<IPublisherService,PublisherService>();
builder.Services.AddScoped<IBookService,BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
