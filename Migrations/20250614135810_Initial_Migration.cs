using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Існуюча база даних. Таблиці створені вручну.
            // Тому нічого не створюємо в цій початковій міграції.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Початкова міграція — відкат не передбачається.
        }
    }
}
