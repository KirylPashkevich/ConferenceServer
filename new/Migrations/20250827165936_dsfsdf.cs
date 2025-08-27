using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManager.Migrations
{
    /// <inheritdoc />
    public partial class dsfsdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSponsors_Conferences_ConferenceId",
                table: "ConferenceSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSponsors_Sponsors_SponsorId",
                table: "ConferenceSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_PresentationSpeakers_Presentations_PresentationId",
                table: "PresentationSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_PresentationSpeakers_Users_UserId",
                table: "PresentationSpeakers");

            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Presentations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PresentationSpeakers",
                newName: "SpeakersId");

            migrationBuilder.RenameColumn(
                name: "PresentationId",
                table: "PresentationSpeakers",
                newName: "PresentationsId");

            migrationBuilder.RenameIndex(
                name: "IX_PresentationSpeakers_UserId",
                table: "PresentationSpeakers",
                newName: "IX_PresentationSpeakers_SpeakersId");

            migrationBuilder.RenameColumn(
                name: "SponsorId",
                table: "ConferenceSponsors",
                newName: "SponsorsId");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "ConferenceSponsors",
                newName: "ConferencesId");

            migrationBuilder.RenameIndex(
                name: "IX_ConferenceSponsors_SponsorId",
                table: "ConferenceSponsors",
                newName: "IX_ConferenceSponsors_SponsorsId");

            migrationBuilder.UpdateData(
                table: "Sponsors",
                keyColumn: "Website",
                keyValue: null,
                column: "Website",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Sponsors",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sponsors",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Sponsors",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sponsors",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Sponsors",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Sponsors",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Sponsors",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Sponsors",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Presentations",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Presentations",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Presentations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Presentations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Presentations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSponsors_Conferences_ConferencesId",
                table: "ConferenceSponsors",
                column: "ConferencesId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSponsors_Sponsors_SponsorsId",
                table: "ConferenceSponsors",
                column: "SponsorsId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentationSpeakers_Presentations_PresentationsId",
                table: "PresentationSpeakers",
                column: "PresentationsId",
                principalTable: "Presentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentationSpeakers_Users_SpeakersId",
                table: "PresentationSpeakers",
                column: "SpeakersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSponsors_Conferences_ConferencesId",
                table: "ConferenceSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSponsors_Sponsors_SponsorsId",
                table: "ConferenceSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_PresentationSpeakers_Presentations_PresentationsId",
                table: "PresentationSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_PresentationSpeakers_Users_SpeakersId",
                table: "PresentationSpeakers");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Presentations");

            migrationBuilder.RenameColumn(
                name: "SpeakersId",
                table: "PresentationSpeakers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PresentationsId",
                table: "PresentationSpeakers",
                newName: "PresentationId");

            migrationBuilder.RenameIndex(
                name: "IX_PresentationSpeakers_SpeakersId",
                table: "PresentationSpeakers",
                newName: "IX_PresentationSpeakers_UserId");

            migrationBuilder.RenameColumn(
                name: "SponsorsId",
                table: "ConferenceSponsors",
                newName: "SponsorId");

            migrationBuilder.RenameColumn(
                name: "ConferencesId",
                table: "ConferenceSponsors",
                newName: "ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_ConferenceSponsors_SponsorsId",
                table: "ConferenceSponsors",
                newName: "IX_ConferenceSponsors_SponsorId");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Sponsors",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sponsors",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sponsors",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Presentations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Presentations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSponsors_Conferences_ConferenceId",
                table: "ConferenceSponsors",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSponsors_Sponsors_SponsorId",
                table: "ConferenceSponsors",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentationSpeakers_Presentations_PresentationId",
                table: "PresentationSpeakers",
                column: "PresentationId",
                principalTable: "Presentations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentationSpeakers_Users_UserId",
                table: "PresentationSpeakers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
