using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class AddDomainClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Excercise identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Excercise name"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Excercise description"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Excercise image"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Excercise status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                },
                comment: "Table of excercise");

            migrationBuilder.CreateTable(
                name: "FitnessPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Fitness program name"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Fitness program creator/owner"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Excercise status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FitnessPrograms_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table with fitness programs");

            migrationBuilder.CreateTable(
                name: "FitnessProgramsExercises",
                columns: table => new
                {
                    FitnessProgramId = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false, comment: "Exercise identifier"),
                    Sets = table.Column<int>(type: "int", nullable: false, comment: "Sets for the exercise"),
                    Reps = table.Column<int>(type: "int", nullable: false, comment: "Reps for the exercise"),
                    Weight = table.Column<int>(type: "int", nullable: false, comment: "Weight for the exercise")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessProgramsExercises", x => new { x.FitnessProgramId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_FitnessProgramsExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FitnessProgramsExercises_FitnessPrograms_FitnessProgramId",
                        column: x => x.FitnessProgramId,
                        principalTable: "FitnessPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table of excercise in one fitness program");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPrograms_UserId",
                table: "FitnessPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessProgramsExercises_ExerciseId",
                table: "FitnessProgramsExercises",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessProgramsExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "FitnessPrograms");
        }
    }
}
