using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreetAddress = table.Column<string>(type: "TEXT", nullable: true),
                    StreetAddress2 = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AnimalApprovalForms",
                columns: table => new
                {
                    AnimalApprovalFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    Permission = table.Column<string>(type: "TEXT", nullable: true),
                    SignatureOne = table.Column<string>(type: "TEXT", nullable: true),
                    SignatureTwo = table.Column<string>(type: "TEXT", nullable: true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalApprovalForms", x => x.AnimalApprovalFormId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedAdultAndPickupForms",
                columns: table => new
                {
                    ApprovedAdultAndPickupFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApprovedAdultId = table.Column<int>(type: "INTEGER", nullable: false),
                    PickupFormId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedAdultAndPickupForms", x => x.ApprovedAdultAndPickupFormId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedAdults",
                columns: table => new
                {
                    ApprovedAdultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Relationship = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    WithoutNotice = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedAdults", x => x.ApprovedAdultId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedPickupForms",
                columns: table => new
                {
                    ApprovedPickupFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentGuardian = table.Column<string>(type: "TEXT", nullable: true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedPickupForms", x => x.ApprovedPickupFormId);
                });

            migrationBuilder.CreateTable(
                name: "ChildApprovedAdults",
                columns: table => new
                {
                    ChildApprovedAdultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApprovedAdultId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildApprovedAdults", x => x.ChildApprovedAdultId);
                });

            migrationBuilder.CreateTable(
                name: "ChildEmergencyContacts",
                columns: table => new
                {
                    ChildEmergencyContactId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmergencyContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildEmergencyContacts", x => x.ChildEmergencyContactId);
                });

            migrationBuilder.CreateTable(
                name: "ChildParents",
                columns: table => new
                {
                    ChildParentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildParents", x => x.ChildParentId);
                });

            migrationBuilder.CreateTable(
                name: "Childs",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildFirstName = table.Column<string>(type: "TEXT", nullable: true),
                    ChildLastName = table.Column<string>(type: "TEXT", nullable: true),
                    ChildMiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    Photo = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDate = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    Hours = table.Column<string>(type: "TEXT", nullable: true),
                    Allergies = table.Column<string>(type: "TEXT", nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true),
                    ClassroomIds = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.ChildId);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassroomId);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContactEmergencyInformationForms",
                columns: table => new
                {
                    EmergencyContactEmergencyInformationFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmergencyContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmergencyInformationFormId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContactEmergencyInformationForms", x => x.EmergencyContactEmergencyInformationFormId);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    EmergencyContactId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.EmergencyContactId);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyInformationForms",
                columns: table => new
                {
                    EmergencyInformationFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyInformationForms", x => x.EmergencyInformationFormId);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplateIdss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnrollmentContractId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmergencyInformationTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ApprovedPickupTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    PhotoReleaseTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    AnimalApprovalTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateIdss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ShareContactInfo = table.Column<string>(type: "TEXT", nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ParentId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoReleaseForms",
                columns: table => new
                {
                    PhotoReleaseFormId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: true),
                    Print = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Internal = table.Column<string>(type: "TEXT", nullable: true),
                    Signature = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoReleaseForms", x => x.PhotoReleaseFormId);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    ProgrammeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    ProgrammeName = table.Column<string>(type: "TEXT", nullable: true),
                    Ecp = table.Column<string>(type: "TEXT", nullable: true),
                    DismissalTime = table.Column<string>(type: "TEXT", nullable: true),
                    ClassroomId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.ProgrammeId);
                });

            migrationBuilder.CreateTable(
                name: "StaffClassrooms",
                columns: table => new
                {
                    StaffClassroomId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassroomId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffClassrooms", x => x.StaffClassroomId);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    ClassroomId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.StaffId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetAddress",
                table: "Addresses",
                column: "StreetAddress");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalApprovalForms_ChildId",
                table: "AnimalApprovalForms",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedPickupForms_ChildId",
                table: "ApprovedPickupForms",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Childs_AddressId",
                table: "Childs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Childs_BirthDate",
                table: "Childs",
                column: "BirthDate");

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ClassroomIds",
                table: "Childs",
                column: "ClassroomIds");

            migrationBuilder.CreateIndex(
                name: "IX_Childs_Hours",
                table: "Childs",
                column: "Hours");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Level",
                table: "Classrooms",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Name",
                table: "Classrooms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyInformationForms_ChildId",
                table: "EmergencyInformationForms",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_AddressId",
                table: "Parents",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoReleaseForms_ChildId",
                table: "PhotoReleaseForms",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_ClassroomId",
                table: "Programmes",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AnimalApprovalForms");

            migrationBuilder.DropTable(
                name: "ApprovedAdultAndPickupForms");

            migrationBuilder.DropTable(
                name: "ApprovedAdults");

            migrationBuilder.DropTable(
                name: "ApprovedPickupForms");

            migrationBuilder.DropTable(
                name: "ChildApprovedAdults");

            migrationBuilder.DropTable(
                name: "ChildEmergencyContacts");

            migrationBuilder.DropTable(
                name: "ChildParents");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "EmergencyContactEmergencyInformationForms");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "EmergencyInformationForms");

            migrationBuilder.DropTable(
                name: "FormTemplateIdss");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "PhotoReleaseForms");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "StaffClassrooms");

            migrationBuilder.DropTable(
                name: "StaffMembers");
        }
    }
}
