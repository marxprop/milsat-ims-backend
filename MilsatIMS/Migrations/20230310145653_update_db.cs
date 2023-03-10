using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class update_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("32ea9d03-c649-4609-8ba6-64b2e4c00559"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("d15590cc-ff79-4e3a-a377-af8a71d8c6cd"), "", "admin@milsat.com", "Admin", 0, new byte[] { 47, 54, 34, 214, 23, 5, 145, 206, 181, 230, 78, 141, 236, 30, 186, 191, 173, 108, 145, 254, 66, 89, 110, 137, 34, 37, 147, 113, 54, 169, 206, 123, 15, 68, 132, 93, 94, 8, 69, 3, 70, 41, 90, 110, 159, 100, 231, 185, 114, 229, 151, 70, 116, 102, 189, 72, 125, 231, 128, 106, 51, 190, 197, 187 }, null, new byte[] { 78, 139, 14, 222, 37, 34, 66, 72, 142, 172, 237, 116, 97, 147, 125, 162, 208, 215, 231, 166, 109, 187, 110, 177, 59, 148, 179, 148, 246, 94, 12, 142, 55, 86, 32, 36, 20, 0, 79, 254, 234, 8, 127, 248, 201, 13, 226, 216, 9, 47, 219, 82, 147, 251, 105, 11, 122, 167, 50, 179, 137, 36, 8, 216, 63, 143, 238, 78, 195, 101, 108, 107, 9, 204, 232, 123, 32, 220, 225, 186, 208, 121, 228, 155, 193, 87, 191, 124, 210, 145, 138, 193, 184, 103, 233, 9, 254, 100, 69, 153, 20, 117, 91, 78, 52, 81, 78, 247, 144, 202, 144, 163, 112, 175, 134, 31, 49, 232, 65, 127, 177, 120, 248, 207, 163, 59, 162, 112 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d15590cc-ff79-4e3a-a377-af8a71d8c6cd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("32ea9d03-c649-4609-8ba6-64b2e4c00559"), "", "admin@milsat.com", "Admin", 0, new byte[] { 99, 123, 27, 25, 87, 146, 93, 172, 186, 36, 89, 186, 192, 103, 53, 248, 191, 235, 5, 112, 249, 164, 81, 190, 18, 143, 167, 192, 197, 4, 158, 242, 180, 146, 128, 145, 79, 30, 71, 249, 181, 86, 221, 67, 204, 221, 98, 4, 59, 42, 200, 169, 131, 151, 143, 189, 158, 35, 14, 89, 88, 40, 131, 196 }, null, new byte[] { 122, 159, 135, 172, 73, 44, 164, 116, 204, 230, 237, 114, 210, 71, 87, 128, 18, 231, 0, 57, 157, 179, 246, 224, 152, 198, 91, 30, 155, 229, 165, 57, 191, 241, 225, 182, 222, 106, 255, 90, 158, 27, 57, 231, 116, 26, 97, 241, 61, 135, 182, 158, 125, 30, 213, 63, 9, 123, 255, 153, 221, 244, 247, 49, 236, 245, 51, 32, 31, 141, 219, 238, 112, 25, 234, 98, 128, 91, 120, 47, 174, 242, 48, 54, 247, 174, 73, 227, 231, 21, 223, 112, 222, 24, 231, 220, 57, 52, 5, 7, 219, 231, 173, 31, 24, 27, 137, 189, 236, 75, 254, 242, 100, 231, 55, 22, 107, 232, 84, 250, 178, 28, 240, 128, 236, 238, 99, 106 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
