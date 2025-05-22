using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2_Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ApproverRole",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverRole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_ApproverRole_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "ApproverRole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Area_ID = table.Column<int>(type: "int", nullable: true),
                    Type_ID = table.Column<int>(type: "int", nullable: true),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    ApproverRole_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRule_ApproverRole_ApproverRole_ID",
                        column: x => x.ApproverRole_ID,
                        principalTable: "ApproverRole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalRule_Area_Area_ID",
                        column: x => x.Area_ID,
                        principalTable: "Area",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ApprovalRule_ProjectType_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ProjectType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectProposal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area_ID = table.Column<int>(type: "int", nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: false),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    Status_ID = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProposal_ApprovalStatus_Status_ID",
                        column: x => x.Status_ID,
                        principalTable: "ApprovalStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProposal_Area_Area_ID",
                        column: x => x.Area_ID,
                        principalTable: "Area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProposal_ProjectType_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ProjectType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProposal_User_CreateBy_ID",
                        column: x => x.CreateBy_ID,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectApprovalStep",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectProposal_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApproverUser_ID = table.Column<int>(type: "int", nullable: true),
                    ApproverRole_ID = table.Column<int>(type: "int", nullable: false),
                    Status_ID = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectApprovalStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApprovalStatus_Status_ID",
                        column: x => x.Status_ID,
                        principalTable: "ApprovalStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ApproverRole_ApproverRole_ID",
                        column: x => x.ApproverRole_ID,
                        principalTable: "ApproverRole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_ProjectProposal_ProjectProposal_ID",
                        column: x => x.ProjectProposal_ID,
                        principalTable: "ProjectProposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectApprovalStep_User_ApproverUser_ID",
                        column: x => x.ApproverUser_ID,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Observed" }
                });

            migrationBuilder.InsertData(
                table: "ApproverRole",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "Líder de Área" },
                    { 2, "Gerente" },
                    { 3, "Director" },
                    { 4, "Comité Tecnico" }
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "Finanzas" },
                    { 2, "Tecnología" },
                    { 3, "Recursos Humanos" },
                    { 4, "Operaciones" }
                });

            migrationBuilder.InsertData(
                table: "ProjectType",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "Mejora de Procesos" },
                    { 2, "Innovación y Desarrollo" },
                    { 3, "Infraestructura" },
                    { 4, "Capacitación Interna" }
                });

            migrationBuilder.InsertData(
                table: "ApprovalRule",
                columns: new[] { "Id", "ApproverRole_ID", "Area_ID", "MaxAmount", "MinAmount", "StepOrder", "Type_ID" },
                values: new object[,]
                {
                    { 1L, 1, null, 100000m, 0m, 1, null },
                    { 2L, 2, null, 20000m, 5000m, 2, null },
                    { 3L, 2, 2, 20000m, 0m, 1, 2 },
                    { 4L, 3, null, 0m, 20000m, 3, null },
                    { 5L, 2, 1, 0m, 5000m, 2, 1 },
                    { 6L, 1, null, 10000m, 0m, 1, 2 },
                    { 7L, 4, 2, 10000m, 0m, 1, 1 },
                    { 8L, 2, 2, 30000m, 10000m, 2, null },
                    { 9L, 3, 3, 0m, 30000m, 2, null },
                    { 10L, 4, null, 50000m, 0m, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "Email", "Name", "Role_ID" },
                values: new object[,]
                {
                    { 1, "jferreyra@unaj.com", "José Ferreyra", 2 },
                    { 2, "alucero@unaj.com", "Ana Lucero", 1 },
                    { 3, "gmolinas@unaj.com", "Gonzalo Molinas", 2 },
                    { 4, "lolivera@unaj.com", "Lucas Olivera", 3 },
                    { 5, "dfagundez@unaj.com", "Danilo Fagundez", 4 },
                    { 6, "ggalli@unaj.com", "Gabriel Galli", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_ApproverRole_ID",
                table: "ApprovalRule",
                column: "ApproverRole_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_Area_ID",
                table: "ApprovalRule",
                column: "Area_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_Type_ID",
                table: "ApprovalRule",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverRole_ID",
                table: "ProjectApprovalStep",
                column: "ApproverRole_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ApproverUser_ID",
                table: "ProjectApprovalStep",
                column: "ApproverUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_ProjectProposal_ID",
                table: "ProjectApprovalStep",
                column: "ProjectProposal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApprovalStep_Status_ID",
                table: "ProjectApprovalStep",
                column: "Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Area_ID",
                table: "ProjectProposal",
                column: "Area_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_CreateBy_ID",
                table: "ProjectProposal",
                column: "CreateBy_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Status_ID",
                table: "ProjectProposal",
                column: "Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_Type_ID",
                table: "ProjectProposal",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_ID",
                table: "User",
                column: "Role_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRule");

            migrationBuilder.DropTable(
                name: "ProjectApprovalStep");

            migrationBuilder.DropTable(
                name: "ProjectProposal");

            migrationBuilder.DropTable(
                name: "ApprovalStatus");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "ProjectType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ApproverRole");
        }
    }
}
