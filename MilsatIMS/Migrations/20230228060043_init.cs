using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prompt",
                columns: table => new
                {
                    PromptId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Info = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.PromptId);
                })
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
                values: new object[] { new Guid("4e8ab9c6-81ea-4f60-9e5b-bb801aeedb1e"), "", "admin@milsat.com", "Admin", 0, new byte[] { 164, 64, 63, 191, 180, 87, 143, 72, 137, 218, 118, 232, 243, 249, 230, 161, 43, 120, 196, 216, 79, 165, 236, 239, 197, 228, 20, 109, 225, 148, 56, 92, 15, 94, 235, 242, 44, 246, 29, 33, 124, 32, 5, 63, 67, 17, 199, 18, 167, 230, 143, 140, 232, 249, 255, 157, 231, 225, 54, 176, 100, 184, 239, 142 }, null, new byte[] { 195, 147, 82, 96, 220, 251, 148, 19, 113, 220, 65, 165, 131, 183, 242, 107, 0, 83, 196, 22, 59, 54, 16, 48, 147, 152, 148, 192, 152, 153, 229, 30, 108, 2, 13, 11, 72, 197, 191, 98, 48, 76, 183, 228, 198, 64, 185, 215, 163, 157, 241, 104, 178, 33, 104, 202, 87, 229, 177, 97, 160, 119, 180, 112, 225, 89, 254, 166, 137, 124, 250, 37, 13, 210, 31, 230, 83, 157, 146, 108, 45, 151, 119, 204, 198, 5, 178, 94, 96, 188, 98, 254, 220, 51, 128, 130, 68, 176, 215, 0, 49, 114, 99, 106, 145, 167, 141, 141, 247, 140, 182, 198, 21, 149, 73, 223, 129, 200, 22, 252, 29, 101, 200, 231, 61, 153, 90, 161 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("6254a0b2-1500-4125-868b-95c67cfdeaec"), "", "mentor1@gmail.com", "Sodiq Agboola", 0, new byte[] { 123, 139, 59, 150, 127, 15, 193, 91, 10, 19, 155, 193, 156, 252, 224, 193, 211, 168, 218, 18, 118, 44, 188, 138, 149, 250, 48, 148, 152, 27, 85, 87, 26, 15, 19, 49, 27, 127, 113, 181, 197, 205, 101, 42, 110, 91, 247, 32, 175, 121, 47, 77, 101, 237, 170, 118, 69, 43, 55, 74, 54, 34, 192, 12 }, null, new byte[] { 176, 118, 40, 75, 32, 24, 133, 123, 37, 179, 164, 27, 55, 241, 234, 233, 156, 249, 183, 60, 225, 37, 169, 115, 49, 217, 32, 254, 98, 16, 153, 44, 132, 99, 114, 229, 152, 208, 188, 27, 233, 159, 6, 151, 190, 150, 29, 58, 202, 238, 210, 145, 76, 137, 105, 107, 177, 87, 39, 161, 195, 48, 7, 243, 81, 29, 128, 144, 64, 237, 202, 240, 76, 162, 135, 78, 249, 165, 209, 224, 209, 233, 106, 13, 9, 230, 26, 255, 52, 211, 136, 84, 28, 244, 67, 114, 20, 198, 247, 198, 154, 64, 252, 118, 187, 240, 49, 23, 45, 19, 5, 139, 120, 76, 231, 197, 1, 95, 64, 98, 107, 5, 171, 219, 124, 134, 190, 239 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("ba1cb60b-d669-4db1-a53f-ade2f269e822"), "", "mentor2@gmail.com", "Sodiq Agboola", 0, new byte[] { 8, 255, 217, 198, 183, 152, 217, 255, 39, 125, 78, 170, 242, 125, 72, 121, 66, 23, 198, 93, 195, 145, 9, 25, 143, 130, 96, 4, 247, 203, 92, 154, 133, 220, 143, 195, 113, 246, 7, 5, 159, 215, 42, 196, 48, 203, 177, 149, 229, 29, 121, 42, 12, 10, 111, 29, 15, 92, 107, 102, 246, 79, 45, 254 }, null, new byte[] { 138, 135, 155, 5, 216, 181, 74, 212, 104, 89, 35, 67, 189, 196, 75, 140, 208, 75, 145, 28, 216, 14, 164, 191, 105, 120, 236, 224, 85, 127, 176, 208, 162, 211, 30, 63, 58, 15, 205, 19, 108, 139, 162, 112, 1, 9, 89, 147, 111, 108, 76, 223, 126, 161, 92, 43, 46, 130, 245, 245, 21, 46, 51, 18, 199, 180, 207, 118, 125, 176, 22, 111, 103, 82, 128, 87, 248, 58, 122, 67, 40, 157, 163, 105, 69, 232, 11, 138, 85, 82, 145, 236, 25, 35, 233, 23, 18, 162, 37, 237, 25, 36, 79, 90, 63, 208, 108, 214, 84, 188, 235, 25, 18, 63, 164, 117, 9, 7, 1, 180, 197, 180, 1, 109, 194, 160, 224, 90 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("6254a0b2-1500-4125-868b-95c67cfdeaec"), new DateTime(2023, 2, 28, 6, 0, 42, 922, DateTimeKind.Utc).AddTicks(7534), 0 });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("ba1cb60b-d669-4db1-a53f-ade2f269e822"), new DateTime(2023, 2, 28, 6, 0, 42, 922, DateTimeKind.Utc).AddTicks(7537), 0 });

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
                name: "Prompt");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
