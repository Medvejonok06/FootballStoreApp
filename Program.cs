using FootballStoreApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var optionsBuilder = new DbContextOptionsBuilder<FootballStoreContext>();
optionsBuilder.UseNpgsql(config.GetConnectionString("FootballStore"));

using var context = new FootballStoreContext(optionsBuilder.Options);

// Очистити всі товари
DeleteAllItems(context);

// Створити новий товар
EnsureItem(context);

// Змінити ціни
UpdateItems(context);

// Вивести всі активні товари
Console.WriteLine("🛒 Товари в базі:");
foreach (var item in context.Items.Where(i => i.IsActive))
{
    Console.WriteLine($"- {item.Id}: {item.Description}, {item.CurrentOrFinalPrice}₴, Created: {item.CreatedDate}, Active: {item.IsActive}");
}


// 🔧 Методи:

static void EnsureItem(FootballStoreContext context)
{
    var item = new Item
    {
        Quantity = 10,
        Description = "Кросівки для футболу",
        Notes = "Новинка сезону",
        IsOnSale = true,
        PurchasedDate = DateTime.UtcNow,
        PurchasePrice = 1200,
        CurrentOrFinalPrice = 1499
    };

    context.Items.Add(item);
    context.SaveChanges();
}

static void UpdateItems(FootballStoreContext context)
{
    var items = context.Items.ToList();

    foreach (var item in items)
    {
        item.CurrentOrFinalPrice += 100; // умовна зміна
    }

    context.SaveChanges();
}

static void DeleteAllItems(FootballStoreContext context)
{
    var items = context.Items.ToList();

    foreach (var item in items)
    {
        context.Items.Remove(item); // буде soft-delete
    }

    context.SaveChanges();
}
