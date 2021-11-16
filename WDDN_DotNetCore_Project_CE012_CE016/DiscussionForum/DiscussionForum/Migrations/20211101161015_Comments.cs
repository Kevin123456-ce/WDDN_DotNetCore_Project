using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscussionForum.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentData",
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
                    table.PrimaryKey("PK_CommentData", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentData_AspNetUsers_CommentUserId",
                        column: x => x.CommentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionDetails",
                columns: table => new
                {
                    QId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QDescr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionDetails", x => x.QId);
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

            migrationBuilder.CreateTable(
                name: "QuestionUserDetails",
                columns: table => new
                {
                    QUId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreaterUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUserDetails", x => x.QUId);
                    table.ForeignKey(
                        name: "FK_QuestionUserDetails_AspNetUsers_CreaterUserId",
                        column: x => x.CreaterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionUserDetails_QuestionDetails_QuestionId",
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

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUserDetails_CreaterUserId",
                table: "QuestionUserDetails",
                column: "CreaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUserDetails_QuestionId",
                table: "QuestionUserDetails",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentQuestionData");

            migrationBuilder.DropTable(
                name: "QuestionUserDetails");

            migrationBuilder.DropTable(
                name: "CommentData");

            migrationBuilder.DropTable(
                name: "QuestionDetails");
        }
    }
}
