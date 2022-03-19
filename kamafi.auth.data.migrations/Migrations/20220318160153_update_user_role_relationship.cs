using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kamafi.auth.data.migrations.Migrations
{
    public partial class update_user_role_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_role_name",
                table: "user");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_name",
                table: "user",
                column: "role_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_role_name",
                table: "user");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_name",
                table: "user",
                column: "role_name",
                unique: true);
        }
    }
}
