using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uptym.Data.Migrations
{
    public partial class addingsubproblemtypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubProblemTypes",
                schema: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProblemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProblemTypes_ProblemTypes_ProblemTypeId",
                        column: x => x.ProblemTypeId,
                        principalSchema: "Metadata",
                        principalTable: "ProblemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProblemTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProblemTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubProblemTypes_CreatedBy",
                schema: "Metadata",
                table: "SubProblemTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubProblemTypes_ProblemTypeId",
                schema: "Metadata",
                table: "SubProblemTypes",
                column: "ProblemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProblemTypes_UpdatedBy",
                schema: "Metadata",
                table: "SubProblemTypes",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubProblemTypes",
                schema: "Metadata");
        }
    }
}
