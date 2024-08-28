using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Ecp",
                table: "Programmes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammeId",
                table: "Childs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ProgrammeId",
                table: "Childs",
                column: "ProgrammeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Programmes_ProgrammeId",
                table: "Childs",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "ProgrammeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Programmes_ProgrammeId",
                table: "Childs");

            migrationBuilder.DropIndex(
                name: "IX_Childs_ProgrammeId",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "ProgrammeId",
                table: "Childs");

            migrationBuilder.AlterColumn<string>(
                name: "Ecp",
                table: "Programmes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
