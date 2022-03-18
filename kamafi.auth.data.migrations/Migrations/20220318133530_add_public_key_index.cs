using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kamafi.auth.data.migrations.Migrations
{
    public partial class add_public_key_index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_public_key",
                table: "user",
                column: "public_key");

            migrationBuilder.CreateIndex(
                name: "ix_role_public_key",
                table: "role",
                column: "public_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_public_key",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_role_public_key",
                table: "role");
        }
    }
}
