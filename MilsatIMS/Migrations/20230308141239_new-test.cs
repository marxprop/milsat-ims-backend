using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class newtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mentor",
                keyColumn: "UserId",
                keyValue: new Guid("6254a0b2-1500-4125-868b-95c67cfdeaec"));

            migrationBuilder.DeleteData(
                table: "Mentor",
                keyColumn: "UserId",
                keyValue: new Guid("ba1cb60b-d669-4db1-a53f-ade2f269e822"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("4e8ab9c6-81ea-4f60-9e5b-bb801aeedb1e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6254a0b2-1500-4125-868b-95c67cfdeaec"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ba1cb60b-d669-4db1-a53f-ade2f269e822"));

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InternUserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    MentorUserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Session_Intern_InternUserId",
                        column: x => x.InternUserId,
                        principalTable: "Intern",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Session_Mentor_MentorUserId",
                        column: x => x.MentorUserId,
                        principalTable: "Mentor",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("6d51f3f8-a5da-495d-a162-1a6b8f349323"), "", "admin@milsat.com", "Admin", 0, new byte[] { 109, 136, 42, 214, 81, 2, 115, 118, 195, 221, 212, 126, 49, 190, 49, 169, 168, 154, 24, 204, 140, 1, 232, 159, 197, 224, 202, 148, 227, 133, 197, 50, 229, 242, 62, 145, 58, 38, 12, 160, 238, 94, 253, 30, 205, 174, 211, 190, 16, 119, 206, 109, 182, 131, 208, 152, 41, 123, 190, 196, 46, 206, 58, 80 }, null, new byte[] { 182, 105, 64, 187, 165, 125, 118, 25, 168, 78, 45, 158, 70, 239, 248, 9, 182, 218, 71, 38, 37, 104, 151, 51, 6, 125, 85, 224, 229, 55, 107, 254, 0, 111, 194, 104, 91, 67, 8, 31, 159, 255, 217, 181, 83, 42, 24, 14, 140, 28, 112, 243, 15, 235, 83, 94, 19, 101, 205, 125, 62, 67, 17, 103, 251, 74, 99, 100, 215, 155, 203, 115, 83, 89, 84, 85, 25, 57, 216, 88, 108, 47, 97, 194, 98, 19, 48, 26, 192, 223, 63, 44, 61, 11, 226, 160, 178, 237, 139, 91, 118, 143, 76, 109, 214, 134, 165, 148, 6, 102, 104, 254, 255, 21, 146, 40, 188, 180, 228, 41, 150, 41, 185, 218, 162, 70, 168, 226 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("c1a8e789-dc5c-486d-be22-b6f07a891aa8"), "", "mentor2@gmail.com", "Sodiq Agboola", 0, new byte[] { 190, 207, 121, 209, 134, 163, 90, 254, 220, 109, 210, 255, 178, 197, 105, 192, 184, 118, 148, 75, 137, 122, 215, 190, 212, 96, 240, 236, 125, 212, 174, 17, 214, 75, 32, 245, 3, 50, 10, 141, 77, 174, 71, 85, 211, 214, 174, 163, 32, 89, 208, 101, 201, 56, 139, 67, 0, 118, 21, 149, 131, 176, 220, 62 }, null, new byte[] { 151, 180, 189, 27, 50, 50, 245, 76, 186, 198, 59, 144, 95, 189, 172, 167, 5, 226, 32, 216, 57, 96, 107, 158, 247, 137, 80, 229, 160, 126, 146, 80, 124, 35, 7, 162, 229, 162, 233, 178, 251, 2, 240, 26, 93, 45, 106, 24, 222, 92, 125, 3, 190, 54, 74, 48, 47, 212, 209, 54, 246, 237, 0, 230, 28, 190, 233, 144, 46, 57, 246, 17, 208, 209, 51, 175, 68, 118, 8, 53, 16, 69, 171, 4, 174, 91, 58, 230, 237, 175, 148, 153, 162, 63, 232, 207, 227, 206, 5, 168, 66, 224, 33, 128, 140, 110, 231, 238, 170, 124, 227, 193, 250, 89, 59, 51, 239, 171, 49, 115, 205, 221, 245, 52, 43, 138, 152, 80 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("c6f2bfb6-ac19-49a9-85b2-a5b0f41b7b27"), "", "mentor1@gmail.com", "Sodiq Agboola", 0, new byte[] { 139, 149, 192, 87, 170, 146, 21, 248, 172, 207, 158, 195, 205, 66, 105, 22, 242, 250, 30, 24, 230, 57, 84, 26, 42, 166, 90, 80, 249, 81, 183, 160, 214, 165, 243, 61, 133, 30, 233, 173, 61, 248, 187, 68, 244, 17, 250, 73, 26, 240, 254, 5, 128, 16, 235, 123, 171, 94, 57, 15, 181, 51, 55, 255 }, null, new byte[] { 254, 236, 129, 164, 193, 135, 62, 29, 242, 174, 167, 32, 234, 125, 200, 202, 127, 199, 4, 17, 106, 220, 229, 3, 98, 60, 41, 66, 122, 204, 128, 252, 237, 141, 230, 194, 64, 148, 83, 60, 74, 143, 39, 113, 134, 134, 145, 253, 54, 197, 6, 217, 105, 226, 43, 143, 122, 39, 89, 47, 114, 104, 203, 105, 36, 104, 204, 173, 67, 237, 25, 17, 147, 96, 165, 49, 156, 254, 146, 220, 221, 70, 114, 12, 31, 103, 233, 180, 125, 107, 233, 105, 196, 160, 251, 78, 57, 54, 99, 220, 205, 143, 120, 36, 115, 54, 137, 179, 13, 62, 131, 218, 8, 110, 136, 233, 2, 60, 220, 30, 182, 92, 191, 195, 50, 185, 107, 122 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "passwords", "", null, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("c1a8e789-dc5c-486d-be22-b6f07a891aa8"), new DateTime(2023, 3, 8, 14, 12, 39, 503, DateTimeKind.Utc).AddTicks(7910), 0 });

            migrationBuilder.InsertData(
                table: "Mentor",
                columns: new[] { "UserId", "CreatedOn", "Status" },
                values: new object[] { new Guid("c6f2bfb6-ac19-49a9-85b2-a5b0f41b7b27"), new DateTime(2023, 3, 8, 14, 12, 39, 503, DateTimeKind.Utc).AddTicks(7906), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Session_InternUserId",
                table: "Session",
                column: "InternUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_MentorUserId",
                table: "Session",
                column: "MentorUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DeleteData(
                table: "Mentor",
                keyColumn: "UserId",
                keyValue: new Guid("c1a8e789-dc5c-486d-be22-b6f07a891aa8"));

            migrationBuilder.DeleteData(
                table: "Mentor",
                keyColumn: "UserId",
                keyValue: new Guid("c6f2bfb6-ac19-49a9-85b2-a5b0f41b7b27"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6d51f3f8-a5da-495d-a162-1a6b8f349323"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c1a8e789-dc5c-486d-be22-b6f07a891aa8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c6f2bfb6-ac19-49a9-85b2-a5b0f41b7b27"));

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
        }
    }
}
