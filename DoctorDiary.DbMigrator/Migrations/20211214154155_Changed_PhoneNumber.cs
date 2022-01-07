using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.DbMigrator.Migrations
{
    public partial class Changed_PhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PatientCards");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberClearedFromFormat",
                table: "PatientCards",
                type: "NVARCHAR(11)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberInReadableFormat",
                table: "PatientCards",
                type: "NVARCHAR(15)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumberClearedFromFormat",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "PhoneNumberInReadableFormat",
                table: "PatientCards");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PatientCards",
                type: "NVARCHAR(30)",
                nullable: true);
        }
    }
}
