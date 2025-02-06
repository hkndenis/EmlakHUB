using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PropertyListing.Infrastructure.Persistence.Migrations;

public partial class AddPropertyAlerts : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PropertyAlerts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                MaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                MinPrice = table.Column<decimal>(type: "numeric", nullable: true),
                MinBedrooms = table.Column<int>(type: "integer", nullable: true),
                MinSquareMeters = table.Column<decimal>(type: "numeric", nullable: true),
                Type = table.Column<int>(type: "integer", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                LastNotificationSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "text", nullable: true),
                LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                LastModifiedBy = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PropertyAlerts", x => x.Id);
                table.ForeignKey(
                    name: "FK_PropertyAlerts_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PropertyAlerts_UserId",
            table: "PropertyAlerts",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PropertyAlerts");
    }
} 