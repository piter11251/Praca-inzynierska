using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDatabaseStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Gener",
                table: "Users",
                newName: "Gender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MealTime",
                table: "Entries",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Pressures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StolicPressure = table.Column<int>(type: "int", nullable: false),
                    DiaStolicPressure = table.Column<int>(type: "int", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    IsIrregularPulse = table.Column<bool>(type: "bit", nullable: false),
                    MeasureDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pressures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pressures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferableSugarLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserPreferencesId = table.Column<int>(type: "int", nullable: false),
                    MealMarker = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferableSugarLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferableSugarLevel_UserPreferences_UserPreferencesId",
                        column: x => x.UserPreferencesId,
                        principalTable: "UserPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferableSugarLevel_UserPreferencesId",
                table: "PreferableSugarLevel",
                column: "UserPreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pressures_UserId",
                table: "Pressures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferableSugarLevel");

            migrationBuilder.DropTable(
                name: "Pressures");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "Gener");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MealTime",
                table: "Entries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
