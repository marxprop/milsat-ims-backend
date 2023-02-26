﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatInternAPI.Migrations
{
    public partial class freshstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfilePicture = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordResetToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordTokenExpires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Mentor_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Intern",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CourseOfStudy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Institution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MentorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intern", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Intern_Mentor_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentor",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Intern_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("ba31200f-a92d-4ec1-acb5-beb397bd5718"), "", "mentor2@gmail.com", "Sodiq Agboola", 0, new byte[] { 219, 218, 3, 121, 52, 3, 168, 46, 190, 225, 116, 173, 17, 235, 160, 39, 10, 64, 74, 29, 35, 65, 81, 32, 175, 153, 71, 58, 26, 218, 174, 183, 7, 95, 77, 233, 193, 61, 200, 162, 31, 148, 118, 191, 130, 68, 3, 18, 233, 19, 92, 123, 247, 167, 248, 51, 196, 153, 117, 234, 252, 152, 234, 21 }, null, new byte[] { 62, 220, 131, 92, 96, 248, 182, 55, 186, 152, 84, 146, 223, 243, 227, 119, 227, 9, 128, 254, 4, 207, 117, 73, 82, 207, 197, 11, 20, 69, 253, 121, 48, 36, 207, 100, 97, 20, 150, 229, 247, 175, 137, 19, 80, 119, 11, 3, 139, 204, 212, 143, 29, 51, 138, 86, 107, 213, 188, 70, 68, 235, 20, 67, 232, 147, 179, 162, 32, 119, 65, 11, 0, 64, 213, 192, 65, 142, 0, 222, 60, 189, 235, 168, 55, 63, 107, 145, 121, 183, 115, 176, 25, 119, 242, 90, 222, 120, 154, 35, 229, 50, 42, 189, 130, 233, 183, 185, 198, 140, 243, 151, 205, 152, 95, 19, 163, 33, 184, 209, 114, 143, 10, 71, 170, 101, 115, 211 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("c7a4c6a3-54fb-4ccc-b474-e3e2da16ebcf"), "", "admin@milsat.com", "Admin", 0, new byte[] { 141, 207, 33, 90, 103, 57, 204, 84, 191, 223, 66, 142, 29, 142, 181, 67, 138, 42, 73, 68, 226, 169, 66, 0, 167, 14, 231, 147, 103, 247, 103, 188, 248, 111, 1, 28, 202, 22, 214, 19, 48, 26, 49, 221, 27, 50, 204, 235, 71, 88, 240, 246, 202, 104, 106, 135, 216, 213, 205, 167, 125, 207, 82, 129 }, null, new byte[] { 52, 209, 177, 165, 72, 80, 162, 76, 64, 237, 43, 163, 232, 95, 100, 151, 137, 222, 9, 142, 217, 91, 68, 187, 116, 93, 90, 201, 240, 53, 197, 123, 247, 154, 192, 140, 136, 215, 181, 35, 188, 62, 13, 157, 238, 253, 19, 33, 43, 27, 226, 207, 158, 113, 246, 99, 251, 97, 100, 232, 24, 210, 112, 147, 167, 220, 128, 219, 82, 244, 39, 188, 41, 118, 20, 166, 220, 214, 59, 42, 249, 198, 145, 69, 137, 92, 100, 72, 228, 205, 45, 59, 198, 95, 36, 52, 224, 235, 223, 164, 25, 132, 141, 189, 98, 134, 245, 254, 83, 130, 86, 166, 8, 235, 168, 168, 120, 62, 221, 135, 23, 64, 236, 238, 49, 49, 49, 230 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("d32d6639-6784-4259-9249-4a3507f2e8a1"), "", "mentor1@gmail.com", "Sodiq Agboola", 0, new byte[] { 244, 83, 77, 38, 228, 210, 52, 245, 233, 6, 184, 234, 250, 232, 162, 170, 84, 33, 120, 199, 216, 253, 131, 251, 42, 251, 6, 175, 103, 5, 144, 108, 94, 209, 243, 106, 187, 96, 89, 112, 8, 151, 252, 179, 239, 205, 114, 139, 217, 158, 107, 205, 33, 247, 196, 162, 84, 7, 215, 164, 200, 143, 122, 220 }, null, new byte[] { 228, 70, 252, 95, 165, 223, 61, 108, 220, 155, 84, 53, 109, 42, 175, 13, 163, 231, 162, 230, 109, 138, 198, 223, 234, 167, 93, 71, 218, 134, 19, 135, 246, 97, 29, 160, 221, 171, 77, 171, 12, 222, 117, 90, 159, 198, 23, 192, 252, 11, 182, 42, 198, 234, 96, 128, 138, 135, 98, 199, 126, 132, 59, 199, 249, 47, 53, 117, 167, 229, 134, 52, 179, 162, 155, 126, 45, 18, 145, 53, 155, 140, 55, 100, 169, 60, 241, 14, 42, 118, 16, 236, 99, 33, 147, 71, 216, 102, 5, 110, 210, 108, 131, 52, 182, 95, 58, 28, 75, 234, 33, 96, 5, 5, 157, 85, 231, 126, 10, 219, 63, 175, 244, 16, 241, 19, 77, 100 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("ba31200f-a92d-4ec1-acb5-beb397bd5718"), new DateTime(2023, 2, 26, 11, 4, 17, 813, DateTimeKind.Utc).AddTicks(4787), 0 });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("d32d6639-6784-4259-9249-4a3507f2e8a1"), new DateTime(2023, 2, 26, 11, 4, 17, 813, DateTimeKind.Utc).AddTicks(4784), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Intern_MentorId",
                table: "Intern",
                column: "MentorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intern");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
