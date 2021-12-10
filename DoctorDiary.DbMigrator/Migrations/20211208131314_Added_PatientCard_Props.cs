using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.DbMigrator.Migrations
{
    public partial class Added_PatientCard_Props : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "PatientCards",
                newName: "Street");

            migrationBuilder.AlterColumn<string>(
                name: "Snils",
                table: "PatientCards",
                type: "NVARCHAR(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(30)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apartment",
                table: "PatientCards",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PatientCards",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PatientCards",
                type: "NVARCHAR(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "PatientCards",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicy",
                table: "PatientCards",
                type: "NVARCHAR(16)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfWork",
                table: "PatientCards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Precinct",
                table: "PatientCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apartment",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "House",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "InsurancePolicy",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "PlaceOfWork",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "Precinct",
                table: "PatientCards");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "PatientCards",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Snils",
                table: "PatientCards",
                type: "NVARCHAR(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)",
                oldNullable: true);
        }
    }
}
