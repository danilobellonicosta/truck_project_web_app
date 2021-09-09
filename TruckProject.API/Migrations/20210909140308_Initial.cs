using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TruckProject.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelTrucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelTrucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FabricationYear = table.Column<int>(type: "int", nullable: false),
                    Chassi = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModelYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_ModelTrucks_ModelTruckId",
                        column: x => x.ModelTruckId,
                        principalTable: "ModelTrucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ModelTrucks",
                columns: new[] { "Id", "Model" },
                values: new object[] { new Guid("e86812a3-9d40-49de-bd13-6e4c3fc38445"), "FC" });

            migrationBuilder.InsertData(
                table: "ModelTrucks",
                columns: new[] { "Id", "Model" },
                values: new object[] { new Guid("0389f142-9e57-4225-8657-036e815d0e83"), "FH" });

            migrationBuilder.InsertData(
                table: "ModelTrucks",
                columns: new[] { "Id", "Model" },
                values: new object[] { new Guid("9c128ed3-7617-44e3-8bea-3d79a9d9e8f3"), "FM" });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ModelTruckId",
                table: "Trucks",
                column: "ModelTruckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "ModelTrucks");
        }
    }
}
