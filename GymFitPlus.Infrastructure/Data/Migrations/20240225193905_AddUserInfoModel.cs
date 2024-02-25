using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Data.Migrations
{
    public partial class AddUserInfoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User first name"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User last name"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "User birth date"),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
