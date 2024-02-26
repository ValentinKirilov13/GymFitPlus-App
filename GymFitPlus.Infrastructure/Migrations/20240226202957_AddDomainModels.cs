using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Data.Migrations
{
    public partial class AddDomainModels : Migration
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
                    ImgUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Excercise image"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Excercise status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excercises", x => x.Id);
                },
                comment: "Table of excercise");

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User first name"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User last name"),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false, comment: "User birth date"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "User gender"),
                    ImgUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user profile image"),
                    FacebookUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user Facebook account"),
                    InstagramUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user Instagram account"),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user YouTube account")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                },
                comment: "Table of users personal information");

            migrationBuilder.CreateTable(
                name: "UsersExcercises",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    ExcerciseId = table.Column<int>(type: "int", nullable: false, comment: "Excercise identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersExcercises", x => new { x.ExcerciseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersExcercises_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersExcercises_Excercises_ExcerciseId",
                        column: x => x.ExcerciseId,
                        principalTable: "Excercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table of users with their excercise");

            migrationBuilder.CreateIndex(
                name: "IX_UsersExcercises_UserId",
                table: "UsersExcercises",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "UsersExcercises");

            migrationBuilder.DropTable(
                name: "Excercises");
        }
    }
}
