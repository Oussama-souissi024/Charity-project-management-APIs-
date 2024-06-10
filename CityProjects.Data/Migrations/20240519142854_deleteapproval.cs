using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class deleteapproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals");

            migrationBuilder.DropColumn(
                name: "Approval",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "PresidentApproval",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecretaryApproval",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresidentApproval",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SecretaryApproval",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "Approval",
                table: "Projects",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    ApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresidentID = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SecretaryID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PresidentApproval = table.Column<bool>(type: "bit", nullable: false),
                    SecretaryApproval = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_Approvals_President",
                        column: x => x.PresidentID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Approvals_Secretary",
                        column: x => x.SecretaryID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_Projet_Approval",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_PresidentID",
                table: "Approvals",
                column: "PresidentID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_SecretaryID",
                table: "Approvals",
                column: "SecretaryID");

            migrationBuilder.CreateIndex(
                name: "uq_Project",
                table: "Approvals",
                column: "ProjectId",
                unique: true);
        }
    }
}
