using FootballStoreApp.Models;
using FootballStoreApp.Repositories;
using FootballStoreApp.Services;
using FootballStoreApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Налаштування DbContext з конекшеном
builder.Services.AddDbContext<FootballStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FootballStore")));

// Додаємо UnitOfWork, репозиторії та сервіси
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Додаємо контролери
builder.Services.AddControllers();

// Підключаємо FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Додаємо Swagger для документації
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Використовуємо Swagger завжди, без перевірки середовища
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
