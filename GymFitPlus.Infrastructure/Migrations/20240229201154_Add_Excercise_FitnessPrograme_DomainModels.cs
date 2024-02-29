using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class Add_Excercise_FitnessPrograme_DomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excercises",
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
                    table.PrimaryKey("PK_Excercises", x => x.Id);
                },
                comment: "Table of excercise");

            migrationBuilder.CreateTable(
                name: "FitnessPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Fitness program name"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Fitness program creator/owner")
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
                name: "FitnessProgramExcercise",
                columns: table => new
                {
                    FitnessProgramId = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier"),
                    ExcerciseId = table.Column<int>(type: "int", nullable: false, comment: "Excercise identifier"),
                    Sets = table.Column<int>(type: "int", nullable: false, comment: "Sets for the excercise"),
                    Reps = table.Column<int>(type: "int", nullable: false, comment: "Reps for the excercise"),
                    Weigth = table.Column<int>(type: "int", nullable: false, comment: "Weigth for the excercise")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessProgramExcercise", x => new { x.FitnessProgramId, x.ExcerciseId });
                    table.ForeignKey(
                        name: "FK_FitnessProgramExcercise_Excercises_ExcerciseId",
                        column: x => x.ExcerciseId,
                        principalTable: "Excercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FitnessProgramExcercise_FitnessPrograms_FitnessProgramId",
                        column: x => x.FitnessProgramId,
                        principalTable: "FitnessPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table of excercise in one fitness program");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessProgramExcercise_ExcerciseId",
                table: "FitnessProgramExcercise",
                column: "ExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPrograms_UserId",
                table: "FitnessPrograms",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessProgramExcercise");

            migrationBuilder.DropTable(
                name: "Excercises");

            migrationBuilder.DropTable(
                name: "FitnessPrograms");
        }
    }
}
