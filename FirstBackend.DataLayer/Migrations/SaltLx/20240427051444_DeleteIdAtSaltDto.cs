using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstBackend.DataLayer.Migrations.SaltLx
{
    /// <inheritdoc />
    public partial class DeleteIdAtSaltDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_salts",
                table: "salts");

            migrationBuilder.DropColumn(
                name: "id",
                table: "salts");

            migrationBuilder.AddPrimaryKey(
                name: "pk_salts",
                table: "salts",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_salts",
                table: "salts");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "salts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_salts",
                table: "salts",
                column: "id");
        }
    }
}
