using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kamafi.auth.data.migrations.Migrations
{
    public partial class update_user_api_pk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_api_key",
                table: "user_api_key");

            migrationBuilder.AlterColumn<bool>(
                name: "is_enabled",
                table: "user_api_key",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "true");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_api_key",
                table: "user_api_key",
                columns: new[] { "user_id", "api_key" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_api_key",
                table: "user_api_key");

            migrationBuilder.AlterColumn<bool>(
                name: "is_enabled",
                table: "user_api_key",
                type: "boolean",
                nullable: false,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_api_key",
                table: "user_api_key",
                column: "user_id");
        }
    }
}
