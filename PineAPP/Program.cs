using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using PineAPP.Controllers;
using PineAPP.Data;
using PineAPP.Exceptions;
using PineAPP.Services;
using PineAPP.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

const string allowSpecificOrigin = "AllowSpecificOrigin";
const string origin = "https://localhost:44496";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigin,
        policy  =>
        {
            policy.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnection"));
});

builder.Services.AddTransient<IDecksRepository, DecksRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<ICardsRepository, CardsRepository>();
builder.Services.AddTransient<IUserValidationService, UserValidationService>();
builder.Services.AddTransient<IDeckValidationService, DeckValidationService>();

builder.Logging.AddErrorLogger(configuration =>
{
    builder.Configuration.GetSection("FileErrorLogger").Bind(configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(allowSpecificOrigin);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();