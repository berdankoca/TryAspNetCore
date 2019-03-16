using Microsoft.EntityFrameworkCore.Migrations;

namespace TryAspNetCore.Api.Migrations
{
    public partial class BaseEntityAddedCreatedAndUpdatedInformation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "EventRegistration",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdadetBy",
                table: "EventRegistration",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "EventRegistration",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Event",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdadetBy",
                table: "Event",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Event",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "EventRegistration",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "EventRegistration",
                newName: "UpdadetBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "EventRegistration",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Event",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Event",
                newName: "UpdadetBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Event",
                newName: "CreatedTime");
        }
    }
}
