using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevenCourses.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConnectWeeksAndFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WeekId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Files_WeekId",
                table: "Files",
                column: "WeekId");

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

            migrationBuilder.DropIndex(
                name: "IX_Files_WeekId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "WeekId",
                table: "Files");
        }
    }
}
