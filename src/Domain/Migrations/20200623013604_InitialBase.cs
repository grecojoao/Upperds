using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class InitialBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HarvestGroupTrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    GrossWeight = table.Column<double>(nullable: false),
                    Information = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestGroupTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestGroupTrees_GroupTrees_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    SpeciesId = table.Column<int>(nullable: false),
                    GroupTreesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trees_GroupTrees_GroupTreesId",
                        column: x => x.GroupTreesId,
                        principalTable: "GroupTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trees_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HarvestTree",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    GrossWeight = table.Column<double>(nullable: false),
                    Information = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TreeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestTree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestTree_Trees_TreeId",
                        column: x => x.TreeId,
                        principalTable: "Trees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 1, "q1w2e3r4!1@2#3$4", 1, "Upperds" });

            migrationBuilder.CreateIndex(
                name: "IX_HarvestGroupTrees_GroupId",
                table: "HarvestGroupTrees",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestTree_TreeId",
                table: "HarvestTree",
                column: "TreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_GroupTreesId",
                table: "Trees",
                column: "GroupTreesId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_SpeciesId",
                table: "Trees",
                column: "SpeciesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HarvestGroupTrees");

            migrationBuilder.DropTable(
                name: "HarvestTree");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "GroupTrees");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
