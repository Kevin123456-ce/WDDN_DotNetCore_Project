using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionForum.Migrations
{
    public partial class AllData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentQuestionData");

            migrationBuilder.DropTable(
                name: "CommentData");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommentDetails",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentDescr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDetails", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentDetails_AspNetUsers_CommentUserId",
                        column: x => x.CommentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentQuestionDetails",
                columns: table => new
                {
                    CUId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentQuestionDetails", x => x.CUId);
                    table.ForeignKey(
                        name: "FK_CommentQuestionDetails_CommentDetails_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentDetails",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentQuestionDetails_QuestionDetails_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionDetails",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentDetails_CommentUserId",
                table: "CommentDetails",
                column: "CommentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentQuestionDetails_CommentId",
                table: "CommentQuestionDetails",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentQuestionDetails_QuestionId",
                table: "CommentQuestionDetails",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentQuestionDetails");

            migrationBuilder.DropTable(
                name: "CommentDetails");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CommentData",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentDescr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentData", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentData_AspNetUsers_CommentUserId",
                        column: x => x.CommentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentQuestionData",
                columns: table => new
                {
                    CUId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentQuestionData", x => x.CUId);
                    table.ForeignKey(
                        name: "FK_CommentQuestionData_CommentData_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentData",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentQuestionData_QuestionDetails_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionDetails",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentData_CommentUserId",
                table: "CommentData",
                column: "CommentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentQuestionData_CommentId",
                table: "CommentQuestionData",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentQuestionData_QuestionId",
                table: "CommentQuestionData",
                column: "QuestionId");
        }
    }
}
