using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStructureDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_devices_users_owner_id",
                table: "devices");

            migrationBuilder.DropIndex(
                name: "ix_devices_owner_id",
                table: "devices");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "devices");

            migrationBuilder.CreateTable(
                name: "device_dto_order_dto",
                columns: table => new
                {
                    devices_id = table.Column<Guid>(type: "uuid", nullable: false),
                    orders_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_dto_order_dto", x => new { x.devices_id, x.orders_id });
                    table.ForeignKey(
                        name: "fk_device_dto_order_dto_devices_devices_id",
                        column: x => x.devices_id,
                        principalTable: "devices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_device_dto_order_dto_orders_orders_id",
                        column: x => x.orders_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_device_dto_order_dto_orders_id",
                table: "device_dto_order_dto",
                column: "orders_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_dto_order_dto");

            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "devices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_devices_owner_id",
                table: "devices",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_devices_users_owner_id",
                table: "devices",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
