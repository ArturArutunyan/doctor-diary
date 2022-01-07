using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.DbMigrator.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    Patronymic = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    Address = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "DATE", nullable: false),
                    Snils = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(30)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SickLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<long>(type: "INTEGER", nullable: false),
                    PatientCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SickLeaves_PatientCards_PatientCardId",
                        column: x => x.PatientCardId,
                        principalTable: "PatientCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TermsOfSickLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SickLeaveId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsOfSickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermsOfSickLeaves_SickLeaves_SickLeaveId",
                        column: x => x.SickLeaveId,
                        principalTable: "SickLeaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SickLeaves_PatientCardId",
                table: "SickLeaves",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TermsOfSickLeaves_SickLeaveId",
                table: "TermsOfSickLeaves",
                column: "SickLeaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermsOfSickLeaves");

            migrationBuilder.DropTable(
                name: "SickLeaves");

            migrationBuilder.DropTable(
                name: "PatientCards");
        }
    }
}
