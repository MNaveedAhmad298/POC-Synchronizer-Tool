using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserHobbyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserHobby",
                keyColumns: new[] { "HobbyId", "UserId" },
                keyValues: new object[] { new Guid("a2311cc1-f0aa-49cf-9adf-927734bf9398"), new Guid("64614f8f-5731-442e-b581-aaced7c2741b") });

            migrationBuilder.InsertData(
                table: "UserHobby",
                columns: new[] { "HobbyId", "UserId", "CreatedAt", "IsDeleted", "IsSynced", "JoinedAt", "SkillLevel", "UpdatedAt" },
                values: new object[] { new Guid("3d726243-3c45-41d1-8a22-bf3397a6e293"), new Guid("236fbdbb-6d8d-488b-93e4-7bc61f121ba1"), new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6879), false, false, new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6834), "Expert", new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6881) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserHobby",
                keyColumns: new[] { "HobbyId", "UserId" },
                keyValues: new object[] { new Guid("3d726243-3c45-41d1-8a22-bf3397a6e293"), new Guid("236fbdbb-6d8d-488b-93e4-7bc61f121ba1") });

            migrationBuilder.InsertData(
                table: "UserHobby",
                columns: new[] { "HobbyId", "UserId", "CreatedAt", "IsDeleted", "IsSynced", "JoinedAt", "SkillLevel", "UpdatedAt" },
                values: new object[] { new Guid("a2311cc1-f0aa-49cf-9adf-927734bf9398"), new Guid("64614f8f-5731-442e-b581-aaced7c2741b"), new DateTime(2025, 11, 28, 6, 54, 16, 718, DateTimeKind.Utc).AddTicks(6868), false, false, new DateTime(2025, 11, 28, 6, 54, 16, 718, DateTimeKind.Utc).AddTicks(6841), "Expert", new DateTime(2025, 11, 28, 6, 54, 16, 718, DateTimeKind.Utc).AddTicks(6869) });
        }
    }
}
