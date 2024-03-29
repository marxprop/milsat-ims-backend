﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MilsatIMS.Data;

#nullable disable

namespace MilsatIMS.Migrations
{
    [DbContext(typeof(MilsatIMSContext))]
    partial class MilsatIMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("MilsatIMS.Models.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReportName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("SessionId")
                        .IsUnique();

                    b.ToTable("Report");
                });

            modelBuilder.Entity("MilsatIMS.Models.ReportFeedback", b =>
                {
                    b.Property<Guid>("ReportFeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("InternRating")
                        .HasColumnType("int");

                    b.Property<Guid>("MentorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ReportSubmissionId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("SubmitDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ReportFeedbackId");

                    b.HasIndex("MentorId");

                    b.HasIndex("ReportSubmissionId")
                        .IsUnique();

                    b.ToTable("ReportFeedback");
                });

            modelBuilder.Entity("MilsatIMS.Models.ReportSubmission", b =>
                {
                    b.Property<Guid>("ReportSubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("BlockerOrigin")
                        .HasColumnType("int");

                    b.Property<int>("BlockerType")
                        .HasColumnType("int");

                    b.Property<Guid>("InternId")
                        .HasColumnType("char(36)");

                    b.Property<int>("MentorRating")
                        .HasColumnType("int");

                    b.Property<string>("OtherTeams")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SubmitDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Task")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TaskDetails")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Timeline")
                        .HasColumnType("int");

                    b.HasKey("ReportSubmissionId");

                    b.HasIndex("InternId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportSubmission");
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
                            UserId = new Guid("0f92e48e-a0f3-4c87-bc63-1363bc577337"),
                            Bio = "",
                            Email = "admin@milsat.com",
                            FullName = "Admin",
                            Gender = 0,
                            PasswordHash = new byte[] { 44, 68, 20, 35, 203, 43, 96, 143, 225, 123, 251, 190, 135, 137, 249, 180, 245, 17, 183, 32, 59, 76, 225, 11, 202, 85, 110, 246, 149, 6, 116, 159, 255, 61, 204, 123, 67, 30, 174, 177, 170, 220, 82, 102, 199, 212, 222, 179, 58, 201, 94, 182, 255, 201, 37, 237, 107, 74, 210, 51, 68, 3, 130, 99 },
                            PasswordSalt = new byte[] { 1, 179, 98, 171, 242, 232, 32, 45, 111, 48, 204, 114, 71, 102, 195, 23, 203, 128, 52, 225, 193, 245, 47, 225, 102, 169, 81, 167, 46, 159, 146, 199, 208, 182, 188, 139, 204, 107, 1, 231, 57, 42, 190, 66, 127, 45, 158, 55, 229, 236, 219, 56, 197, 147, 134, 108, 131, 68, 43, 244, 0, 94, 172, 38, 95, 0, 136, 229, 214, 136, 21, 75, 130, 102, 250, 199, 229, 204, 243, 68, 216, 239, 254, 185, 166, 249, 6, 110, 140, 75, 188, 89, 235, 3, 183, 92, 87, 24, 99, 169, 238, 102, 139, 0, 53, 190, 198, 210, 234, 3, 175, 143, 63, 90, 52, 252, 100, 69, 171, 111, 145, 37, 53, 198, 199, 18, 84, 194 },
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

            modelBuilder.Entity("MilsatIMS.Models.Report", b =>
                {
                    b.HasOne("MilsatIMS.Models.Session", "Session")
                        .WithOne("Report")
                        .HasForeignKey("MilsatIMS.Models.Report", "SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("MilsatIMS.Models.ReportFeedback", b =>
                {
                    b.HasOne("MilsatIMS.Models.Mentor", "Mentor")
                        .WithMany("ReportFeedbacks")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilsatIMS.Models.ReportSubmission", "ReportSubmission")
                        .WithOne("ReportFeedback")
                        .HasForeignKey("MilsatIMS.Models.ReportFeedback", "ReportSubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");

                    b.Navigation("ReportSubmission");
                });

            modelBuilder.Entity("MilsatIMS.Models.ReportSubmission", b =>
                {
                    b.HasOne("MilsatIMS.Models.Intern", "Intern")
                        .WithMany("ReportSubmissions")
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilsatIMS.Models.Report", "Report")
                        .WithMany("Submissions")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Intern");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("MilsatIMS.Models.Intern", b =>
                {
                    b.Navigation("IMS");

                    b.Navigation("ReportSubmissions");
                });

            modelBuilder.Entity("MilsatIMS.Models.Mentor", b =>
                {
                    b.Navigation("IMS");

                    b.Navigation("ReportFeedbacks");
                });

            modelBuilder.Entity("MilsatIMS.Models.Report", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("MilsatIMS.Models.ReportSubmission", b =>
                {
                    b.Navigation("ReportFeedback");
                });

            modelBuilder.Entity("MilsatIMS.Models.Session", b =>
                {
                    b.Navigation("IMS");

                    b.Navigation("Report")
                        .IsRequired();
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
