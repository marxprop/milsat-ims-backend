using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class user_session : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ddfc72bd-261f-461f-a25d-2f02c757473a"));

            migrationBuilder.CreateTable(
                name: "SessionUser",
                columns: table => new
                {
                    SessionsSessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsersUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionUser", x => new { x.SessionsSessionId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_SessionUser_Session_SessionsSessionId",
                        column: x => x.SessionsSessionId,
                        principalTable: "Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("15597cc2-84c0-4b0b-a9bc-5d5242520e24"), "", "admin@milsat.com", "Admin", 0, new byte[] { 162, 5, 255, 87, 3, 111, 37, 75, 217, 216, 39, 139, 186, 210, 245, 85, 72, 122, 3, 249, 189, 147, 59, 99, 185, 174, 12, 36, 106, 139, 85, 185, 34, 249, 64, 227, 4, 222, 238, 219, 253, 23, 197, 203, 27, 127, 81, 169, 7, 237, 213, 138, 44, 224, 150, 76, 53, 139, 133, 37, 30, 87, 155, 210 }, null, new byte[] { 158, 148, 110, 50, 67, 152, 54, 120, 143, 61, 131, 54, 126, 189, 77, 204, 191, 36, 31, 249, 90, 74, 44, 127, 243, 12, 226, 218, 189, 138, 249, 18, 211, 238, 176, 123, 122, 23, 184, 28, 109, 29, 126, 224, 178, 30, 25, 165, 53, 162, 64, 148, 13, 92, 121, 75, 23, 111, 86, 99, 130, 136, 17, 47, 48, 244, 22, 167, 110, 161, 116, 135, 220, 183, 51, 55, 109, 217, 195, 182, 135, 85, 204, 67, 207, 38, 217, 212, 116, 232, 44, 119, 75, 232, 42, 219, 247, 187, 56, 29, 209, 92, 83, 214, 187, 29, 2, 112, 76, 81, 130, 221, 60, 0, 193, 126, 131, 3, 45, 121, 190, 51, 165, 243, 188, 175, 70, 40 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.CreateIndex(
                name: "IX_SessionUser_UsersUserId",
                table: "SessionUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionUser");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("15597cc2-84c0-4b0b-a9bc-5d5242520e24"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("ddfc72bd-261f-461f-a25d-2f02c757473a"), "", "admin@milsat.com", "Admin", 0, new byte[] { 72, 240, 253, 96, 48, 164, 57, 167, 116, 63, 44, 80, 200, 86, 77, 78, 211, 8, 78, 245, 139, 134, 188, 201, 55, 81, 118, 36, 170, 38, 55, 164, 188, 56, 241, 182, 219, 91, 212, 232, 230, 147, 8, 1, 176, 50, 132, 139, 106, 102, 20, 105, 77, 83, 92, 117, 174, 221, 129, 93, 190, 31, 28, 202 }, null, new byte[] { 29, 196, 104, 77, 81, 163, 186, 96, 154, 233, 75, 68, 142, 0, 122, 217, 67, 238, 115, 201, 164, 39, 137, 238, 122, 43, 101, 23, 57, 114, 83, 81, 33, 43, 186, 88, 0, 119, 130, 181, 20, 61, 161, 138, 84, 190, 147, 76, 58, 108, 140, 162, 238, 178, 90, 133, 31, 33, 186, 250, 205, 164, 191, 243, 70, 20, 248, 106, 174, 110, 63, 59, 67, 100, 218, 64, 12, 136, 108, 58, 205, 64, 203, 109, 20, 146, 129, 213, 34, 85, 109, 5, 69, 217, 127, 186, 75, 181, 8, 21, 247, 183, 29, 202, 175, 147, 183, 211, 121, 151, 86, 120, 72, 85, 52, 140, 59, 200, 81, 132, 165, 254, 136, 58, 46, 233, 172, 22 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
