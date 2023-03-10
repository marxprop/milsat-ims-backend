using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class update_db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d15590cc-ff79-4e3a-a377-af8a71d8c6cd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("54677a17-2d92-475e-89e8-6f1f8f3da2d8"), "", "admin@milsat.com", "Admin", 0, new byte[] { 96, 119, 122, 158, 72, 26, 254, 236, 59, 148, 50, 102, 66, 117, 137, 156, 206, 147, 99, 156, 148, 109, 175, 57, 120, 245, 217, 0, 150, 141, 94, 4, 191, 222, 60, 166, 200, 18, 199, 203, 126, 167, 200, 84, 8, 237, 38, 88, 137, 158, 60, 156, 198, 46, 223, 217, 214, 124, 86, 34, 102, 80, 23, 81 }, null, new byte[] { 156, 129, 169, 48, 215, 54, 135, 50, 245, 45, 80, 216, 38, 15, 14, 241, 146, 97, 184, 123, 222, 178, 137, 192, 208, 107, 180, 222, 95, 129, 101, 63, 72, 62, 231, 20, 251, 115, 40, 107, 148, 243, 122, 200, 193, 52, 95, 171, 125, 78, 134, 62, 147, 211, 85, 80, 23, 32, 34, 219, 143, 32, 45, 241, 150, 173, 73, 197, 132, 62, 203, 21, 191, 101, 223, 5, 212, 246, 20, 111, 113, 124, 90, 69, 39, 128, 21, 237, 98, 161, 225, 142, 149, 81, 53, 9, 193, 174, 54, 16, 49, 221, 99, 18, 180, 68, 136, 21, 175, 249, 58, 8, 13, 122, 150, 227, 150, 157, 245, 245, 234, 230, 231, 141, 198, 185, 23, 29 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("54677a17-2d92-475e-89e8-6f1f8f3da2d8"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("d15590cc-ff79-4e3a-a377-af8a71d8c6cd"), "", "admin@milsat.com", "Admin", 0, new byte[] { 47, 54, 34, 214, 23, 5, 145, 206, 181, 230, 78, 141, 236, 30, 186, 191, 173, 108, 145, 254, 66, 89, 110, 137, 34, 37, 147, 113, 54, 169, 206, 123, 15, 68, 132, 93, 94, 8, 69, 3, 70, 41, 90, 110, 159, 100, 231, 185, 114, 229, 151, 70, 116, 102, 189, 72, 125, 231, 128, 106, 51, 190, 197, 187 }, null, new byte[] { 78, 139, 14, 222, 37, 34, 66, 72, 142, 172, 237, 116, 97, 147, 125, 162, 208, 215, 231, 166, 109, 187, 110, 177, 59, 148, 179, 148, 246, 94, 12, 142, 55, 86, 32, 36, 20, 0, 79, 254, 234, 8, 127, 248, 201, 13, 226, 216, 9, 47, 219, 82, 147, 251, 105, 11, 122, 167, 50, 179, 137, 36, 8, 216, 63, 143, 238, 78, 195, 101, 108, 107, 9, 204, 232, 123, 32, 220, 225, 186, 208, 121, 228, 155, 193, 87, 191, 124, 210, 145, 138, 193, 184, 103, 233, 9, 254, 100, 69, 153, 20, 117, 91, 78, 52, 81, 78, 247, 144, 202, 144, 163, 112, 175, 134, 31, 49, 232, 65, 127, 177, 120, 248, 207, 163, 59, 162, 112 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
