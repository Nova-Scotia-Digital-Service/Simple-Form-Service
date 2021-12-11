using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

#nullable disable

namespace SimpleFormsService.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form_Submission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Template_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Submission_Data = table.Column<FormSubmissionData>(type: "jsonb", nullable: true),
                    Create_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Create_User = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Update_Date = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false, defaultValueSql: "now()"),
                    Update_User = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Submission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Form_Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Create_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Create_User = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Update_Date = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false, defaultValueSql: "now()"),
                    Update_User = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_Template", x => x.Id);
                });
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
