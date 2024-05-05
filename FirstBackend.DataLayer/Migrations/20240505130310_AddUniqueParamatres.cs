using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueParamatres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_users_id",
                table: "users",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_mail",
                table: "users",
                column: "mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_id",
                table: "orders",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_devices_id",
                table: "devices",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_mail",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_orders_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_devices_id",
                table: "devices");
        }
    }
}
