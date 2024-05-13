using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstBackend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ReconfigureDatabaseManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_dto_order_dto");

            migrationBuilder.CreateTable(
                name: "devices_orders",
                columns: table => new
                {
                    device_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number_devices = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_devices_orders", x => new { x.device_id, x.order_id });
                    table.ForeignKey(
                        name: "fk_devices_orders_devices_device_id",
                        column: x => x.device_id,
                        principalTable: "devices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_devices_orders_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_devices_orders_order_id",
                table: "devices_orders",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "devices_orders");

            migrationBuilder.CreateTable(
                name: "device_dto_order_dto",
                columns: table => new
                {
                    devices_id = table.Column<Guid>(type: "uuid", nullable: false),
                    orders_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number_devices = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
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
    }
}
