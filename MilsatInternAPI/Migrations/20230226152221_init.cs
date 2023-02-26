using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatInternAPI.Migrations
{
    public partial class init : Migration
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
                values: new object[] { new Guid("19418d07-a32a-4117-bf16-7d07a4c54120"), "", "mentor1@gmail.com", "Sodiq Agboola", 0, new byte[] { 5, 141, 21, 146, 108, 138, 135, 104, 150, 21, 151, 176, 247, 109, 1, 152, 104, 127, 203, 251, 194, 172, 213, 98, 127, 81, 130, 249, 99, 188, 244, 29, 157, 199, 37, 52, 7, 149, 135, 83, 39, 78, 84, 78, 254, 157, 8, 68, 100, 32, 170, 135, 185, 50, 173, 16, 73, 215, 73, 247, 96, 127, 115, 31 }, null, new byte[] { 160, 131, 46, 112, 145, 227, 85, 36, 130, 137, 216, 60, 234, 225, 18, 169, 0, 218, 241, 123, 143, 229, 127, 144, 58, 15, 73, 90, 35, 155, 21, 238, 1, 80, 251, 145, 84, 204, 208, 49, 8, 116, 142, 148, 246, 16, 59, 252, 210, 25, 231, 53, 188, 86, 73, 119, 20, 77, 217, 120, 104, 204, 39, 94, 228, 128, 134, 64, 42, 16, 49, 210, 203, 214, 49, 107, 112, 162, 72, 18, 132, 133, 15, 198, 226, 223, 34, 167, 15, 108, 38, 11, 160, 80, 252, 85, 108, 209, 98, 28, 55, 144, 233, 80, 92, 4, 83, 171, 251, 12, 5, 108, 153, 53, 151, 30, 254, 116, 255, 249, 24, 45, 87, 91, 240, 118, 71, 219 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("383a669d-2fc7-436d-8f35-3fb3ac8d2c7f"), "", "mentor2@gmail.com", "Sodiq Agboola", 0, new byte[] { 235, 204, 205, 93, 172, 89, 98, 45, 204, 35, 244, 113, 224, 190, 198, 50, 29, 4, 143, 7, 235, 112, 18, 177, 251, 45, 41, 120, 120, 157, 161, 40, 70, 91, 216, 80, 77, 193, 84, 124, 67, 25, 31, 35, 18, 130, 141, 248, 210, 86, 24, 240, 196, 252, 144, 125, 216, 215, 88, 190, 72, 147, 243, 188 }, null, new byte[] { 228, 22, 178, 128, 184, 59, 245, 13, 234, 70, 153, 166, 58, 217, 161, 104, 65, 242, 75, 100, 239, 142, 37, 247, 32, 215, 184, 204, 160, 186, 95, 95, 71, 24, 224, 154, 180, 173, 67, 255, 135, 171, 162, 49, 246, 33, 133, 90, 103, 69, 7, 119, 225, 176, 103, 233, 135, 27, 151, 145, 241, 254, 87, 23, 89, 133, 35, 179, 73, 135, 226, 89, 61, 148, 214, 224, 94, 142, 230, 235, 183, 209, 145, 160, 179, 14, 214, 147, 107, 144, 205, 131, 243, 19, 34, 79, 49, 219, 144, 57, 89, 44, 108, 64, 217, 100, 65, 143, 48, 101, 63, 80, 218, 108, 155, 206, 55, 61, 77, 239, 54, 55, 173, 254, 171, 215, 188, 57 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("6cc024da-1be9-4e07-b796-5aaa96b4852d"), "", "admin@milsat.com", "Admin", 0, new byte[] { 252, 190, 116, 107, 164, 134, 103, 201, 156, 203, 240, 191, 7, 248, 14, 229, 138, 237, 213, 180, 36, 8, 215, 8, 240, 108, 18, 130, 116, 206, 8, 248, 7, 54, 143, 104, 46, 170, 5, 255, 178, 197, 122, 68, 99, 181, 50, 102, 90, 198, 69, 177, 200, 246, 211, 111, 219, 165, 159, 115, 120, 154, 135, 175 }, null, new byte[] { 10, 196, 67, 55, 210, 71, 15, 240, 55, 154, 176, 94, 236, 125, 206, 73, 181, 113, 140, 200, 215, 24, 214, 224, 168, 204, 239, 122, 247, 221, 111, 30, 122, 164, 230, 143, 238, 78, 160, 94, 214, 9, 5, 63, 79, 245, 244, 27, 151, 101, 15, 3, 189, 212, 138, 77, 215, 131, 61, 172, 96, 149, 127, 27, 123, 155, 189, 52, 183, 155, 10, 234, 234, 59, 193, 138, 217, 31, 193, 51, 225, 174, 225, 80, 145, 62, 115, 161, 80, 68, 111, 208, 100, 29, 31, 112, 52, 153, 78, 128, 193, 156, 102, 196, 122, 209, 188, 222, 246, 204, 206, 170, 128, 175, 56, 85, 238, 100, 216, 197, 177, 167, 52, 25, 7, 112, 208, 56 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("19418d07-a32a-4117-bf16-7d07a4c54120"), new DateTime(2023, 2, 26, 15, 22, 21, 623, DateTimeKind.Utc).AddTicks(9813), 0 });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("383a669d-2fc7-436d-8f35-3fb3ac8d2c7f"), new DateTime(2023, 2, 26, 15, 22, 21, 623, DateTimeKind.Utc).AddTicks(9815), 0 });

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
