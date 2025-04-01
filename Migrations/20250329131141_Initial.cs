using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuathAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesOfLogin = table.Column<int>(type: "int", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastLoginAt", "LastName", "Password", "Role", "TimesOfLogin" },
                values: new object[] { new Guid("e58b5d4c-8f57-4c0b-a2b5-6efc3a7a620c"), "Admin", "AdminUser@gmail.com", "Admin", new DateTime(2024, 3, 29, 12, 0, 0, 0, DateTimeKind.Utc), "Admin", "$2a$11$hsXlBxzfPhPmlqraxM4hlOV.z6Miz2/AHnT5.i2MX3XH/fqxVTOAm", "Admin", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
