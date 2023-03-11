using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilsatIMS.Migrations
{
    public partial class report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("54677a17-2d92-475e-89e8-6f1f8f3da2d8"));

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportSubmission",
                columns: table => new
                {
                    ReportSubmissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InternId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BlockerType = table.Column<int>(type: "int", nullable: false),
                    BlockerOrigin = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskDetails = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timeline = table.Column<int>(type: "int", nullable: false),
                    OtherTeams = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmitDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MentorRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSubmission", x => x.ReportSubmissionId);
                    table.ForeignKey(
                        name: "FK_ReportSubmission_Intern_InternId",
                        column: x => x.InternId,
                        principalTable: "Intern",
                        principalColumn: "InternId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportSubmission_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportFeedback",
                columns: table => new
                {
                    ReportFeedbackId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InternRating = table.Column<int>(type: "int", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReportSubmissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MentorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFeedback", x => x.ReportFeedbackId);
                    table.ForeignKey(
                        name: "FK_ReportFeedback_Mentor_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentor",
                        principalColumn: "MentorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportFeedback_ReportSubmission_ReportSubmissionId",
                        column: x => x.ReportSubmissionId,
                        principalTable: "ReportSubmission",
                        principalColumn: "ReportSubmissionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("bf38d403-f470-4833-ba6e-d6c7f1a0cdc3"), "", "admin@milsat.com", "Admin", 0, new byte[] { 61, 119, 76, 21, 84, 86, 63, 26, 35, 35, 200, 204, 73, 201, 121, 88, 3, 177, 161, 28, 40, 54, 253, 3, 198, 86, 253, 24, 126, 10, 4, 148, 88, 176, 211, 207, 247, 125, 92, 145, 25, 60, 90, 228, 75, 54, 53, 215, 167, 88, 48, 234, 167, 57, 65, 53, 187, 31, 11, 17, 83, 58, 8, 160 }, null, new byte[] { 185, 181, 236, 17, 82, 226, 24, 117, 110, 48, 141, 152, 52, 51, 174, 206, 105, 6, 15, 174, 25, 107, 163, 180, 220, 19, 45, 38, 6, 203, 228, 37, 156, 50, 241, 213, 138, 255, 57, 62, 204, 107, 93, 110, 24, 225, 24, 235, 224, 94, 238, 165, 182, 234, 201, 248, 247, 149, 201, 81, 243, 139, 47, 49, 110, 204, 193, 86, 42, 160, 197, 29, 205, 37, 227, 254, 124, 194, 164, 160, 153, 168, 53, 223, 14, 48, 200, 104, 34, 209, 48, 20, 136, 71, 216, 113, 43, 216, 160, 147, 131, 22, 224, 32, 179, 179, 176, 66, 178, 155, 59, 63, 215, 115, 218, 239, 74, 154, 156, 26, 152, 107, 219, 98, 10, 157, 5, 196 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.CreateIndex(
                name: "IX_Report_SessionId",
                table: "Report",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedback_MentorId",
                table: "ReportFeedback",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedback_ReportSubmissionId",
                table: "ReportFeedback",
                column: "ReportSubmissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportSubmission_InternId",
                table: "ReportSubmission",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSubmission_ReportId",
                table: "ReportSubmission",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFeedback");

            migrationBuilder.DropTable(
                name: "ReportSubmission");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf38d403-f470-4833-ba6e-d6c7f1a0cdc3"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Bio", "Email", "FullName", "Gender", "PasswordHash", "PasswordResetToken", "PasswordSalt", "PasswordTokenExpires", "PhoneNumber", "ProfilePicture", "RefreshToken", "Role", "Team", "TokenCreated", "TokenExpires", "isDeleted" },
                values: new object[] { new Guid("54677a17-2d92-475e-89e8-6f1f8f3da2d8"), "", "admin@milsat.com", "Admin", 0, new byte[] { 96, 119, 122, 158, 72, 26, 254, 236, 59, 148, 50, 102, 66, 117, 137, 156, 206, 147, 99, 156, 148, 109, 175, 57, 120, 245, 217, 0, 150, 141, 94, 4, 191, 222, 60, 166, 200, 18, 199, 203, 126, 167, 200, 84, 8, 237, 38, 88, 137, 158, 60, 156, 198, 46, 223, 217, 214, 124, 86, 34, 102, 80, 23, 81 }, null, new byte[] { 156, 129, 169, 48, 215, 54, 135, 50, 245, 45, 80, 216, 38, 15, 14, 241, 146, 97, 184, 123, 222, 178, 137, 192, 208, 107, 180, 222, 95, 129, 101, 63, 72, 62, 231, 20, 251, 115, 40, 107, 148, 243, 122, 200, 193, 52, 95, 171, 125, 78, 134, 62, 147, 211, 85, 80, 23, 32, 34, 219, 143, 32, 45, 241, 150, 173, 73, 197, 132, 62, 203, 21, 191, 101, 223, 5, 212, 246, 20, 111, 113, 124, 90, 69, 39, 128, 21, 237, 98, 161, 225, 142, 149, 81, 53, 9, 193, 174, 54, 16, 49, 221, 99, 18, 180, 68, 136, 21, 175, 249, 58, 8, 13, 122, 150, 227, 150, 157, 245, 245, 234, 230, 231, 141, 198, 185, 23, 29 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "datasolutions", "", null, 0, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });
        }
    }
}
