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
    [Migration("20231120025211_AddProgramme2")]
    partial class AddProgramme2
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

                    b.HasIndex("ApprovedAdultId");

                    b.HasIndex("PickupFormId");

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

                    b.Property<int?>("ProgrammeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChildId");

                    b.HasIndex("AddressId");

                    b.HasIndex("BirthDate");

                    b.HasIndex("ClassroomIds");

                    b.HasIndex("Hours");

                    b.HasIndex("ProgrammeId");

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

                    b.HasIndex("ApprovedAdultId");

                    b.HasIndex("ChildId");

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

                    b.HasIndex("ChildId");

                    b.HasIndex("EmergencyContactId");

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

                    b.HasIndex("ChildId");

                    b.HasIndex("ParentId");

                    b.ToTable("ChildParents");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Classroom", b =>
                {
                    b.Property<int>("ClassroomId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClassroomName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LessonSetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.HasKey("ClassroomId");

                    b.HasIndex("ClassroomName");

                    b.HasIndex("Level");

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

                    b.HasIndex("EmergencyContactId");

                    b.HasIndex("EmergencyInformationFormId");

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

                    b.Property<int?>("ParentSelector")
                        .HasColumnType("INTEGER");

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

                    b.Property<bool?>("Ecp")
                        .HasColumnType("INTEGER");

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

                    b.HasIndex("ClassroomId");

                    b.HasIndex("StaffId");

                    b.ToTable("StaffClassrooms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.StaffMember", b =>
                {
                    b.Property<int>("StaffId")
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

            modelBuilder.Entity("ManjTables.DataModels.Models.ApprovedAdultAndPickupForm", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.ApprovedAdult", "ApprovedAdult")
                        .WithMany("ApprovedAdultAndPickupForms")
                        .HasForeignKey("ApprovedAdultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.Forms.ApprovedPickupForm", "ApprovedPickupForm")
                        .WithMany("ApprovedAdultAndPickupForms")
                        .HasForeignKey("PickupFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedAdult");

                    b.Navigation("ApprovedPickupForm");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Child", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.Programme", "Programme")
                        .WithMany("Children")
                        .HasForeignKey("ProgrammeId");

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildApprovedAdult", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.ApprovedAdult", "ApprovedAdult")
                        .WithMany("ChildApprovedAdults")
                        .HasForeignKey("ApprovedAdultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.Child", "Child")
                        .WithMany("ChildApprovedAdults")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedAdult");

                    b.Navigation("Child");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildEmergencyContact", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.Child", "Child")
                        .WithMany("ChildEmergencyContacts")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.EmergencyContact", "EmergencyContact")
                        .WithMany("ChildEmergencyContacts")
                        .HasForeignKey("EmergencyContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("EmergencyContact");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ChildParent", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.Child", "Child")
                        .WithMany("ChildParents")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.Parent", "Parent")
                        .WithMany("ChildParents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.EmergencyContactEmergencyInformationForm", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.EmergencyContact", "EmergencyContact")
                        .WithMany("EmergencyContactEmergencyInformationForms")
                        .HasForeignKey("EmergencyContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.Forms.EmergencyInformationForm", "EmergencyInformationForm")
                        .WithMany("EmergencyContactEmergencyInformationForms")
                        .HasForeignKey("EmergencyInformationFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmergencyContact");

                    b.Navigation("EmergencyInformationForm");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.StaffClassroom", b =>
                {
                    b.HasOne("ManjTables.DataModels.Models.Classroom", "Classroom")
                        .WithMany("StaffClassrooms")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManjTables.DataModels.Models.StaffMember", "StaffMember")
                        .WithMany("StaffClassrooms")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.ApprovedAdult", b =>
                {
                    b.Navigation("ApprovedAdultAndPickupForms");

                    b.Navigation("ChildApprovedAdults");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Child", b =>
                {
                    b.Navigation("ChildApprovedAdults");

                    b.Navigation("ChildEmergencyContacts");

                    b.Navigation("ChildParents");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Classroom", b =>
                {
                    b.Navigation("StaffClassrooms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.EmergencyContact", b =>
                {
                    b.Navigation("ChildEmergencyContacts");

                    b.Navigation("EmergencyContactEmergencyInformationForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.ApprovedPickupForm", b =>
                {
                    b.Navigation("ApprovedAdultAndPickupForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Forms.EmergencyInformationForm", b =>
                {
                    b.Navigation("EmergencyContactEmergencyInformationForms");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Parent", b =>
                {
                    b.Navigation("ChildParents");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.Programme", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("ManjTables.DataModels.Models.StaffMember", b =>
                {
                    b.Navigation("StaffClassrooms");
                });
#pragma warning restore 612, 618
        }
    }
}