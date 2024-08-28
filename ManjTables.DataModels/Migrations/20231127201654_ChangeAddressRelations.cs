using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddressRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Addresses_AddressId",
                table: "Parents");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Childs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Addresses_AddressId",
                table: "Childs",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Addresses_AddressId",
                table: "Parents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Addresses_AddressId",
                table: "Childs");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Addresses_AddressId",
                table: "Parents");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Childs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Addresses_AddressId",
                table: "Parents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
