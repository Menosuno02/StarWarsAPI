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
                name: "HABITANTS",
                columns: table => new
                {
                    IDHABITANT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMEHABITANT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDSPECIES = table.Column<int>(type: "int", nullable: false),
                    IDHOMEPLANET = table.Column<int>(type: "int", nullable: false),
                    ISREBEL = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HABITANTS", x => x.IDHABITANT);
                });

            migrationBuilder.CreateTable(
                name: "PLANETS",
                columns: table => new
                {
                    IDPLANET = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMEPLANET = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLANETS", x => x.IDPLANET);
                });

            migrationBuilder.CreateTable(
                name: "SPECIES",
                columns: table => new
                {
                    IDSPECIES = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMESPECIES = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPECIES", x => x.IDSPECIES);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HABITANTS");

            migrationBuilder.DropTable(
                name: "PLANETS");

            migrationBuilder.DropTable(
                name: "SPECIES");
        }
    }
}
