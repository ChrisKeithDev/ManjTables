﻿// <auto-generated />
using System;
using ManjTables.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManjTables.DataModels.Migrations
{
    [DbContext(typeof(ManjTablesContext))]
    [Migration("20231023185749_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("ManjTables.DataModels.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressId");

                    b.HasIndex("StreetAddress");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ApprovedAdult", b =>
                {
                    b.Property<int>("ApprovedAdultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Relationship")
                        .HasColumnType("TEXT");

                    b.Property<string>("WithoutNotice")
                        .HasColumnType("TEXT");

                    b.HasKey("ApprovedAdultId");

                    b.ToTable("ApprovedAdults");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ApprovedAdultAndPickupForm", b =>
                {
                    b.Property<int>("ApprovedAdultAndPickupFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApprovedAdultId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("PickupFormId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ApprovedAdultAndPickupFormId");

                    b.ToTable("ApprovedAdultAndPickupForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Child", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Allergies")
                        .HasColumnType("TEXT");

                    b.Property<string>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChildFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChildLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChildMiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassroomIds")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hours")
                        .HasColumnType("TEXT");

                    b.Property<string>("Photo")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrimaryLanguage")
                        .HasColumnType("TEXT");

                    b.HasKey("ChildId");

                    b.HasIndex("AddressId");

                    b.HasIndex("BirthDate");

                    b.HasIndex("ClassroomIds");

                    b.HasIndex("Hours");

                    b.ToTable("Childs");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildApprovedAdult", b =>
                {
                    b.Property<int>("ChildApprovedAdultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApprovedAdultId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.HasKey("ChildApprovedAdultId");

                    b.ToTable("ChildApprovedAdults");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildEmergencyContact", b =>
                {
                    b.Property<int>("ChildEmergencyContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmergencyContactId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChildEmergencyContactId");

                    b.ToTable("ChildEmergencyContacts");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildParent", b =>
                {
                    b.Property<int>("ChildParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChildParentId");

                    b.ToTable("ChildParents");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Classroom", b =>
                {
                    b.Property<int>("ClassroomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ClassroomId");

                    b.HasIndex("Level");

                    b.HasIndex("Name");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.EmergencyContact", b =>
                {
                    b.Property<int>("EmergencyContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("EmergencyContactId");

                    b.ToTable("EmergencyContacts");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.EmergencyContactEmergencyInformationForm", b =>
                {
                    b.Property<int>("EmergencyContactEmergencyInformationFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmergencyContactId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmergencyInformationFormId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmergencyContactEmergencyInformationFormId");

                    b.ToTable("EmergencyContactEmergencyInformationForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.FormTemplateIds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnimalApprovalTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ApprovedPickupTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmergencyInformationTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EnrollmentContractId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PhotoReleaseTemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("FormTemplateIdss");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.AnimalApprovalForm", b =>
                {
                    b.Property<int>("AnimalApprovalFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FormTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Permission")
                        .HasColumnType("TEXT");

                    b.Property<string>("SignatureOne")
                        .HasColumnType("TEXT");

                    b.Property<string>("SignatureTwo")
                        .HasColumnType("TEXT");

                    b.HasKey("AnimalApprovalFormId");

                    b.HasIndex("ChildId")
                        .IsUnique();

                    b.ToTable("AnimalApprovalForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.ApprovedPickupForm", b =>
                {
                    b.Property<int>("ApprovedPickupFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FormTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParentGuardian")
                        .HasColumnType("TEXT");

                    b.HasKey("ApprovedPickupFormId");

                    b.HasIndex("ChildId")
                        .IsUnique();

                    b.ToTable("ApprovedPickupForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.EmergencyInformationForm", b =>
                {
                    b.Property<int>("EmergencyInformationFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FormTemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmergencyInformationFormId");

                    b.HasIndex("ChildId")
                        .IsUnique();

                    b.ToTable("EmergencyInformationForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.PhotoReleaseForm", b =>
                {
                    b.Property<int>("PhotoReleaseFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChildId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FormTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Internal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Print")
                        .HasColumnType("TEXT");

                    b.Property<string>("Signature")
                        .HasColumnType("TEXT");

                    b.Property<string>("Website")
                        .HasColumnType("TEXT");

                    b.HasKey("PhotoReleaseFormId");

                    b.HasIndex("ChildId")
                        .IsUnique();

                    b.ToTable("PhotoReleaseForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Parent", b =>
                {
                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShareContactInfo")
                        .HasColumnType("TEXT");

                    b.HasKey("ParentId");

                    b.HasIndex("AddressId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Programme", b =>
                {
                    b.Property<int>("ProgrammeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClassroomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("DismissalTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ecp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProgrammeName")
                        .HasColumnType("TEXT");

                    b.HasKey("ProgrammeId");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.StaffClassroom", b =>
                {
                    b.Property<int>("StaffClassroomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassroomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("StaffId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StaffClassroomId");

                    b.ToTable("StaffClassrooms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.StaffMember", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClassroomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("StaffMembers");
                });
#pragma warning restore 612, 618
        }
    }
}