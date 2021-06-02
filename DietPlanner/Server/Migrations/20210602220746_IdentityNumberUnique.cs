using Microsoft.EntityFrameworkCore.Migrations;

namespace DietPlanner.Server.Migrations
{
    public partial class IdentityNumberUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdentityNumber",
                table: "Patients",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_IdentityNumber",
                table: "Dieticians",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IdentityNumber",
                table: "Admins",
                column: "IdentityNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_IdentityNumber",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Dieticians_IdentityNumber",
                table: "Dieticians");

            migrationBuilder.DropIndex(
                name: "IX_Admins_IdentityNumber",
                table: "Admins");
        }
    }
}
