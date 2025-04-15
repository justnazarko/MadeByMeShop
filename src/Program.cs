using MadeByMe.src.Models;
using MadeByMe.src.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// 1. Перевірка та отримання рядка підключення
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration");
}

// 2. Налаштування DbContext з ретраями
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    });

    // Додаткові опції для розробки
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});

// 3. Реєстрація сервісів
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<BuyerCartService>();

// 4. Додаткові сервіси
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

var app = builder.Build();

// 5. Обробка міграцій
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment())
    {
        // Автоміграції тільки для розробки
        await dbContext.Database.MigrateAsync();
    }
    else
    {
        // Перевірка підключення для production
        await dbContext.Database.CanConnectAsync();
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Database initialization failed");
    throw;
}

// 6. Конфігурація middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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