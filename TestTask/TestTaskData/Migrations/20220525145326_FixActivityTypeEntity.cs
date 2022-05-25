using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskData.Migrations
{
    public partial class FixActivityTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_ActivityTypes_ProjectId",
                table: "WorkTimes");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityTypeId",
                table: "WorkTimes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimes_ActivityTypeId",
                table: "WorkTimes",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_ActivityTypes_ActivityTypeId",
                table: "WorkTimes",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_ActivityTypes_ActivityTypeId",
                table: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTimes_ActivityTypeId",
                table: "WorkTimes");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityTypeId",
                table: "WorkTimes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_ActivityTypes_ProjectId",
                table: "WorkTimes",
                column: "ProjectId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
