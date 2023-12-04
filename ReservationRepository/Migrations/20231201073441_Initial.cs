using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationRepository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightSchedule",
                columns: table => new
                {
                    FlightNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchedule", x => new { x.FlightNo, x.TravelDate });
                });

            migrationBuilder.CreateTable(
                name: "ReservationMaster",
                columns: table => new
                {
                    PNRNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FlightNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoOfPassengers = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationMaster", x => x.PNRNo);
                    table.ForeignKey(
                        name: "FK_ReservationMaster_FlightSchedule_FlightNo_TravelDate",
                        columns: x => new { x.FlightNo, x.TravelDate },
                        principalTable: "FlightSchedule",
                        principalColumns: new[] { "FlightNo", "TravelDate" });
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetail",
                columns: table => new
                {
                    PNRNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    PassengerNo = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Age = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetail", x => new { x.PNRNo, x.PassengerNo });
                    table.ForeignKey(
                        name: "FK_ReservationDetail_ReservationMaster_PNRNo",
                        column: x => x.PNRNo,
                        principalTable: "ReservationMaster",
                        principalColumn: "PNRNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationMaster_FlightNo_TravelDate",
                table: "ReservationMaster",
                columns: new[] { "FlightNo", "TravelDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationDetail");

            migrationBuilder.DropTable(
                name: "ReservationMaster");

            migrationBuilder.DropTable(
                name: "FlightSchedule");
        }
    }
}
