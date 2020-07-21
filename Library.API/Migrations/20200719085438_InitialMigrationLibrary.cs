using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class InitialMigrationLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisheres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisheres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    YearOfIssue = table.Column<DateTime>(nullable: false),
                    NumberOfPages = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publisheres_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisheres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfCopies = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    LibraryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCopies_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lendings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WitdrawDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    BookId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lendings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lendings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "City", "Name", "Phone" },
                values: new object[] { 1, null, null, "Vlatko", "222" });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "Address", "City", "Name" },
                values: new object[] { 1, "Partizanska bb", "Skopje", "First Library" });

            migrationBuilder.InsertData(
                table: "Publisheres",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { 1, "Macedonia", "Tri" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "NumberOfPages", "PublisherId", "Title", "YearOfIssue" },
                values: new object[] { 1, 300, 1, "First Book", new DateTime(2020, 7, 19, 10, 54, 37, 778, DateTimeKind.Local).AddTicks(294) });

            migrationBuilder.InsertData(
                table: "BookCopies",
                columns: new[] { "Id", "BookId", "LibraryId", "NumberOfCopies" },
                values: new object[] { 1, 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "Lendings",
                columns: new[] { "Id", "BookId", "ClientId", "ReturnDate", "WitdrawDate" },
                values: new object[] { 1, 1, 1, null, new DateTime(2020, 7, 19, 10, 54, 37, 781, DateTimeKind.Local).AddTicks(8144) });

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookId",
                table: "BookCopies",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_LibraryId",
                table: "BookCopies",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_BookId",
                table: "Lendings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_ClientId",
                table: "Lendings",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Lendings");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Publisheres");
        }
    }
}
