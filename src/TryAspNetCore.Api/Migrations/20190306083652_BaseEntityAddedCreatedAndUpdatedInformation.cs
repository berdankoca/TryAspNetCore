using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TryAspNetCore.Api.Migrations
{
    public partial class BaseEntityAddedCreatedAndUpdatedInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "EventRegistration",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "EventRegistration",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdadetBy",
                table: "EventRegistration",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "EventRegistration",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Event",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdadetBy",
                table: "Event",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "UpdadetBy",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "UpdadetBy",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Event");
        }
    }
}
