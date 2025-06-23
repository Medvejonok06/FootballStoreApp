using FootballStoreApp.Models;
using FootballStoreApp.Dtos;
using FootballStoreApp.Services;
using FootballStoreApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var optionsBuilder = new DbContextOptionsBuilder<FootballStoreContext>();
optionsBuilder.UseNpgsql(config.GetConnectionString("FootballStore"));

using var context = new FootballStoreContext(optionsBuilder.Options);
var itemService = new ItemService(context);

// Очистити всі товари
DeleteAllItems(context);

// Створити категорію
EnsureCategory(context);

// Додати новий товар через сервіс
await itemService.CreateAsync(new CreateItemDto
{
    Name = "Тренувальний м’яч",
    Quantity = 10,
    Description = "Класичний футбольний м’яч для тренувань",
    IsOnSale = true,
    PurchasedDate = DateTime.UtcNow,
    CurrentOrFinalPrice = 650,
    CategoryId = context.Categories.First().Id
});

// Вивести всі товари через сервіс
Console.WriteLine("🛒 Товари в базі:");
var items = await itemService.GetAllAsync(new ItemQueryParameters());
foreach (var item in items)
{
    Console.WriteLine($"- {item.Id}: {item.Name}, {item.CurrentOrFinalPrice}₴, OnSale: {item.IsOnSale}");
}

// 🔧 Старі методи (крім CreateItem) залишаються:

static void EnsureCategory(FootballStoreContext context)
{
    if (!context.Categories.Any())
    {
        var category = new Category { Name = "Екіпірування" };
        context.Categories.Add(category);
        context.SaveChanges();
    }
}

static void DeleteAllItems(FootballStoreContext context)
{
    var items = context.Items.ToList();
    foreach (var item in items)
    {
        context.Items.Remove(item); // soft-delete
    }

    context.SaveChanges();
}
