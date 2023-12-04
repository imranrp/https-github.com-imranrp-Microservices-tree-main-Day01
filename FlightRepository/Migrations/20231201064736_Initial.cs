using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightRepository.Migrations
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
                    FlightNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FromCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ToCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
