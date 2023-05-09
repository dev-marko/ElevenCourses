using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevenCourses.Data.Migrations
{
    /// <inheritdoc />
    public partial class pdfUploadChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_User_CreatorId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "Files",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Files",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Files",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_CreatorId",
                table: "Files",
                newName: "IX_Files_ApplicationUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekId",
                table: "Files",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_User_ApplicationUserId",
                table: "Files",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_User_ApplicationUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Files",
                newName: "FileUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Files",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Files",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ApplicationUserId",
                table: "Files",
                newName: "IX_Files_CreatorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_User_CreatorId",
                table: "Files",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Weeks_WeekId",
                table: "Files",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
