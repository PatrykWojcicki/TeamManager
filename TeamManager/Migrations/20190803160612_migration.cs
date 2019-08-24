using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManager.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    groupName = table.Column<string>(nullable: true),
                    isChecked = table.Column<bool>(nullable: false),
                    TrainingDay = table.Column<int>(nullable: false),
                    TrainingTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KarateKidsAll",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    phone = table.Column<int>(nullable: false),
                    Group = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KarateKidsAll", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DateBaseAll",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualDate = table.Column<DateTime>(nullable: false),
                    ChildName = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    ItIsPayment = table.Column<bool>(nullable: false),
                    KarateKidID = table.Column<int>(nullable: true),
                    KarateKidID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateBaseAll", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DateBaseAll_KarateKidsAll_KarateKidID",
                        column: x => x.KarateKidID,
                        principalTable: "KarateKidsAll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DateBaseAll_KarateKidsAll_KarateKidID1",
                        column: x => x.KarateKidID1,
                        principalTable: "KarateKidsAll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateBaseAll_KarateKidID",
                table: "DateBaseAll",
                column: "KarateKidID");

            migrationBuilder.CreateIndex(
                name: "IX_DateBaseAll_KarateKidID1",
                table: "DateBaseAll",
                column: "KarateKidID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateBaseAll");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "KarateKidsAll");
        }
    }
}
