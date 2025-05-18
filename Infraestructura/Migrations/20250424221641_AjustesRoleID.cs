using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BD.Migrations
{
    /// <inheritdoc />
    public partial class AjustesRoleID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "User",
                newName: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_ID",
                table: "User",
                column: "Role_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ApproverRole_Role_ID",
                table: "User",
                column: "Role_ID",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ApproverRole_Role_ID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Role_ID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Role_ID",
                table: "User",
                newName: "Role");
        }
    }
}
