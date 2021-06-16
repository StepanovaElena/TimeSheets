using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeSheets.Migrations
{
    public partial class ChangesAfterAddingValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_ClientId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "clients",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "invoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_clients_UserId",
                table: "clients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_UserId",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "invoices");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "clients",
                newName: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_ClientId",
                table: "users",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
