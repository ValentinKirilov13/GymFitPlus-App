using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class AddUserStaticticsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Table with registered application users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "User last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "User last name");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                comment: "User gender",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "User gender");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "User first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "User first name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                comment: "User birth date",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "User birth date");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "UserSatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Statistics identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Statistics owner"),
                    DateOfМeasurements = table.Column<DateTime>(type: "date", nullable: false, comment: "Date Of Мeasurements"),
                    Weight = table.Column<double>(type: "float", nullable: false, comment: "Weight of user in kilograms"),
                    Height = table.Column<double>(type: "float", nullable: false, comment: "Height of user in meters"),
                    ChestCircumference = table.Column<double>(type: "float", nullable: false, comment: "Chest circumference of user in centimeters"),
                    BackCircumference = table.Column<double>(type: "float", nullable: false, comment: "Back circumference of user in centimeters"),
                    RightArmCircumference = table.Column<double>(type: "float", nullable: false, comment: "Right arm circumference of user in centimeters"),
                    LeftArmCircumference = table.Column<double>(type: "float", nullable: false, comment: "Left arm circumference of user in centimeters"),
                    WaistCircumference = table.Column<double>(type: "float", nullable: false, comment: "Waist circumference of user in centimeters"),
                    GluteusCircumference = table.Column<double>(type: "float", nullable: false, comment: "Gluteus circumference of user in centimeters"),
                    RightLegCircumference = table.Column<double>(type: "float", nullable: false, comment: "Right leg circumference of user in centimeters"),
                    LeftLegCircumference = table.Column<double>(type: "float", nullable: false, comment: "Left leg circumference of user in centimeters"),
                    RightCalfCircumference = table.Column<double>(type: "float", nullable: false, comment: "Right calf circumference of user in centimeters"),
                    LeftCalfCircumference = table.Column<double>(type: "float", nullable: false, comment: "Left calf circumference of user in centimeters")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table with statistics of users");

            migrationBuilder.CreateIndex(
                name: "IX_UserSatistics_UserId",
                table: "UserSatistics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSatistics");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Table with registered application users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "User last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "User last name");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "User gender",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "User gender");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "User first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "User first name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "User birth date",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldComment: "User birth date");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
