using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class session : Migration
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
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
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
                name: "Intern",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CourseOfStudy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Institution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intern", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Intern_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InternId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MentorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IMS_Intern_InternId",
                        column: x => x.InternId,
                        principalTable: "Intern",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IMS_Mentor_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IMS_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("9141374b-01d1-4afe-b160-2326b454675c"), "", "admin@milsat.com", "Admin", 0, new byte[] { 206, 143, 93, 203, 168, 89, 67, 199, 20, 176, 166, 78, 89, 239, 252, 47, 182, 125, 32, 47, 235, 170, 196, 14, 201, 106, 243, 92, 28, 11, 111, 126, 227, 15, 238, 104, 41, 83, 52, 63, 3, 80, 107, 251, 230, 94, 171, 230, 138, 239, 88, 49, 193, 104, 148, 165, 39, 162, 130, 134, 144, 91, 209, 201 }, null, new byte[] { 253, 130, 92, 112, 234, 82, 201, 128, 220, 216, 198, 115, 242, 67, 81, 99, 84, 140, 22, 142, 5, 221, 46, 140, 176, 143, 0, 73, 125, 192, 79, 177, 195, 11, 76, 4, 206, 117, 235, 60, 144, 225, 5, 16, 141, 101, 159, 162, 85, 100, 186, 215, 28, 36, 82, 189, 95, 252, 193, 101, 217, 125, 214, 165, 158, 246, 156, 0, 26, 5, 144, 62, 45, 177, 234, 73, 223, 105, 248, 243, 54, 43, 159, 230, 252, 157, 17, 59, 64, 151, 106, 67, 42, 136, 149, 234, 16, 7, 125, 47, 125, 229, 188, 11, 60, 33, 126, 44, 63, 246, 101, 239, 24, 3, 255, 85, 131, 53, 195, 78, 80, 153, 223, 139, 121, 146, 16, 132 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.CreateIndex(
                name: "IX_IMS_InternId",
                table: "IMS",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_IMS_MentorId",
                table: "IMS",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_IMS_SessionId",
                table: "IMS",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IMS");

            migrationBuilder.DropTable(
                name: "Prompt");

            migrationBuilder.DropTable(
                name: "Intern");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
