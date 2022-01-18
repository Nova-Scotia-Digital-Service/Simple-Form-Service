using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

#nullable disable

namespace SimpleFormsService.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form_Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<FormTemplateData>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Form_Submission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Template_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<FormSubmissionData>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Submission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Form_Submission_Form_Template_Template_Id",
                        column: x => x.Template_Id,
                        principalTable: "Form_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Form_Submission_Template_Id",
                table: "Form_Submission",
                column: "Template_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Form_Submission");

            migrationBuilder.DropTable(
                name: "Form_Template");
        }
    }
}
