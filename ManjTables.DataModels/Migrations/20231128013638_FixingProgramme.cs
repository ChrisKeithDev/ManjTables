using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class FixingProgramme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Programmes_ProgrammeId",
                table: "Childs");

            migrationBuilder.AlterColumn<string>(
                name: "ProgrammeName",
                table: "Programmes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Programmes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "Ecp",
                table: "Programmes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "DismissalTime",
                table: "Programmes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammeId",
                table: "Childs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

            migrationBuilder.AlterColumn<string>(
                name: "ProgrammeName",
                table: "Programmes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Programmes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ecp",
                table: "Programmes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DismissalTime",
                table: "Programmes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammeId",
                table: "Childs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Programmes_ProgrammeId",
                table: "Childs",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "ProgrammeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
