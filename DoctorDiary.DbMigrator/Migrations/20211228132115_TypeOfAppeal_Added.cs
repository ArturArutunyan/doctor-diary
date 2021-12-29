using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.DbMigrator.Migrations
{
    public partial class TypeOfAppeal_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfAppeal",
                table: "Visits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentPosition",
                table: "PatientCards",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfAppeal",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "EmploymentPosition",
                table: "PatientCards");
        }
    }
}
