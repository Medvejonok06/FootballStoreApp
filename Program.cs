using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using FootballStoreApp.Interfaces;
using FootballStoreApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Підключення до PostgreSQL
builder.Services.AddDbContext<FootballStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FootballStore")));

// Swagger та DI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

// Додати тестові дані
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FootballStoreContext>();

    var category = context.Categories.FirstOrDefault();
    if (category == null)
    {
        category = new Category { Name = "Взуття" };
        context.Categories.Add(category);
        context.SaveChanges();
    }

    if (!context.Items.Any())
    {
        context.Items.AddRange(
            new Item
            {
                Name = "Бутси Pro Control",
                Description = "Футбольні бутси для гри на газоні",
                Quantity = 10,
                PurchasePrice = 2000,
                CurrentOrFinalPrice = 2499,
                IsOnSale = true,
                PurchasedDate = DateTime.UtcNow.AddDays(-10),
                CategoryId = category.Id,
                IsActive = true
            },
            new Item
            {
                Name = "М’яч тренувальний",
                Description = "М’яч для аматорських тренувань",
                Quantity = 4,
                PurchasePrice = 300,
                CurrentOrFinalPrice = 450,
                IsOnSale = false,
                PurchasedDate = DateTime.UtcNow.AddDays(-5),
                CategoryId = category.Id,
                IsActive = true
            }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
