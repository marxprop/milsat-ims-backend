using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class report_del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf38d403-f470-4833-ba6e-d6c7f1a0cdc3"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("cc89cd27-0269-4141-87c3-afc58c422797"), "", "admin@milsat.com", "Admin", 0, new byte[] { 253, 188, 42, 40, 197, 77, 116, 249, 122, 77, 191, 52, 72, 142, 193, 43, 172, 254, 59, 31, 148, 141, 240, 133, 92, 194, 80, 205, 41, 213, 25, 54, 149, 48, 96, 86, 70, 93, 234, 7, 177, 183, 23, 208, 9, 193, 135, 66, 241, 201, 116, 48, 168, 77, 165, 197, 118, 7, 236, 121, 12, 221, 157, 127 }, null, new byte[] { 19, 3, 36, 248, 224, 106, 66, 216, 180, 90, 87, 25, 5, 13, 10, 112, 78, 163, 14, 223, 222, 193, 176, 142, 138, 254, 186, 18, 100, 251, 234, 27, 247, 93, 145, 111, 127, 25, 6, 184, 10, 200, 5, 42, 9, 55, 60, 25, 136, 126, 124, 45, 136, 75, 50, 158, 123, 92, 149, 100, 26, 90, 203, 236, 81, 26, 241, 140, 11, 210, 114, 91, 123, 176, 67, 103, 28, 15, 173, 48, 154, 87, 20, 225, 55, 132, 251, 116, 9, 187, 16, 159, 215, 95, 133, 244, 85, 114, 223, 245, 55, 61, 70, 14, 104, 136, 206, 239, 228, 59, 100, 126, 218, 54, 20, 99, 190, 248, 230, 133, 171, 31, 225, 202, 6, 242, 218, 239 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("cc89cd27-0269-4141-87c3-afc58c422797"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("bf38d403-f470-4833-ba6e-d6c7f1a0cdc3"), "", "admin@milsat.com", "Admin", 0, new byte[] { 61, 119, 76, 21, 84, 86, 63, 26, 35, 35, 200, 204, 73, 201, 121, 88, 3, 177, 161, 28, 40, 54, 253, 3, 198, 86, 253, 24, 126, 10, 4, 148, 88, 176, 211, 207, 247, 125, 92, 145, 25, 60, 90, 228, 75, 54, 53, 215, 167, 88, 48, 234, 167, 57, 65, 53, 187, 31, 11, 17, 83, 58, 8, 160 }, null, new byte[] { 185, 181, 236, 17, 82, 226, 24, 117, 110, 48, 141, 152, 52, 51, 174, 206, 105, 6, 15, 174, 25, 107, 163, 180, 220, 19, 45, 38, 6, 203, 228, 37, 156, 50, 241, 213, 138, 255, 57, 62, 204, 107, 93, 110, 24, 225, 24, 235, 224, 94, 238, 165, 182, 234, 201, 248, 247, 149, 201, 81, 243, 139, 47, 49, 110, 204, 193, 86, 42, 160, 197, 29, 205, 37, 227, 254, 124, 194, 164, 160, 153, 168, 53, 223, 14, 48, 200, 104, 34, 209, 48, 20, 136, 71, 216, 113, 43, 216, 160, 147, 131, 22, 224, 32, 179, 179, 176, 66, 178, 155, 59, 63, 215, 115, 218, 239, 74, 154, 156, 26, 152, 107, 219, 98, 10, 157, 5, 196 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
