using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class MigrationTwo10_23_23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentSelector",
                table: "Parents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffClassrooms_ClassroomId",
                table: "StaffClassrooms",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffClassrooms_StaffId",
                table: "StaffClassrooms",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContactEmergencyInformationForms_EmergencyContactId",
                table: "EmergencyContactEmergencyInformationForms",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContactEmergencyInformationForms_EmergencyInformationFormId",
                table: "EmergencyContactEmergencyInformationForms",
                column: "EmergencyInformationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildParents_ChildId",
                table: "ChildParents",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildParents_ParentId",
                table: "ChildParents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildEmergencyContacts_ChildId",
                table: "ChildEmergencyContacts",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildEmergencyContacts_EmergencyContactId",
                table: "ChildEmergencyContacts",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildApprovedAdults_ApprovedAdultId",
                table: "ChildApprovedAdults",
                column: "ApprovedAdultId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildApprovedAdults_ChildId",
                table: "ChildApprovedAdults",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedAdultAndPickupForms_ApprovedAdultId",
                table: "ApprovedAdultAndPickupForms",
                column: "ApprovedAdultId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedAdultAndPickupForms_PickupFormId",
                table: "ApprovedAdultAndPickupForms",
                column: "PickupFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedAdultAndPickupForms_ApprovedAdults_ApprovedAdultId",
                table: "ApprovedAdultAndPickupForms",
                column: "ApprovedAdultId",
                principalTable: "ApprovedAdults",
                principalColumn: "ApprovedAdultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedAdultAndPickupForms_ApprovedPickupForms_PickupFormId",
                table: "ApprovedAdultAndPickupForms",
                column: "PickupFormId",
                principalTable: "ApprovedPickupForms",
                principalColumn: "ApprovedPickupFormId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildApprovedAdults_ApprovedAdults_ApprovedAdultId",
                table: "ChildApprovedAdults",
                column: "ApprovedAdultId",
                principalTable: "ApprovedAdults",
                principalColumn: "ApprovedAdultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildApprovedAdults_Childs_ChildId",
                table: "ChildApprovedAdults",
                column: "ChildId",
                principalTable: "Childs",
                principalColumn: "ChildId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildEmergencyContacts_Childs_ChildId",
                table: "ChildEmergencyContacts",
                column: "ChildId",
                principalTable: "Childs",
                principalColumn: "ChildId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildEmergencyContacts_EmergencyContacts_EmergencyContactId",
                table: "ChildEmergencyContacts",
                column: "EmergencyContactId",
                principalTable: "EmergencyContacts",
                principalColumn: "EmergencyContactId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildParents_Childs_ChildId",
                table: "ChildParents",
                column: "ChildId",
                principalTable: "Childs",
                principalColumn: "ChildId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildParents_Parents_ParentId",
                table: "ChildParents",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContactEmergencyInformationForms_EmergencyContacts_EmergencyContactId",
                table: "EmergencyContactEmergencyInformationForms",
                column: "EmergencyContactId",
                principalTable: "EmergencyContacts",
                principalColumn: "EmergencyContactId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContactEmergencyInformationForms_EmergencyInformationForms_EmergencyInformationFormId",
                table: "EmergencyContactEmergencyInformationForms",
                column: "EmergencyInformationFormId",
                principalTable: "EmergencyInformationForms",
                principalColumn: "EmergencyInformationFormId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffClassrooms_StaffMembers_StaffId",
                table: "StaffClassrooms",
                column: "StaffId",
                principalTable: "StaffMembers",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedAdultAndPickupForms_ApprovedAdults_ApprovedAdultId",
                table: "ApprovedAdultAndPickupForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedAdultAndPickupForms_ApprovedPickupForms_PickupFormId",
                table: "ApprovedAdultAndPickupForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildApprovedAdults_ApprovedAdults_ApprovedAdultId",
                table: "ChildApprovedAdults");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildApprovedAdults_Childs_ChildId",
                table: "ChildApprovedAdults");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildEmergencyContacts_Childs_ChildId",
                table: "ChildEmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildEmergencyContacts_EmergencyContacts_EmergencyContactId",
                table: "ChildEmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildParents_Childs_ChildId",
                table: "ChildParents");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildParents_Parents_ParentId",
                table: "ChildParents");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContactEmergencyInformationForms_EmergencyContacts_EmergencyContactId",
                table: "EmergencyContactEmergencyInformationForms");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContactEmergencyInformationForms_EmergencyInformationForms_EmergencyInformationFormId",
                table: "EmergencyContactEmergencyInformationForms");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffClassrooms_Classrooms_ClassroomId",
                table: "StaffClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffClassrooms_StaffMembers_StaffId",
                table: "StaffClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_StaffClassrooms_ClassroomId",
                table: "StaffClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_StaffClassrooms_StaffId",
                table: "StaffClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyContactEmergencyInformationForms_EmergencyContactId",
                table: "EmergencyContactEmergencyInformationForms");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyContactEmergencyInformationForms_EmergencyInformationFormId",
                table: "EmergencyContactEmergencyInformationForms");

            migrationBuilder.DropIndex(
                name: "IX_ChildParents_ChildId",
                table: "ChildParents");

            migrationBuilder.DropIndex(
                name: "IX_ChildParents_ParentId",
                table: "ChildParents");

            migrationBuilder.DropIndex(
                name: "IX_ChildEmergencyContacts_ChildId",
                table: "ChildEmergencyContacts");

            migrationBuilder.DropIndex(
                name: "IX_ChildEmergencyContacts_EmergencyContactId",
                table: "ChildEmergencyContacts");

            migrationBuilder.DropIndex(
                name: "IX_ChildApprovedAdults_ApprovedAdultId",
                table: "ChildApprovedAdults");

            migrationBuilder.DropIndex(
                name: "IX_ChildApprovedAdults_ChildId",
                table: "ChildApprovedAdults");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedAdultAndPickupForms_ApprovedAdultId",
                table: "ApprovedAdultAndPickupForms");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedAdultAndPickupForms_PickupFormId",
                table: "ApprovedAdultAndPickupForms");

            migrationBuilder.DropColumn(
                name: "ParentSelector",
                table: "Parents");
        }
    }
}
