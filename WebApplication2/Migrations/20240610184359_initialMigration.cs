using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Api.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtpLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StateDescription = table.Column<string>(type: "nvarchar(2550)", maxLength: 2550, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLGAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGAName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LGADescription = table.Column<string>(type: "nvarchar(2055)", maxLength: 2055, nullable: true),
                    CustomerStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLGAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLGAs_States_CustomerStateId",
                        column: x => x.CustomerStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerLGAId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerLGAs_CustomerLGAId",
                        column: x => x.CustomerLGAId,
                        principalTable: "CustomerLGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "StateDescription", "StateName" },
                values: new object[,]
                {
                    { 1, "Centre of exellence", "Lagos" },
                    { 2, "God's own state", "Ogun" },
                    { 3, "Owsome state", "Oyo" },
                    { 4, "Land of promise", "Akwa Ibom" },
                    { 5, "Some description", "Delta" }
                });

            migrationBuilder.InsertData(
                table: "CustomerLGAs",
                columns: new[] { "Id", "CustomerStateId", "LGADescription", "LGAName" },
                values: new object[,]
                {
                    { 1, 1, "Some description", "Kosofe" },
                    { 2, 1, "Some description", "Ikorodu" },
                    { 3, 4, "Some description", "Ikono" },
                    { 4, 5, "Some description", "Asaba" },
                    { 5, 2, "Some description", "Ibadan" },
                    { 6, 3, "Some description", "Oyo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLGAs_CustomerStateId",
                table: "CustomerLGAs",
                column: "CustomerStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerLGAId",
                table: "Customers",
                column: "CustomerLGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OtpLogs");

            migrationBuilder.DropTable(
                name: "CustomerLGAs");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
