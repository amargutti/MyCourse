using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Application.Courses;
using MyCourse.Models.Services.Application.Lessons;
using MyCourse.Models.Services.Infrastructure;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICourseService, AdoNetCourseService>();
builder.Services.AddTransient<ICachedCourseService, MemoryCachedCourseService>();
builder.Services.AddTransient<IDatabaseAccessor, SQLServerDatabaseAccessor>();
builder.Services.AddSingleton<IImagePersister, InsecureImagePersister>();
builder.Services.AddTransient<ErrorService>();
builder.Services.AddTransient<ILessonService, AdoNetLessonService>();

builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Home",
        new CacheProfile
        {
            Duration = 60,
            Location = ResponseCacheLocation.Client
        });
});

builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<CoursesOptions>(builder.Configuration.GetSection("Courses"));
builder.Services.Configure<MemoryCacheOptions>(builder.Configuration.GetSection("MemoryCache"));
builder.Services.Configure<CacheTimerOptions>(builder.Configuration.GetSection("MemoryCache"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); //Middleware per recuperare i file nella cartella wwwroot (es. immagini)

//var appCulture = CultureInfo.InvariantCulture;
//app.UseRequestLocalization(new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(appCulture),
//    SupportedCultures = new[] { appCulture }
//});

app.UseResponseCaching();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();