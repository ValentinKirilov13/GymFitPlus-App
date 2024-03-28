using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class AddEntityRecipe_AndUserRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Recipe identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Recipe name"),
                    ProteinPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Protein in 100 grams of food"),
                    CarbsPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Carbohidrates in 100 grams of food"),
                    FatPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Fat in 100 grams of food"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Recipe description about needed products and way of cooking"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Food image"),
                    Category = table.Column<int>(type: "int", nullable: false, comment: "Recipe category"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Recipe status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                },
                comment: "Table of recipes");

            migrationBuilder.CreateTable(
                name: "UsersRecipes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User identifier"),
                    RecipeId = table.Column<int>(type: "int", nullable: false, comment: "Recipe identifier"),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "User note to current recipe")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRecipes", x => new { x.UserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_UsersRecipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table of users and recipes");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRecipes_RecipeId",
                table: "UsersRecipes",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersRecipes");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
