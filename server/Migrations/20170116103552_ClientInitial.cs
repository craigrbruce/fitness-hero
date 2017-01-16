using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace server.Migrations
{
    public partial class ClientInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactName",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactTelephoneNumber",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Clients",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Clients",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Injury",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cause = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InjuryStatus = table.Column<int>(nullable: false),
                    Medication = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PotentialAction = table.Column<string>(nullable: true),
                    SignOrSymptom = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injury_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Uri = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AvatarId",
                table: "Clients",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Injury_ClientId",
                table: "Injury",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Image_AvatarId",
                table: "Clients",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Image_AvatarId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Injury");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AvatarId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmergencyContactName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmergencyContactTelephoneNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Clients");
        }
    }
}
