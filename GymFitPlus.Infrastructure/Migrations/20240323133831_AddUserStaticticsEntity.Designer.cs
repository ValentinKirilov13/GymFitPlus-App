﻿// <auto-generated />
using System;
using GymFitPlus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240323133831_AddUserStaticticsEntity")]
    partial class AddUserStaticticsEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date")
                        .HasComment("User birth date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacebookUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Link to user Facebook account");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("User first name");

                    b.Property<int?>("Gender")
                        .HasColumnType("int")
                        .HasComment("User gender");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)")
                        .HasComment("User profile image");

                    b.Property<string>("InstagramUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Link to user Instagram account");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("User last name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("YouTubeUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Link to user YouTube account");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasComment("Table with registered application users");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Exercise identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasComment("Exercise description");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasComment("Exercise status");

                    b.Property<int>("MuscleGroup")
                        .HasColumnType("int")
                        .HasComment("Exercise muscle group target");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Exercise name");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Exercise video link");

                    b.HasKey("Id");

                    b.ToTable("Exercises");

                    b.HasComment("Table of Exercise");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.FitnessProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Fitness program identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasComment("Exercise status");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Fitness program name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Fitness program creator/owner");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FitnessPrograms");

                    b.HasComment("Table with fitness programs");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.FitnessProgramExercise", b =>
                {
                    b.Property<int>("FitnessProgramId")
                        .HasColumnType("int")
                        .HasComment("Fitness program identifier");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int")
                        .HasComment("Exercise identifier");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasComment("Fitness program order");

                    b.Property<int>("Reps")
                        .HasColumnType("int")
                        .HasComment("Reps for the exercise");

                    b.Property<int>("Sets")
                        .HasColumnType("int")
                        .HasComment("Sets for the exercise");

                    b.Property<double>("Weight")
                        .HasColumnType("float")
                        .HasComment("Weight for the exercise");

                    b.HasKey("FitnessProgramId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("FitnessProgramsExercises");

                    b.HasComment("Table of Exercise in one fitness program");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.UserSatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Statistics identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("BackCircumference")
                        .HasColumnType("float")
                        .HasComment("Back circumference of user in centimeters");

                    b.Property<double>("ChestCircumference")
                        .HasColumnType("float")
                        .HasComment("Chest circumference of user in centimeters");

                    b.Property<DateTime>("DateOfМeasurements")
                        .HasColumnType("date")
                        .HasComment("Date Of Мeasurements");

                    b.Property<double>("GluteusCircumference")
                        .HasColumnType("float")
                        .HasComment("Gluteus circumference of user in centimeters");

                    b.Property<double>("Height")
                        .HasColumnType("float")
                        .HasComment("Height of user in meters");

                    b.Property<double>("LeftArmCircumference")
                        .HasColumnType("float")
                        .HasComment("Left arm circumference of user in centimeters");

                    b.Property<double>("LeftCalfCircumference")
                        .HasColumnType("float")
                        .HasComment("Left calf circumference of user in centimeters");

                    b.Property<double>("LeftLegCircumference")
                        .HasColumnType("float")
                        .HasComment("Left leg circumference of user in centimeters");

                    b.Property<double>("RightArmCircumference")
                        .HasColumnType("float")
                        .HasComment("Right arm circumference of user in centimeters");

                    b.Property<double>("RightCalfCircumference")
                        .HasColumnType("float")
                        .HasComment("Right calf circumference of user in centimeters");

                    b.Property<double>("RightLegCircumference")
                        .HasColumnType("float")
                        .HasComment("Right leg circumference of user in centimeters");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Statistics owner");

                    b.Property<double>("WaistCircumference")
                        .HasColumnType("float")
                        .HasComment("Waist circumference of user in centimeters");

                    b.Property<double>("Weight")
                        .HasColumnType("float")
                        .HasComment("Weight of user in kilograms");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSatistics");

                    b.HasComment("Table with statistics of users");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Workout identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasComment("Date of workout");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasComment("Duration of workout in minutes");

                    b.Property<int>("FitnessProgramId")
                        .HasColumnType("int")
                        .HasComment("Fitness program that is used in workout");

                    b.Property<string>("Note")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("User note on workout");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Workout owner");

                    b.HasKey("Id");

                    b.HasIndex("FitnessProgramId");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");

                    b.HasComment("Table with users workouts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.FitnessProgram", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.FitnessProgramExercise", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.Exercise", "Exercise")
                        .WithMany("FitnessProgramsExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.FitnessProgram", "FitnessProgram")
                        .WithMany("FitnessProgramsExercises")
                        .HasForeignKey("FitnessProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("FitnessProgram");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.UserSatistics", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany("UserSatistics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.Workout", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.FitnessProgram", "FitnessProgram")
                        .WithMany("Workouts")
                        .HasForeignKey("FitnessProgramId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FitnessProgram");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserSatistics");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.Exercise", b =>
                {
                    b.Navigation("FitnessProgramsExercises");
                });

            modelBuilder.Entity("GymFitPlus.Infrastructure.Data.Models.FitnessProgram", b =>
                {
                    b.Navigation("FitnessProgramsExercises");

                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
