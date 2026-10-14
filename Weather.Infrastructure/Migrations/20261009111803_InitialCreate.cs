using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityOfSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id"),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Субъект РФ"),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Административный центр"),
                    Lat = table.Column<double>(type: "double precision", precision: 18, scale: 8, nullable: false, comment: "Широта"),
                    Lon = table.Column<double>(type: "double precision", precision: 18, scale: 8, nullable: false, comment: "Долгота")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityOfSubjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityOfSubjects_Subject_City",
                table: "CityOfSubjects",
                columns: new[] { "Subject", "City" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityOfSubjects");
        }
    }
}
