using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class AddClassroomsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Classrooms",
                newName: "ClassroomName");

            migrationBuilder.RenameIndex(
                name: "IX_Classrooms_Name",
                table: "Classrooms",
                newName: "IX_Classrooms_ClassroomName");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Classrooms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonSetId",
                table: "Classrooms",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "LessonSetId",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "ClassroomName",
                table: "Classrooms",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Classrooms_ClassroomName",
                table: "Classrooms",
                newName: "IX_Classrooms_Name");
        }
    }
}
