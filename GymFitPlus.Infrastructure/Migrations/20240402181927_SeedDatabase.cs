using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymFitPlus.Infrastructure.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("aea2775e-48f6-44ff-a3bd-219937657117"), "0fd4be9c-98aa-4750-8a67-0b8208bdd61d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookUrl", "FirstName", "Gender", "Image", "InstagramUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "YouTubeUrl" },
                values: new object[,]
                {
                    { new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 0, new DateTime(2003, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "a1015040-fe41-429a-9f8f-5bc39dcfe73e", "firstUser@mail.bg", false, "https://www.facebook.com/Valkata13/", "Valentin", 1, null, "https://www.instagram.com/valkatasumaz/", "Kirilov", false, null, "FIRSTUSER@MAIL.BG", "FIRSTUSER", "AQAAAAEAACcQAAAAEGvBl+0+8jLQB678Lt2YjCLaHoZ6i30xi4xdwgJX2bwKcmpgvZPXpl57l45yrIRmAA==", null, false, "de760688-941d-4354-9887-0d2baa9f0d7e", false, "FirstUser", "https://www.youtube.com/channel/UCPntCGP5MhDGIvs4qtCmSeQ" },
                    { new Guid("e3f4c702-f7bd-483e-8eb6-4bb91d0f0bb4"), 0, null, "7853abb7-5df7-43af-8cb6-e4c7d038b507", "admin@mail.bg", false, null, "Admina", null, null, null, "Ada", false, null, "ADMIN@MAIL.BG", "ADMIN", "AQAAAAEAACcQAAAAEA+/2NaBYiTwaySMNKi3qZ107gEtmq+qUkvhbKIxv5w+/AFvOF3mnVDP6NTJPqhBUA==", null, false, "6e26d4b2-0084-45e8-9d5a-9224d80cd295", false, "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "IsDelete", "MuscleGroup", "Name", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "This exercise focuses on developing the upper portion of the chest muscles using dumbbells. It typically involves lying on a bench with dumbbells in hand, then pressing the weights upward in a controlled motion, targeting the upper chest area for strength and definition.", false, 1, "Dumbbell Upper Chest", "EFbifMHOXPc" },
                    { 2, "The hip thrust is a lower body strength exercise that primarily targets the glutes, but also engages the hamstrings and lower back. In this exercise, the individual lies on their back with knees bent and feet flat on the ground, while lifting their hips upward by driving through the heels.", false, 8, "Hip trust", "xDmFkJxPzeM" },
                    { 3, "The squat is a fundamental lower body exercise that targets multiple muscle groups, including the quadriceps, hamstrings, glutes, and lower back. It involves bending the knees and hips to lower the body down into a seated position and then pushing back up to a standing position.", false, 7, "Squat", "bEv6CCg2BC8" },
                    { 4, "Shoulder press is typically performed by sitting or standing with a barbell or dumbbells held at shoulder height. The weight is then pressed overhead until the arms are fully extended, and then lowered back down to shoulder height. This movement strengthens the shoulders, triceps, and upper chest while also improving shoulder stability and mobility.", false, 3, "Shoulder press", "_RlRDWO2jfg" },
                    { 5, "The Romanian deadlift is a weightlifting exercise focusing on the hamstrings, glutes, and lower back. It involves hinging at the hips with a slight knee bend while holding a barbell or dumbbells, then returning to a standing position. It improves hip hinge mechanics and strengthens the posterior chain.", false, 8, "Romanian deadlift", "_oyxCn2iSjU" },
                    { 6, "The Wide-grip pull-up is a bodyweight exercise where an individual grips a pull-up bar with hands placed wider than shoulder-width apart. From a hanging position, they pull themselves upward until their chin passes the bar, engaging the muscles of the upper back, shoulders, and arms.", false, 2, "Wide-grip pull-up", "Hdc7Mw6BIEE" },
                    { 7, "The Front Raise is a shoulder exercise where you hold a dumbbell or barbell in front of your body and lift your arm forward until it's level with your shoulder.", false, 3, "Front raise", "hRJ6tR5-if0" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CaloriesPerHundredGrams", "CarbsPerHundredGrams", "Category", "Description", "FatPerHundredGrams", "Image", "IsDelete", "Name", "ProteinPerHundredGrams" },
                values: new object[,]
                {
                    { 1, 320.0, 4.2000000000000002, 1, "Ingredients: 3 eggs, 50g diced chicken breast, 30g spinach, 20g diced tomatoes, salt, pepper, 10g grated cheese (optional).Method: Beat eggs and season with salt and pepper. Cook chicken in a pan, add spinach and   tomatoes.Pour eggs over the mixture, cook until set. Add cheese if desired.", 24.300000000000001, null, false, "Protein-Packed Omelette", 18.5 },
                    { 2, 280.0, 8.0, 1, "Ingredients: 150g grilled chicken breast, mixed salad greens (lettuce, spinach, arugula), 50g cherry   tomatoes, 30g cucumber, 20g feta cheese, 1 tbsp olive oil, balsamic vinegar. Method: Grill chicken, cho into     pieces.Toss with salad greens, tomatoes, cucumber, and crumbled feta. Drizzle with olive oil an balsamic     vinegar.", 15.0, null, false, "Grilled Chicken Salad", 26.0 },
                    { 3, 320.0, 33.0, 1, "Ingredients: 100g canned tuna, 100g cooked quinoa, 50g diced bell peppers, 30g diced onions, 20g chopped parsley, 1 tbsp lemon juice, salt, pepper. Method: Mix tuna, quinoa, bell peppers, onions, and parsley. Season with lemon juice, salt, and pepper. Serve chilled.", 10.0, null, false, "Tuna Quinoa Bowl", 21.0 },
                    { 4, 250.0, 30.0, 1, "Ingredients: 150g Greek yogurt, 50g mixed berries, 30g granola, honey (optional). Method: Layer Greek yogurt, berries, and granola in a glass. Drizzle with honey if desired. Repeat layers. Serve chilled.", 10.0, null, false, "Greek Yogurt Parfait", 10.0 },
                    { 5, 280.0, 25.0, 2, "Ingredients: 2 scoops protein powder, 2 eggs, 50ml milk, 30g oat flour, 1 tsp baking powder, 1 tsp vanilla extract.Method: Mix all ingredients until smooth. Cook pancakes on a hot griddle until golden brown on both sides.Serve with toppings of choice.", 8.0, null, false, "Protein Pancakes", 20.0 },
                    { 6, 300.0, 5.0, 1, "Ingredients: 150g salmon fillet, 100g asparagus spears, 1 tbsp olive oil, lemon wedges, salt, pepper.    Method: Season salmon with salt and pepper.Grill or bake until cooked through. Sauté asparagus in olive oil    until tender.Serve salmon with asparagus, garnish with lemon wedges.", 18.0, null, false, "Salmon with Asparagus", 24.0 },
                    { 7, 220.0, 25.0, 2, "Ingredients: 1 scoop protein powder, 150ml almond milk, 1 banana, 50g spinach, 30g oats, 5g almond butter, ice cubes.Method: Blend all ingredients until smooth. Adjust consistency with more milk if needed.Enjoy immediately.", 5.0, null, false, "Protein-Packed Smoothie", 10.0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("aea2775e-48f6-44ff-a3bd-219937657117"), new Guid("e3f4c702-f7bd-483e-8eb6-4bb91d0f0bb4") });

            migrationBuilder.InsertData(
                table: "FitnessPrograms",
                columns: new[] { "Id", "IsDelete", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Push", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 2, false, "Pull", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 3, false, "Legs", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") }
                });

            migrationBuilder.InsertData(
                table: "UserStatistics",
                columns: new[] { "Id", "BackCircumference", "ChestCircumference", "DateOfМeasurements", "GluteusCircumference", "Height", "LeftArmCircumference", "LeftCalfCircumference", "LeftLegCircumference", "RightArmCircumference", "RightCalfCircumference", "RightLegCircumference", "UserId", "WaistCircumference", "Weight" },
                values: new object[,]
                {
                    { 1, 150.0, 110.0, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, 174.0, 43.0, 35.399999999999999, 80.599999999999994, 43.299999999999997, 35.299999999999997, 80.0, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 40.0, 75.200000000000003 },
                    { 2, 151.0, 112.0, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 51.0, 175.0, 43.5, 35.600000000000001, 81.200000000000003, 43.799999999999997, 35.5, 81.0, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 41.0, 76.5 },
                    { 3, 149.0, 111.0, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.5, 173.0, 43.200000000000003, 35.5, 80.799999999999997, 43.5, 35.399999999999999, 80.5, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 40.5, 75.799999999999997 },
                    { 4, 148.0, 109.0, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, 172.0, 42.799999999999997, 35.299999999999997, 80.400000000000006, 43.100000000000001, 35.200000000000003, 80.0, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 40.0, 75.0 },
                    { 5, 147.0, 108.0, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.5, 171.0, 42.600000000000001, 35.200000000000003, 80.0, 42.899999999999999, 35.100000000000001, 79.5, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 39.5, 74.700000000000003 },
                    { 6, 146.0, 107.0, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.0, 170.0, 42.399999999999999, 35.100000000000001, 79.599999999999994, 42.700000000000003, 35.0, 79.0, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 39.0, 74.5 },
                    { 7, 145.0, 106.0, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 48.5, 169.0, 42.200000000000003, 35.0, 79.0, 42.5, 34.899999999999999, 78.5, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 38.5, 74.200000000000003 },
                    { 8, 144.0, 105.0, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 48.0, 168.0, 42.0, 34.899999999999999, 78.400000000000006, 42.299999999999997, 34.799999999999997, 78.0, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), 38.0, 74.0 }
                });

            migrationBuilder.InsertData(
                table: "UsersRecipes",
                columns: new[] { "RecipeId", "UserId", "Note" },
                values: new object[,]
                {
                    { 1, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), null },
                    { 3, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), "Need more protein" },
                    { 4, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), null },
                    { 5, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"), "Add some sugar" }
                });

            migrationBuilder.InsertData(
                table: "FitnessProgramsExercises",
                columns: new[] { "ExerciseId", "FitnessProgramId", "DateOfUpdate", "Order", "Reps", "Sets", "Weight" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 1, 12, 4, 25.0 },
                    { 4, 1, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 2, 12, 4, 40.0 },
                    { 7, 1, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 3, 12, 4, 10.0 },
                    { 5, 2, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 1, 12, 4, 75.0 },
                    { 6, 2, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 2, 12, 4, 55.0 },
                    { 2, 3, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 1, 12, 4, 100.0 },
                    { 3, 3, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 2, 12, 4, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Date", "Duration", "FitnessProgramId", "Note", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, 1, "Need more pump in chests", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 2, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, 1, "Focused on cardio", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 3, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 4, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 75, 3, "Increased weight in squats", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 5, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 105, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 6, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, 2, "Focused on core exercises", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 7, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 80, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 8, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 110, 1, "Feeling energetic today", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 9, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 10, new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, 3, "Need to work on flexibility", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 11, new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 12, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 55, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 13, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 85, 3, "Focused on upper body", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 14, new DateTime(2024, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 95, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 15, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 16, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 65, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 17, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 18, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, 2, "Need to increase intensity", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 19, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 75, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 20, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 105, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 21, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 22, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 80, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 23, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 110, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 24, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 25, new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 26, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 27, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 55, 2, "Feeling motivated today", new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 28, new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 85, 3, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 29, new DateTime(2024, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 95, 1, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") },
                    { 30, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, 2, null, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("aea2775e-48f6-44ff-a3bd-219937657117"), new Guid("e3f4c702-f7bd-483e-8eb6-4bb91d0f0bb4") });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "FitnessProgramsExercises",
                keyColumns: new[] { "ExerciseId", "FitnessProgramId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserStatistics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UsersRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 1, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") });

            migrationBuilder.DeleteData(
                table: "UsersRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 3, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") });

            migrationBuilder.DeleteData(
                table: "UsersRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 4, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") });

            migrationBuilder.DeleteData(
                table: "UsersRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { 5, new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6") });

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aea2775e-48f6-44ff-a3bd-219937657117"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e3f4c702-f7bd-483e-8eb6-4bb91d0f0bb4"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FitnessPrograms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FitnessPrograms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FitnessPrograms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4a2d18dc-e328-4940-807f-85b8ac83afc6"));
        }
    }
}
