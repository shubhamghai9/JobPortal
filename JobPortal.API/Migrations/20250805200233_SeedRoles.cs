using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", "Admin", "admin" },
                    { 2, "UAa8nEoRaEMHuyDgSiJiWmmkdVOUO+9aQ1wm27xrXag=", "Recruiter", "recruiter" },
                    { 3, "MCIdwsTWjlF2InrvIxaNzO+v2rPi6G1a16mlSfYo7SM=", "Candidate", "candidate" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

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
    }
}
