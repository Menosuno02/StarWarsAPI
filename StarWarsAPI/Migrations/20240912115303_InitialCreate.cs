using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habitants",
                columns: table => new
                {
                    IdHabitant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHabitant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSpecies = table.Column<int>(type: "int", nullable: false),
                    IdHomePlanet = table.Column<int>(type: "int", nullable: false),
                    IsRebel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitants", x => x.IdHabitant);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    IdPlanet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePlanet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.IdPlanet);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    IdSpecies = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSpecies = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.IdSpecies);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitants");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
