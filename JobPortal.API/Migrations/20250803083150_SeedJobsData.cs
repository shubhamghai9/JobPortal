using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedJobsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Company", "Description", "Location", "PostedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Tech Solutions Inc.", "Build scalable .NET applications.", "Bangalore", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software Engineer" },
                    { 2, "PixelCraft", "Develop UI with React and Tailwind.", "Remote", new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frontend Developer" },
                    { 3, "API Architects", "Design REST APIs using ASP.NET Core.", "Delhi", new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Backend Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
