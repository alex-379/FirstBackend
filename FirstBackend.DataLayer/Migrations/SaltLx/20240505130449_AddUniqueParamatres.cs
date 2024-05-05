using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstBackend.DataLayer.Migrations.SaltLx
{
    /// <inheritdoc />
    public partial class AddUniqueParamatres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_salts_user_id",
                table: "salts",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_salts_user_id",
                table: "salts");
        }
    }
}
