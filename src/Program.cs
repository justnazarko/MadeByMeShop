using MadeByMe.src.Models;
using MadeByMe.src.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Додаємо базу даних PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(connectionString));

// Додаємо сервіси
builder.Services.AddScoped<PostService>();

// Додаємо контролери та представлення
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Автоматично застосовуємо міграції при запуску
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	dbContext.Database.Migrate();
}

// Налаштовуємо pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Маршрутизація
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
