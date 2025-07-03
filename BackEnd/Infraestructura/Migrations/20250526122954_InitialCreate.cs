using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ApproverRole_ApproverRole_ID",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_Area_Area_ID",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ProjectType_Type_ID",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_Status_ID",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ApproverRole_ApproverRole_ID",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposal_ID",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_User_ApproverUser_ID",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_Status_ID",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_Area_Area_ID",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_ProjectType_Type_ID",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProposal_User_CreateBy_ID",
                table: "ProjectProposal");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ApproverRole_Role_ID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Role_ID",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_ID",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameColumn(
                name: "Type_ID",
                table: "ProjectProposal",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Status_ID",
                table: "ProjectProposal",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "CreateBy_ID",
                table: "ProjectProposal",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "Area_ID",
                table: "ProjectProposal",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Type_ID",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Status_ID",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_CreateBy_ID",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_Area_ID",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_AreaId");

            migrationBuilder.RenameColumn(
                name: "Status_ID",
                table: "ProjectApprovalStep",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "ProjectProposal_ID",
                table: "ProjectApprovalStep",
                newName: "ProjectProposalId");

            migrationBuilder.RenameColumn(
                name: "ApproverUser_ID",
                table: "ProjectApprovalStep",
                newName: "ApproverUserId");

            migrationBuilder.RenameColumn(
                name: "ApproverRole_ID",
                table: "ProjectApprovalStep",
                newName: "ApproverRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_Status_ID",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ProjectProposal_ID",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ProjectProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ApproverUser_ID",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ApproverUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ApproverRole_ID",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ApproverRoleId");

            migrationBuilder.RenameColumn(
                name: "Type_ID",
                table: "ApprovalRule",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Area_ID",
                table: "ApprovalRule",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "ApproverRole_ID",
                table: "ApprovalRule",
                newName: "ApproverRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_Type_ID",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_Area_ID",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_ApproverRole_ID",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_ApproverRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ApproverRole_ApproverRoleId",
                table: "ApprovalRule",
                column: "ApproverRoleId",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ProjectApprovalStep_ApproverRole_ApproverRoleId",
                table: "ProjectApprovalStep",
                column: "ApproverRoleId",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposalId",
                table: "ProjectApprovalStep",
                column: "ProjectProposalId",
                principalTable: "ProjectProposal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_User_ApproverUserId",
                table: "ProjectApprovalStep",
                column: "ApproverUserId",
                principalTable: "User",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ApproverRole_ApproverRoleId",
                table: "ApprovalRule");

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
                name: "FK_ProjectApprovalStep_ApproverRole_ApproverRoleId",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposalId",
                table: "ProjectApprovalStep");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApprovalStep_User_ApproverUserId",
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
                name: "RoleId",
                table: "User",
                newName: "Role_ID");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_Role_ID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ProjectProposal",
                newName: "Type_ID");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProjectProposal",
                newName: "Status_ID");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "ProjectProposal",
                newName: "CreateBy_ID");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "ProjectProposal",
                newName: "Area_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_TypeId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Type_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_StatusId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Status_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_CreatedById",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_CreateBy_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectProposal_AreaId",
                table: "ProjectProposal",
                newName: "IX_ProjectProposal_Area_ID");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "ProjectApprovalStep",
                newName: "Status_ID");

            migrationBuilder.RenameColumn(
                name: "ProjectProposalId",
                table: "ProjectApprovalStep",
                newName: "ProjectProposal_ID");

            migrationBuilder.RenameColumn(
                name: "ApproverUserId",
                table: "ProjectApprovalStep",
                newName: "ApproverUser_ID");

            migrationBuilder.RenameColumn(
                name: "ApproverRoleId",
                table: "ProjectApprovalStep",
                newName: "ApproverRole_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_StatusId",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_Status_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ProjectProposalId",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ProjectProposal_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ApproverUserId",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ApproverUser_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApprovalStep_ApproverRoleId",
                table: "ProjectApprovalStep",
                newName: "IX_ProjectApprovalStep_ApproverRole_ID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ApprovalRule",
                newName: "Type_ID");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "ApprovalRule",
                newName: "Area_ID");

            migrationBuilder.RenameColumn(
                name: "ApproverRoleId",
                table: "ApprovalRule",
                newName: "ApproverRole_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_TypeId",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_Type_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_AreaId",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_Area_ID");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRule_ApproverRoleId",
                table: "ApprovalRule",
                newName: "IX_ApprovalRule_ApproverRole_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ApproverRole_ApproverRole_ID",
                table: "ApprovalRule",
                column: "ApproverRole_ID",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_Area_Area_ID",
                table: "ApprovalRule",
                column: "Area_ID",
                principalTable: "Area",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ProjectType_Type_ID",
                table: "ApprovalRule",
                column: "Type_ID",
                principalTable: "ProjectType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ApprovalStatus_Status_ID",
                table: "ProjectApprovalStep",
                column: "Status_ID",
                principalTable: "ApprovalStatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ApproverRole_ApproverRole_ID",
                table: "ProjectApprovalStep",
                column: "ApproverRole_ID",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposal_ID",
                table: "ProjectApprovalStep",
                column: "ProjectProposal_ID",
                principalTable: "ProjectProposal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApprovalStep_User_ApproverUser_ID",
                table: "ProjectApprovalStep",
                column: "ApproverUser_ID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ApprovalStatus_Status_ID",
                table: "ProjectProposal",
                column: "Status_ID",
                principalTable: "ApprovalStatus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_Area_Area_ID",
                table: "ProjectProposal",
                column: "Area_ID",
                principalTable: "Area",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_ProjectType_Type_ID",
                table: "ProjectProposal",
                column: "Type_ID",
                principalTable: "ProjectType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProposal_User_CreateBy_ID",
                table: "ProjectProposal",
                column: "CreateBy_ID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ApproverRole_Role_ID",
                table: "User",
                column: "Role_ID",
                principalTable: "ApproverRole",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
