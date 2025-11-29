using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReSeedUserHobbyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserHobby",
                keyColumns: new[] { "HobbyId", "UserId" },
                keyValues: new object[] { new Guid("3d726243-3c45-41d1-8a22-bf3397a6e293"), new Guid("236fbdbb-6d8d-488b-93e4-7bc61f121ba1") });

            migrationBuilder.InsertData(
                table: "UserHobby",
                columns: new[] { "HobbyId", "UserId", "CreatedAt", "IsDeleted", "IsSynced", "JoinedAt", "SkillLevel", "UpdatedAt" },
                values: new object[] { new Guid("6d005b97-0d1b-4c17-bc21-050b463be6ec"), new Guid("cf763ac7-7d37-402c-919a-cabf7e1fe9fa"), new DateTime(2025, 11, 29, 10, 23, 2, 462, DateTimeKind.Utc).AddTicks(8137), false, false, new DateTime(2025, 11, 29, 10, 23, 2, 462, DateTimeKind.Utc).AddTicks(8106), "Expert", new DateTime(2025, 11, 29, 10, 23, 2, 462, DateTimeKind.Utc).AddTicks(8138) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserHobby",
                keyColumns: new[] { "HobbyId", "UserId" },
                keyValues: new object[] { new Guid("6d005b97-0d1b-4c17-bc21-050b463be6ec"), new Guid("cf763ac7-7d37-402c-919a-cabf7e1fe9fa") });

            migrationBuilder.InsertData(
                table: "UserHobby",
                columns: new[] { "HobbyId", "UserId", "CreatedAt", "IsDeleted", "IsSynced", "JoinedAt", "SkillLevel", "UpdatedAt" },
                values: new object[] { new Guid("3d726243-3c45-41d1-8a22-bf3397a6e293"), new Guid("236fbdbb-6d8d-488b-93e4-7bc61f121ba1"), new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6879), false, false, new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6834), "Expert", new DateTime(2025, 11, 29, 10, 7, 14, 951, DateTimeKind.Utc).AddTicks(6881) });
        }
    }
}
