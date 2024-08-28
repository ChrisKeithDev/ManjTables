using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class FixingProgramme3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_Programmes_ClassroomId",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Programmes");

            migrationBuilder.AlterColumn<string>(
                name: "ClassroomId",
                table: "StaffClassrooms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ClassroomIds",
                table: "Programmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClassroomId",
                table: "Classrooms",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_ClassroomIds",
                table: "Programmes",
                column: "ClassroomIds");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_Programmes_ClassroomIds",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "ClassroomIds",
                table: "Programmes");

            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "StaffClassrooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Programmes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "Classrooms",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_ClassroomId",
                table: "Programmes",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
