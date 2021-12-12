using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.Migrations
{
    public partial class Added_Visit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visit_PatientCards_PatientCardId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visit",
                table: "Visit");

            migrationBuilder.RenameTable(
                name: "Visit",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_PatientCardId",
                table: "Visits",
                newName: "IX_Visits_PatientCardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visits",
                table: "Visits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_PatientCards_PatientCardId",
                table: "Visits",
                column: "PatientCardId",
                principalTable: "PatientCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_PatientCards_PatientCardId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "Visit");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PatientCardId",
                table: "Visit",
                newName: "IX_Visit_PatientCardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visit",
                table: "Visit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_PatientCards_PatientCardId",
                table: "Visit",
                column: "PatientCardId",
                principalTable: "PatientCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
