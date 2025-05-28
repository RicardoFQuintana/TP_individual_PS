using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionDeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_Area_AreaId",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ProjectType_TypeId",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_StatusId",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_StatusId",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_Area_AreaId",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ProjectType_TypeId",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_User_CreatedById",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ApproverRole_RoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_Role");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProjectType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ProjectProposal",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProjectProposal",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "ProjectProposal",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ProjectProposal",
                newName: "CreateBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_TypeId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Type");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_StatusId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_CreatedById",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_CreateBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_AreaId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Area");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProjectApprovalStep",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_StatusId",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Area",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ApproverRole",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ApprovalStatus",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ApprovalRule",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "ApprovalRule",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_TypeId",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_Type");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_AreaId",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_Area");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectType",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectProposal",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectProposal",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "ProjectApprovalStep",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Area",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApproverRole",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApprovalStatus",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_Area_Area",
                table: "ApprovalRule",
                column: "Area",
                principalTable: "Area",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ProjectType_Type",
                table: "ApprovalRule",
                column: "Type",
                principalTable: "ProjectType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_Status",
                table: "ProjectApprovalStep",
                column: "Status",
                principalTable: "ApprovalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_Status",
                table: "ProjectProposal",
                column: "Status",
                principalTable: "ApprovalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_Area_Area",
                table: "ProjectProposal",
                column: "Area",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ProjectType_Type",
                table: "ProjectProposal",
                column: "Type",
                principalTable: "ProjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_User_CreateBy",
                table: "ProjectProposal",
                column: "CreateBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ApproverRole_Role",
                table: "User",
                column: "Role",
                principalTable: "ApproverRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_Area_Area",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ProjectType_Type",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_Status",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_Status",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_Area_Area",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ProjectType_Type",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_User_CreateBy",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ApproverRole_Role",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectType",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ProjectProposal",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ProjectProposal",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "ProjectProposal",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "ProjectProposal",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Type",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Status",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_CreateBy",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Area",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_AreaId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ProjectApprovalStep",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_Status",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_StatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Area",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApproverRole",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApprovalStatus",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ApprovalRule",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "ApprovalRule",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_Type",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_Area",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_AreaId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectType",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProjectProposal",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectProposal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "ProjectApprovalStep",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Area",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApproverRole",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApprovalStatus",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_Area_AreaId",
                table: "ApprovalRule",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ProjectType_TypeId",
                table: "ApprovalRule",
                column: "TypeId",
                principalTable: "ProjectType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_StatusId",
                table: "ProjectApprovalStep",
                column: "StatusId",
                principalTable: "ApprovalStatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_StatusId",
                table: "ProjectProposal",
                column: "StatusId",
                principalTable: "ApprovalStatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_Area_AreaId",
                table: "ProjectProposal",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ProjectType_TypeId",
                table: "ProjectProposal",
                column: "TypeId",
                principalTable: "ProjectType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_User_CreatedById",
                table: "ProjectProposal",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ApproverRole_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
