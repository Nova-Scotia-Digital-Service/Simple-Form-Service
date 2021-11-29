using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFormsService.API.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form_Templates",
                columns: table => new
                {
                    Template_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Json_Config = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Templates", x => x.Template_Id);
                });

            migrationBuilder.CreateTable(
                name: "Form_Submissions",
                columns: table => new
                {
                    Submission_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Template_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Json_Data = table.Column<string>(type: "jsonb", nullable: false),
                    Form_TemplateTemplate_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Submissions", x => x.Submission_Id);
                    table.ForeignKey(
                        name: "FK_Form_Submissions_Form_Templates_Form_TemplateTemplate_Id",
                        column: x => x.Form_TemplateTemplate_Id,
                        principalTable: "Form_Templates",
                        principalColumn: "Template_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Form_Submissions_Form_TemplateTemplate_Id",
                table: "Form_Submissions",
                column: "Form_TemplateTemplate_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Form_Submissions");

            migrationBuilder.DropTable(
                name: "Form_Templates");
        }
    }
}
