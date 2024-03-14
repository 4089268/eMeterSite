using System.Net.Http.Headers;
using System.Numerics;
using eMeterSite.Data;
using eMeterSite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient( "eMeterApi", o => {
    o.BaseAddress = new Uri( builder.Configuration.GetValue<string>("eMeterApi")!);
    o.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
        builder.Configuration.GetValue<string>("eMeterApiToken")!
    );
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAppService, AppService>();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
