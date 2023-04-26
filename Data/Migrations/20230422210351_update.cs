using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevenCourses.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekId",
                table: "Files",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id");
        }
    }
}
