using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Watchlist.API.Migrations
{
    public partial class WatchlistV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Userid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatchListsMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WatchlistId = table.Column<Guid>(nullable: false),
                    MediaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchListsMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchListsMedias_WatchLists_WatchlistId",
                        column: x => x.WatchlistId,
                        principalTable: "WatchLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WatchLists",
                columns: new[] { "Id", "Name", "Userid" },
                values: new object[] { new Guid("c3e95c44-1e39-4328-9d52-e0291ddf2da7"), "WatchlaterPlaylist", new Guid("434c538e-7aed-42d7-a667-92b0760ef88c") });

            migrationBuilder.InsertData(
                table: "WatchLists",
                columns: new[] { "Id", "Name", "Userid" },
                values: new object[] { new Guid("34487500-ba60-4962-9342-907c4665681f"), "WatchlaterPlaylist", new Guid("36de8e2f-e3ff-4718-a2e8-c9fc08610728") });

            migrationBuilder.InsertData(
                table: "WatchLists",
                columns: new[] { "Id", "Name", "Userid" },
                values: new object[] { new Guid("1c389e4c-0272-48ea-b260-6b29c0c51ad7"), "WatchlaterPlaylist", new Guid("443ed08c-e98a-4ac1-84ed-fee7b27513f9") });

            migrationBuilder.InsertData(
                table: "WatchListsMedias",
                columns: new[] { "Id", "MediaId", "WatchlistId" },
                values: new object[] { new Guid("e0545da1-bc9c-47b6-aa2c-b81e61424a30"), new Guid("4259cac2-fcf2-4ca2-b311-813bc291c2ce"), new Guid("c3e95c44-1e39-4328-9d52-e0291ddf2da7") });

            migrationBuilder.InsertData(
                table: "WatchListsMedias",
                columns: new[] { "Id", "MediaId", "WatchlistId" },
                values: new object[] { new Guid("b25a6a4c-eefe-4793-aa5d-5b2d357f4d29"), new Guid("b79a54f9-45a2-421b-735e-08d713cec375"), new Guid("c3e95c44-1e39-4328-9d52-e0291ddf2da7") });

            migrationBuilder.InsertData(
                table: "WatchListsMedias",
                columns: new[] { "Id", "MediaId", "WatchlistId" },
                values: new object[] { new Guid("0c58c6e8-2c84-4b7a-b6a5-068652b0058a"), new Guid("8d81a6f1-f933-429a-91fb-7d38cd54b142"), new Guid("c3e95c44-1e39-4328-9d52-e0291ddf2da7") });

            migrationBuilder.CreateIndex(
                name: "IX_WatchListsMedias_WatchlistId",
                table: "WatchListsMedias",
                column: "WatchlistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchListsMedias");

            migrationBuilder.DropTable(
                name: "WatchLists");
        }
    }
}
