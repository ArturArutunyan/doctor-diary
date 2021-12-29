using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorDiary.DbMigrator.Migrations
{
    public partial class More_Address_Props_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumberClearedFromFormat",
                table: "PatientCards");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberInReadableFormat",
                table: "PatientCards",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "PatientCards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "PatientCards",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.AddColumn<string>(
                name: "Entrance",
                table: "PatientCards",
                type: "NVARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "PatientCards",
                type: "NVARCHAR(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entrance",
                table: "PatientCards");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "PatientCards");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "PatientCards",
                newName: "PhoneNumberInReadableFormat");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "PatientCards",
                type: "NVARCHAR(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "PatientCards",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberClearedFromFormat",
                table: "PatientCards",
                type: "NVARCHAR(11)",
                nullable: true);
        }
    }
}
