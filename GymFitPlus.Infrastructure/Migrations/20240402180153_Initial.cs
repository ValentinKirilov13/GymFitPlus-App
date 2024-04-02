using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "User first name"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "User last name"),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true, comment: "User birth date"),
                    Gender = table.Column<int>(type: "int", nullable: true, comment: "User gender"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "User profile image"),
                    FacebookUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user Facebook account"),
                    InstagramUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user Instagram account"),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Link to user YouTube account"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "Table with registered application users");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Exercise identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Exercise name"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Exercise description"),
                    VideoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Exercise video link"),
                    MuscleGroup = table.Column<int>(type: "int", nullable: false, comment: "Exercise muscle group target"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Exercise status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                },
                comment: "Table of Exercise");

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Recipe identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Recipe name"),
                    CaloriesPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Calories in 100 grams of food"),
                    ProteinPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Protein in 100 grams of food"),
                    CarbsPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Carbohidrates in 100 grams of food"),
                    FatPerHundredGrams = table.Column<double>(type: "float", nullable: false, comment: "Fat in 100 grams of food"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Recipe description about needed products and way of cooking"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "Food image"),
                    Category = table.Column<int>(type: "int", nullable: false, comment: "Recipe category"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Recipe status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                },
                comment: "Table of recipes");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FitnessPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Fitness program name"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Fitness program creator/owner"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, comment: "Exercise status")
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
                name: "UserStatistics",
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
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table with statistics of users");

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

            migrationBuilder.CreateTable(
                name: "FitnessProgramsExercises",
                columns: table => new
                {
                    FitnessProgramId = table.Column<int>(type: "int", nullable: false, comment: "Fitness program identifier"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false, comment: "Exercise identifier"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "Fitness program order"),
                    Sets = table.Column<int>(type: "int", nullable: false, comment: "Sets for the exercise"),
                    Reps = table.Column<int>(type: "int", nullable: false, comment: "Reps for the exercise"),
                    Weight = table.Column<double>(type: "float", nullable: false, comment: "Weight for the exercise"),
                    DateOfUpdate = table.Column<DateTime>(type: "date", nullable: false, comment: "Date Of Update")
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
                comment: "Table of Exercise in one fitness program");

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Workout identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Workout owner"),
                    FitnessProgramId = table.Column<int>(type: "int", nullable: false, comment: "Fitness program that is used in workout"),
                    Date = table.Column<DateTime>(type: "date", nullable: false, comment: "Date of workout"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Duration of workout in minutes"),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "User note on workout")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workouts_FitnessPrograms_FitnessProgramId",
                        column: x => x.FitnessProgramId,
                        principalTable: "FitnessPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table with users workouts");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPrograms_UserId",
                table: "FitnessPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessProgramsExercises_ExerciseId",
                table: "FitnessProgramsExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRecipes_RecipeId",
                table: "UsersRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_FitnessProgramId",
                table: "Workouts",
                column: "FitnessProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FitnessProgramsExercises");

            migrationBuilder.DropTable(
                name: "UsersRecipes");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "FitnessPrograms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
