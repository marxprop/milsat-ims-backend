﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MilsatIMS.Data;

#nullable disable

namespace MilsatIMS.Migrations
{
    [DbContext(typeof(MilsatIMSContext))]
    [Migration("20230308222855_start")]
    partial class start
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MilsatIMS.Models.Intern", b =>
                {
                    b.Property<Guid>("InternId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CourseOfStudy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("InternId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Intern");
                });

            modelBuilder.Entity("MilsatIMS.Models.InternMentorSession", b =>
                {
                    b.Property<Guid>("IMSId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("InternId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("MentorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("char(36)");

                    b.HasKey("IMSId");

                    b.HasIndex("InternId");

                    b.HasIndex("MentorId");

                    b.HasIndex("SessionId");

                    b.ToTable("IMS");
                });

            modelBuilder.Entity("MilsatIMS.Models.Mentor", b =>
                {
                    b.Property<Guid>("MentorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("MentorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Mentor");
                });

            modelBuilder.Entity("MilsatIMS.Models.Prompt", b =>
                {
                    b.Property<Guid>("PromptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PromptId");

                    b.ToTable("Prompt");
                });

            modelBuilder.Entity("MilsatIMS.Models.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("MilsatIMS.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<DateTime>("PasswordTokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("daeec15f-190e-4506-a08b-58fb1ff49989"),
                            Bio = "",
                            Email = "admin@milsat.com",
                            FullName = "Admin",
                            Gender = 0,
                            PasswordHash = new byte[] { 2, 148, 255, 110, 201, 179, 164, 42, 77, 140, 192, 161, 179, 181, 101, 94, 45, 212, 219, 92, 65, 146, 50, 229, 63, 22, 242, 234, 47, 89, 218, 246, 233, 92, 31, 171, 178, 173, 121, 38, 233, 222, 6, 84, 234, 174, 39, 226, 172, 86, 14, 88, 219, 134, 152, 24, 154, 132, 127, 149, 56, 26, 218, 64 },
                            PasswordSalt = new byte[] { 112, 63, 197, 224, 32, 79, 112, 241, 125, 38, 96, 22, 240, 38, 76, 159, 74, 186, 225, 125, 112, 77, 108, 236, 255, 242, 30, 158, 209, 88, 31, 158, 161, 63, 193, 15, 112, 26, 12, 250, 26, 157, 188, 93, 83, 139, 21, 151, 79, 36, 11, 207, 229, 106, 142, 78, 115, 157, 101, 168, 54, 119, 51, 4, 27, 13, 23, 107, 15, 106, 173, 66, 18, 5, 100, 5, 166, 41, 233, 91, 7, 62, 14, 120, 224, 11, 15, 238, 159, 28, 48, 181, 250, 216, 230, 123, 196, 57, 111, 27, 214, 9, 144, 192, 157, 76, 195, 247, 122, 29, 64, 126, 43, 247, 97, 124, 219, 12, 75, 213, 157, 246, 2, 195, 49, 89, 225, 221 },
                            PasswordTokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "datasolutions",
                            ProfilePicture = "",
                            Role = 0,
                            Team = 5,
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("MilsatIMS.Models.Intern", b =>
                {
                    b.HasOne("MilsatIMS.Models.User", "User")
                        .WithOne("Intern")
                        .HasForeignKey("MilsatIMS.Models.Intern", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MilsatIMS.Models.InternMentorSession", b =>
                {
                    b.HasOne("MilsatIMS.Models.Intern", "Intern")
                        .WithMany("IMS")
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilsatIMS.Models.Mentor", "Mentor")
                        .WithMany("IMS")
                        .HasForeignKey("MentorId");

                    b.HasOne("MilsatIMS.Models.Session", "Session")
                        .WithMany("IMS")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Intern");

                    b.Navigation("Mentor");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("MilsatIMS.Models.Mentor", b =>
                {
                    b.HasOne("MilsatIMS.Models.User", "User")
                        .WithOne("Mentor")
                        .HasForeignKey("MilsatIMS.Models.Mentor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MilsatIMS.Models.Intern", b =>
                {
                    b.Navigation("IMS");
                });

            modelBuilder.Entity("MilsatIMS.Models.Mentor", b =>
                {
                    b.Navigation("IMS");
                });

            modelBuilder.Entity("MilsatIMS.Models.Session", b =>
                {
                    b.Navigation("IMS");
                });

            modelBuilder.Entity("MilsatIMS.Models.User", b =>
                {
                    b.Navigation("Intern")
                        .IsRequired();

                    b.Navigation("Mentor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}