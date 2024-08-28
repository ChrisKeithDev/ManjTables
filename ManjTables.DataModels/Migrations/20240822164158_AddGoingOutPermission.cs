using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class AddGoingOutPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoingOutPermissionTemplateId",
                table: "FormTemplateIdss",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoingOutPermissionForms",
                columns: table => new
                {
                    GoingOutPermissionFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: true),
                    Contact = table.Column<string>(type: "TEXT", nullable: true),
                    DriverAvailability = table.Column<string>(type: "TEXT", nullable: true),
                    ParentGuardianSignature = table.Column<string>(type: "TEXT", nullable: true),
                    PermissionToParticipate = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoingOutPermissionForms", x => x.GoingOutPermissionFormId);
                    table.ForeignKey(
                        name: "FK_GoingOutPermissionForms_Childs_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Childs",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoingOutPermissionForms_ChildId",
                table: "GoingOutPermissionForms",
                column: "ChildId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoingOutPermissionForms");

            migrationBuilder.DropColumn(
                name: "GoingOutPermissionTemplateId",
                table: "FormTemplateIdss");
        }
    }
}
