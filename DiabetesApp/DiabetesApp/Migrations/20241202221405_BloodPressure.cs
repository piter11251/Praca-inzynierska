using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Migrations
{
    /// <inheritdoc />
    public partial class BloodPressure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pressures_Users_UserId",
                table: "Pressures");

            migrationBuilder.RenameColumn(
                name: "DiaStolicPressure",
                table: "Pressures",
                newName: "DiastolicPressure");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Pressures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIrregularPulse",
                table: "Pressures",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Pressures_Users_UserId",
                table: "Pressures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pressures_Users_UserId",
                table: "Pressures");

            migrationBuilder.RenameColumn(
                name: "DiastolicPressure",
                table: "Pressures",
                newName: "DiaStolicPressure");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Pressures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIrregularPulse",
                table: "Pressures",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pressures_Users_UserId",
                table: "Pressures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
