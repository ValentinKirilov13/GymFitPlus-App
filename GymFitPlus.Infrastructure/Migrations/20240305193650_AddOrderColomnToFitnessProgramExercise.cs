using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class AddOrderColomnToFitnessProgramExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FitnessProgramsExercises",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Fitness program order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "FitnessProgramsExercises");
        }
    }
}
