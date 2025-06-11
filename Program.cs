using FootballStoreApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        // 1. Завантаження конфігурації з appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // 2. Створення опцій для DbContext
        var optionsBuilder = new DbContextOptionsBuilder<FootballStoreContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("FootballStore"));

        // 3. Використання контексту для читання даних
        using var context = new FootballStoreContext(optionsBuilder.Options);

        var products = context.Products.ToList();

        Console.WriteLine("🛒 Список товарів у магазині:\n");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id}. {p.Name} — {p.Price}₴ (Залишок: {p.StockQuantity})");
        }
    }
}
