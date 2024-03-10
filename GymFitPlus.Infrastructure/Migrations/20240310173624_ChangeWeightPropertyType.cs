using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class ChangeWeightPropertyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "FitnessProgramsExercises",
                type: "float",
                nullable: false,
                comment: "Weight for the exercise",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Weight for the exercise");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "FitnessProgramsExercises",
                type: "int",
                nullable: false,
                comment: "Weight for the exercise",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Weight for the exercise");
        }
    }
}
