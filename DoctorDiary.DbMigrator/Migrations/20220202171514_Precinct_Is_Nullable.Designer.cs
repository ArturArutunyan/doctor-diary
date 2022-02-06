﻿// <auto-generated />
using System;
using DoctorDiary.DbMigrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DoctorDiary.DbMigrator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220202171514_Precinct_Is_Nullable")]
    partial class Precinct_Is_Nullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DoctorDiary.Models.PatientCards.PatientCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("DATE")
                        .HasColumnName("Birthday");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationTime");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("Description");

                    b.Property<string>("EmploymentPosition")
                        .HasColumnType("TEXT")
                        .HasColumnName("EmploymentPosition");

                    b.Property<string>("FirstName")
                        .HasColumnType("NVARCHAR(30)")
                        .HasColumnName("FirstName");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR(30)")
                        .HasColumnName("LastName");

                    b.Property<string>("Patronymic")
                        .HasColumnType("NVARCHAR(30)")
                        .HasColumnName("Patronymic");

                    b.Property<string>("PlaceOfWork")
                        .HasColumnType("TEXT")
                        .HasColumnName("PlaceOfWork");

                    b.Property<int?>("Precinct")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Precinct");

                    b.HasKey("Id");

                    b.ToTable("PatientCards");
                });

            modelBuilder.Entity("DoctorDiary.Models.Reminders.Reminder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsClosed");

                    b.Property<string>("NavigationLinkOnClick")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("NavigationLinkOnClick");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("TEXT")
                        .HasColumnName("Time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("DoctorDiary.Models.SickLeaves.SickLeave", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationTime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Number")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Number");

                    b.Property<Guid>("PatientCardId")
                        .HasColumnType("TEXT")
                        .HasColumnName("PatientCardId");

                    b.HasKey("Id");

                    b.HasIndex("PatientCardId");

                    b.ToTable("SickLeaves");
                });

            modelBuilder.Entity("DoctorDiary.Models.Visits.Visit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationTime");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsCompleted");

                    b.Property<Guid>("PatientCardId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("DATE")
                        .HasColumnName("Time");

                    b.Property<string>("TypeOfAppeal")
                        .HasColumnType("TEXT")
                        .HasColumnName("TypeOfAppeal");

                    b.HasKey("Id");

                    b.HasIndex("PatientCardId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("DoctorDiary.Models.PatientCards.PatientCard", b =>
                {
                    b.OwnsOne("DoctorDiary.Models.PatientCards.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PatientCardId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Apartment")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("Apartment");

                            b1.Property<string>("City")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("City");

                            b1.Property<string>("Entrance")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("Entrance");

                            b1.Property<string>("Floor")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("Floor");

                            b1.Property<string>("House")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("House");

                            b1.Property<string>("Street")
                                .HasColumnType("NVARCHAR(100)")
                                .HasColumnName("Street");

                            b1.HasKey("PatientCardId");

                            b1.ToTable("PatientCards");

                            b1.WithOwner()
                                .HasForeignKey("PatientCardId");
                        });

                    b.OwnsOne("DoctorDiary.Models.PatientCards.ValueObjects.InsurancePolicy", "InsurancePolicy", b1 =>
                        {
                            b1.Property<Guid>("PatientCardId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(16)")
                                .HasColumnName("InsurancePolicy");

                            b1.HasKey("PatientCardId");

                            b1.ToTable("PatientCards");

                            b1.WithOwner()
                                .HasForeignKey("PatientCardId");
                        });

                    b.OwnsOne("DoctorDiary.Models.PatientCards.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("PatientCardId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(15)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("PatientCardId");

                            b1.ToTable("PatientCards");

                            b1.WithOwner()
                                .HasForeignKey("PatientCardId");
                        });

                    b.OwnsOne("DoctorDiary.Models.PatientCards.ValueObjects.Snils", "Snils", b1 =>
                        {
                            b1.Property<Guid>("PatientCardId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .HasColumnType("NVARCHAR(11)")
                                .HasColumnName("Snils");

                            b1.HasKey("PatientCardId");

                            b1.ToTable("PatientCards");

                            b1.WithOwner()
                                .HasForeignKey("PatientCardId");
                        });

                    b.Navigation("Address");

                    b.Navigation("InsurancePolicy");

                    b.Navigation("PhoneNumber");

                    b.Navigation("Snils");
                });

            modelBuilder.Entity("DoctorDiary.Models.SickLeaves.SickLeave", b =>
                {
                    b.HasOne("DoctorDiary.Models.PatientCards.PatientCard", null)
                        .WithMany()
                        .HasForeignKey("PatientCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("DoctorDiary.Models.SickLeaves.ValueObjects.Term", "Terms", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("TEXT")
                                .HasColumnName("EndDate");

                            b1.Property<Guid>("SickLeaveId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("TEXT")
                                .HasColumnName("StartDate");

                            b1.HasKey("Id");

                            b1.HasIndex("SickLeaveId");

                            b1.ToTable("TermsOfSickLeaves");

                            b1.WithOwner()
                                .HasForeignKey("SickLeaveId");
                        });

                    b.Navigation("Terms");
                });

            modelBuilder.Entity("DoctorDiary.Models.Visits.Visit", b =>
                {
                    b.HasOne("DoctorDiary.Models.PatientCards.PatientCard", null)
                        .WithMany()
                        .HasForeignKey("PatientCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
