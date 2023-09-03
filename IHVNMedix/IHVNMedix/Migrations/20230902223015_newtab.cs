using Microsoft.EntityFrameworkCore.Migrations;

namespace IHVNMedix.Migrations
{
    public partial class newtab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncounterId",
                table: "Diagnosis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncounterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthItem_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_EncounterId",
                table: "Diagnosis",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthItem_EncounterId",
                table: "HealthItem",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Encounters_EncounterId",
                table: "Diagnosis",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Encounters_EncounterId",
                table: "Diagnosis");

            migrationBuilder.DropTable(
                name: "HealthItem");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosis_EncounterId",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Diagnosis");
        }
    }
}
