using System.Net.Http.Headers;
using System.Numerics;
using eMeterSite.Data;
using eMeterSite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.ConfigureApplicationCookie( options => options.LoginPath = "/Authentication" );
builder.Services.AddSession( options => {
    options.IdleTimeout = TimeSpan.FromHours(6);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpClient( "eMeterApi", o => {
    o.BaseAddress = new Uri( builder.Configuration.GetValue<string>("eMeterApi")!);
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<AuthenticationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
