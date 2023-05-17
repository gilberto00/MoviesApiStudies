using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingrelashionshipbetweenSessionandCinema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CinemaId",
                table: "Sessions",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Cinemas_CinemaId",
                table: "Sessions",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Cinemas_CinemaId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CinemaId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Sessions");
        }
    }
}
