using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001")//permite escuchar SOLO peticiones locales
builder.WebHost.UseUrls("http://localhost:5200", "http://*:5200");//permite escuchar peticiones locales y remotas

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Usuario/Loggin";
		options.LogoutPath = "/Usuario/Logout";
        options.AccessDeniedPath = "/Home";
	});

  


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireClaim("Rol", "Administrador"));
   // options.AddPolicy("Empleado", policy => policy.RequireClaim("Rol", "Empleado"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Loggin",
    pattern: "Loggin",
    defaults: new { controller = "Usuario", action = "Loggin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
