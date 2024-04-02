using Microsoft.EntityFrameworkCore;
using WaterProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WaterProjectContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionString:WaterConnection"]);
}
);

builder.Services.AddScoped<IWaterRepository, EFWaterRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache(); // stores cached data in memory across the application instances
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // required to access the current session in the SessionCart class

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

// These have to be in our preferred order
app.MapControllerRoute("pageNumAndType", "{projectType}/{pageNum}", new {Controller = "Home", action = "Index"});
app.MapControllerRoute("pagination", "{pageNum}", new {Controller = "Home", action = "Index", pageNum = 1});
app.MapControllerRoute("projectType", "{projectType}", new { Controller = "Home", action = "Index", pageNum = 1 });
app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
