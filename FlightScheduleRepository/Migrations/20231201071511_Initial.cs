using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightScheduleRepository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightNo);
                });

            migrationBuilder.CreateTable(
                name: "FlightSchedule",
                columns: table => new
                {
                    FlightNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArriveTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchedule", x => new { x.FlightNo, x.TravelDate });
                    table.ForeignKey(
                        name: "FK_FlightSchedule_Flight_FlightNo",
                        column: x => x.FlightNo,
                        principalTable: "Flight",
                        principalColumn: "FlightNo",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightSchedule");

            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
