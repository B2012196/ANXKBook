using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookService.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatus_BookStatusId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookStatusId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookStatusId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Books",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "BookCopys",
                columns: table => new
                {
                    BookCopyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookStatusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopys", x => x.BookCopyId);
                    table.ForeignKey(
                        name: "FK_BookCopys_BookStatus_BookStatusId",
                        column: x => x.BookStatusId,
                        principalTable: "BookStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCopys_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StatusId",
                table: "Books",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopys_BookId",
                table: "BookCopys",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopys_BookStatusId",
                table: "BookCopys",
                column: "BookStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "BookStatus",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookCopys");

            migrationBuilder.DropIndex(
                name: "IX_Books_StatusId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookStatusId",
                table: "Books",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookStatusId",
                table: "Books",
                column: "BookStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatus_BookStatusId",
                table: "Books",
                column: "BookStatusId",
                principalTable: "BookStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
