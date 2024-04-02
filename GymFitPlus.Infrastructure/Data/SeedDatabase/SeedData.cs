using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;

namespace GymFitPlus.Infrastructure.Data.SeedDatabase
{
    public class SeedData
    {
        public SeedData()
        {
            SeedUsers();
            SeedRoles();
            SeedAdminUserWithAdminRole();
            SeedExercise();
            SeedRecipes();
            SeedFitnessPrograms();
            SeedFitnessProgramsExercises();
            SeedUserRecipes();
            SeedUserStatistics();
            SeedWorkouts();
        }

        public ApplicationUser FirstUser { get; set; } = null!;

        public ApplicationUser AdminUser { get; set; } = null!;

        public ApplicationRole AdminRole { get; set; } = null!;

        public IdentityUserRole<Guid> MapAdminUserWithAdminRole { get; set; } = null!;

        public Exercise[] Exercises { get; set; } = null!;

        public Recipe[] Recipes { get; set; } = null!;

        public FitnessProgram[] FitnessPrograms { get; set; } = null!;

        public FitnessProgramExercise[] FitnessProgramsExercise { get; set; } = null!;

        public UserRecipe[] UserRecipes { get; set; } = null!;

        public UserStatistics[] UserStatistics { get; set; } = null!;

        public Workout[] Workouts { get; set; } = null!;


        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Email = "admin@mail.bg",
                NormalizedEmail = "ADMIN@MAIL.BG",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Admina",
                LastName = "Ada",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            AdminUser.PasswordHash =
                hasher.HashPassword(AdminUser, "Admin123@");


            FirstUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Email = "firstUser@mail.bg",
                NormalizedEmail = "FIRSTUSER@MAIL.BG",
                UserName = "FirstUser",
                NormalizedUserName = "FIRSTUSER",
                FirstName = "Valentin",
                LastName = "Kirilov",
                BirthDate = DateTime.Parse("2003-04-13"),
                Gender = Enums.GenderType.Male,
                FacebookUrl = "https://www.facebook.com/Valkata13/",
                InstagramUrl = "https://www.instagram.com/valkatasumaz/",
                YouTubeUrl = "https://www.youtube.com/channel/UCPntCGP5MhDGIvs4qtCmSeQ",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            FirstUser.PasswordHash =
                hasher.HashPassword(FirstUser, "FirstUser123@");
        }

        private void SeedRoles()
        {
            AdminRole = new ApplicationRole()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            };
        }

        private void SeedAdminUserWithAdminRole()
        {
            MapAdminUserWithAdminRole = new IdentityUserRole<Guid>
            {
                RoleId = AdminRole.Id,
                UserId = AdminUser.Id,
            };
        }

        private void SeedExercise()
        {
            Exercises = new Exercise[]
            {
                new Exercise()
                {
                    Id = 1,
                    Name = "Dumbbell Upper Chest",
                    Description = "This exercise focuses on developing the upper portion of the chest muscles using dumbbells. It typically involves lying on a bench with dumbbells in hand, then pressing the weights upward in a controlled motion, targeting the upper chest area for strength and definition.",
                    MuscleGroup = MuscleGroup.Chest,
                    VideoUrl = "EFbifMHOXPc"
                },

                new Exercise
                {
                    Id = 2,
                    Name = "Hip trust",
                    Description = "The hip thrust is a lower body strength exercise that primarily targets the glutes, but also engages the hamstrings and lower back. In this exercise, the individual lies on their back with knees bent and feet flat on the ground, while lifting their hips upward by driving through the heels.",
                    MuscleGroup = MuscleGroup.Glutes,
                    VideoUrl = "xDmFkJxPzeM"
                },

                new Exercise
                {
                    Id = 3,
                    Name = "Squat",
                    Description = "The squat is a fundamental lower body exercise that targets multiple muscle groups, including the quadriceps, hamstrings, glutes, and lower back. It involves bending the knees and hips to lower the body down into a seated position and then pushing back up to a standing position.",
                    MuscleGroup = MuscleGroup.Legs,
                    VideoUrl = "bEv6CCg2BC8"
                },

                new Exercise
                {
                    Id = 4,
                    Name = "Shoulder press",
                    Description = "Shoulder press is typically performed by sitting or standing with a barbell or dumbbells held at shoulder height. The weight is then pressed overhead until the arms are fully extended, and then lowered back down to shoulder height. This movement strengthens the shoulders, triceps, and upper chest while also improving shoulder stability and mobility.",
                    MuscleGroup = MuscleGroup.Shoulders,
                    VideoUrl = "_RlRDWO2jfg"
                },

                new Exercise
                {
                    Id = 5,
                    Name = "Romanian deadlift",
                    Description = "The Romanian deadlift is a weightlifting exercise focusing on the hamstrings, glutes, and lower back. It involves hinging at the hips with a slight knee bend while holding a barbell or dumbbells, then returning to a standing position. It improves hip hinge mechanics and strengthens the posterior chain.",
                    MuscleGroup = MuscleGroup.Glutes,
                    VideoUrl = "_oyxCn2iSjU"
                },

                new Exercise
                {
                    Id = 6,
                    Name = "Wide-grip pull-up",
                    Description = "The Wide-grip pull-up is a bodyweight exercise where an individual grips a pull-up bar with hands placed wider than shoulder-width apart. From a hanging position, they pull themselves upward until their chin passes the bar, engaging the muscles of the upper back, shoulders, and arms.",
                    MuscleGroup = MuscleGroup.Back,
                    VideoUrl = "Hdc7Mw6BIEE"
                },

                new Exercise
                {
                    Id = 7,
                    Name = "Front raise",
                    Description = "The Front Raise is a shoulder exercise where you hold a dumbbell or barbell in front of your body and lift your arm forward until it's level with your shoulder.",
                    MuscleGroup = MuscleGroup.Shoulders,
                    VideoUrl = "hRJ6tR5-if0"
                },
            };
        }

        private void SeedRecipes()
        {
            Recipes = new Recipe[]
            {
                new Recipe
                {
                    Id = 1,
                    Name = "Protein-Packed Omelette",
                    CaloriesPerHundredGrams = 320.0,
                    ProteinPerHundredGrams = 18.5,
                    CarbsPerHundredGrams = 4.2,
                    FatPerHundredGrams = 24.3,
                    Description = "Ingredients: 3 eggs, 50g diced chicken breast, 30g spinach, 20g diced tomatoes, salt, pepper, 10g grated cheese (optional).Method: Beat eggs and season with salt and pepper. Cook chicken in a pan, add spinach and   tomatoes.Pour eggs over the mixture, cook until set. Add cheese if desired.",
                    Category = RecipeType.Salty,
                },

                new Recipe
                {
                    Id = 2,
                    Name = "Grilled Chicken Salad",
                    CaloriesPerHundredGrams= 280.0,
                    ProteinPerHundredGrams = 26.0,
                    CarbsPerHundredGrams = 8.0,
                    FatPerHundredGrams = 15.0,
                    Description = "Ingredients: 150g grilled chicken breast, mixed salad greens (lettuce, spinach, arugula), 50g cherry   tomatoes, 30g cucumber, 20g feta cheese, 1 tbsp olive oil, balsamic vinegar. Method: Grill chicken, cho into     pieces.Toss with salad greens, tomatoes, cucumber, and crumbled feta. Drizzle with olive oil an balsamic     vinegar.",
                    Category = RecipeType.Salty,
                },

                new Recipe
                {
                    Id = 3,
                    Name = "Tuna Quinoa Bowl",
                    CaloriesPerHundredGrams= 320.0,
                    ProteinPerHundredGrams = 21.0,
                    CarbsPerHundredGrams = 33.0,
                    FatPerHundredGrams = 10.0,
                    Description = "Ingredients: 100g canned tuna, 100g cooked quinoa, 50g diced bell peppers, 30g diced onions, 20g chopped parsley, 1 tbsp lemon juice, salt, pepper. Method: Mix tuna, quinoa, bell peppers, onions, and parsley. Season with lemon juice, salt, and pepper. Serve chilled.",
                    Category = RecipeType.Salty,
                },

                new Recipe
                {
                    Id = 4,
                    Name = "Greek Yogurt Parfait",
                    CaloriesPerHundredGrams= 250.0,
                    ProteinPerHundredGrams = 10.0,
                    CarbsPerHundredGrams = 30.0,
                    FatPerHundredGrams = 10.0,
                    Description = "Ingredients: 150g Greek yogurt, 50g mixed berries, 30g granola, honey (optional). Method: Layer Greek yogurt, berries, and granola in a glass. Drizzle with honey if desired. Repeat layers. Serve chilled.",
                    Category = RecipeType.Salty
                },

                new Recipe
                {
                    Id = 5,
                    Name = "Protein Pancakes",
                    CaloriesPerHundredGrams= 280.0,
                    ProteinPerHundredGrams = 20.0,
                    CarbsPerHundredGrams = 25.0,
                    FatPerHundredGrams = 8.0,
                    Description = "Ingredients: 2 scoops protein powder, 2 eggs, 50ml milk, 30g oat flour, 1 tsp baking powder, 1 tsp vanilla extract.Method: Mix all ingredients until smooth. Cook pancakes on a hot griddle until golden brown on both sides.Serve with toppings of choice.",
                    Category = RecipeType.Sweety,
                },

                new Recipe
                {
                    Id = 6,
                    Name = "Salmon with Asparagus",
                    CaloriesPerHundredGrams= 300.0,
                    ProteinPerHundredGrams = 24.0,
                    CarbsPerHundredGrams = 5.0,
                    FatPerHundredGrams = 18.0,
                    Description = "Ingredients: 150g salmon fillet, 100g asparagus spears, 1 tbsp olive oil, lemon wedges, salt, pepper.    Method: Season salmon with salt and pepper.Grill or bake until cooked through. Sauté asparagus in olive oil    until tender.Serve salmon with asparagus, garnish with lemon wedges.",
                    Category = RecipeType.Salty,
                },

                new Recipe
                {
                    Id = 7,
                    Name = "Protein-Packed Smoothie",
                    CaloriesPerHundredGrams= 220.0,
                    ProteinPerHundredGrams = 10.0,
                    CarbsPerHundredGrams = 25.0,
                    FatPerHundredGrams = 5.0,
                    Description = "Ingredients: 1 scoop protein powder, 150ml almond milk, 1 banana, 50g spinach, 30g oats, 5g almond butter, ice cubes.Method: Blend all ingredients until smooth. Adjust consistency with more milk if needed.Enjoy immediately.",
                    Category = RecipeType.Sweety,
                },
            };
        }

        private void SeedFitnessPrograms()
        {
            FitnessPrograms = new FitnessProgram[]
            {
                new FitnessProgram
                {
                    Id = 1,
                    Name = "Push",
                    UserId = FirstUser.Id,
                },

                new FitnessProgram
                {
                    Id = 2,
                    Name = "Pull",
                    UserId = FirstUser.Id,
                },

                new FitnessProgram
                {
                    Id = 3,
                    Name = "Legs",
                    UserId = FirstUser.Id,
                },
            };
        }

        private void SeedFitnessProgramsExercises()
        {
            FitnessProgramsExercise = new FitnessProgramExercise[]
            {
                new FitnessProgramExercise
                {
                    FitnessProgramId = 1,
                    ExerciseId = 1,
                    Sets = 4,
                    Reps = 12,
                    Weight = 25,
                    Order = 1,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 1,
                    ExerciseId = 4,
                    Sets = 4,
                    Reps = 12,
                    Weight = 40,
                    Order = 2,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 1,
                    ExerciseId = 7,
                    Sets = 4,
                    Reps = 12,
                    Weight = 10,
                    Order = 3,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 2,
                    ExerciseId = 5,
                    Sets = 4,
                    Reps = 12,
                    Weight = 75,
                    Order = 1,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 2,
                    ExerciseId = 6,
                    Sets = 4,
                    Reps = 12,
                    Weight = 55,
                    Order = 2,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 3,
                    ExerciseId = 2,
                    Sets = 4,
                    Reps = 12,
                    Weight = 100,
                    Order = 1,
                    DateOfUpdate = DateTime.Today,
                },

                new FitnessProgramExercise
                {
                    FitnessProgramId = 3,
                    ExerciseId = 3,
                    Sets = 4,
                    Reps = 12,
                    Weight = 80,
                    Order = 2,
                    DateOfUpdate = DateTime.Today,
                },
            };
        }

        private void SeedUserRecipes()
        {
            UserRecipes = new UserRecipe[]
            {
                new UserRecipe
                {
                    UserId = FirstUser.Id,
                    RecipeId = 3,
                    Note = "Need more protein"
                },

                new UserRecipe
                {
                    UserId = FirstUser.Id,
                    RecipeId = 4,
                },

                new UserRecipe
                {
                    UserId = FirstUser.Id,
                    RecipeId = 5,
                    Note = "Add some sugar"
                },

                new UserRecipe
                {
                    UserId = FirstUser.Id,
                    RecipeId = 1,
                },
            };
        }

        private void SeedUserStatistics()
        {
            UserStatistics = new UserStatistics[]
            {
                new UserStatistics
                {
                    Id = 1,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-01"),
                    Weight = 75.2,
                    Height = 174,
                    ChestCircumference = 110,
                    BackCircumference = 150,
                    LeftArmCircumference = 43,
                    RightArmCircumference = 43.3,
                    WaistCircumference = 40,
                    GluteusCircumference = 50,
                    RightLegCircumference = 80,
                    LeftLegCircumference = 80.6,
                    RightCalfCircumference = 35.3,
                    LeftCalfCircumference = 35.4,                  
                },
                new UserStatistics
                {
                    Id = 2,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-05"),
                    Weight = 76.5,
                    Height = 175,
                    ChestCircumference = 112,
                    BackCircumference = 151,
                    LeftArmCircumference = 43.5,
                    RightArmCircumference = 43.8,
                    WaistCircumference = 41,
                    GluteusCircumference = 51,
                    RightLegCircumference = 81,
                    LeftLegCircumference = 81.2,
                    RightCalfCircumference = 35.5,
                    LeftCalfCircumference = 35.6
                },
                new UserStatistics
                {
                    Id = 3,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-10"),
                    Weight = 75.8,
                    Height = 173,
                    ChestCircumference = 111,
                    BackCircumference = 149,
                    LeftArmCircumference = 43.2,
                    RightArmCircumference = 43.5,
                    WaistCircumference = 40.5,
                    GluteusCircumference = 50.5,
                    RightLegCircumference = 80.5,
                    LeftLegCircumference = 80.8,
                    RightCalfCircumference = 35.4,
                    LeftCalfCircumference = 35.5
                },
                new UserStatistics
                {
                    Id = 4,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-15"),
                    Weight = 75.0,
                    Height = 172,
                    ChestCircumference = 109,
                    BackCircumference = 148,
                    LeftArmCircumference = 42.8,
                    RightArmCircumference = 43.1,
                    WaistCircumference = 40,
                    GluteusCircumference = 50,
                    RightLegCircumference = 80,
                    LeftLegCircumference = 80.4,
                    RightCalfCircumference = 35.2,
                    LeftCalfCircumference = 35.3
                },
                new UserStatistics
                {
                    Id = 5,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-20"),
                    Weight = 74.7,
                    Height = 171,
                    ChestCircumference = 108,
                    BackCircumference = 147,
                    LeftArmCircumference = 42.6,
                    RightArmCircumference = 42.9,
                    WaistCircumference = 39.5,
                    GluteusCircumference = 49.5,
                    RightLegCircumference = 79.5,
                    LeftLegCircumference = 80,
                    RightCalfCircumference = 35.1,
                    LeftCalfCircumference = 35.2
                },
                new UserStatistics
                {
                    Id = 6,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-25"),
                    Weight = 74.5,
                    Height = 170,
                    ChestCircumference = 107,
                    BackCircumference = 146,
                    LeftArmCircumference = 42.4,
                    RightArmCircumference = 42.7,
                    WaistCircumference = 39,
                    GluteusCircumference = 49,
                    RightLegCircumference = 79,
                    LeftLegCircumference = 79.6,
                    RightCalfCircumference = 35.0,
                    LeftCalfCircumference = 35.1
                },
                new UserStatistics
                {
                    Id = 7,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-03-30"),
                    Weight = 74.2,
                    Height = 169,
                    ChestCircumference = 106,
                    BackCircumference = 145,
                    LeftArmCircumference = 42.2,
                    RightArmCircumference = 42.5,
                    WaistCircumference = 38.5,
                    GluteusCircumference = 48.5,
                    RightLegCircumference = 78.5,
                    LeftLegCircumference = 79,
                    RightCalfCircumference = 34.9,
                    LeftCalfCircumference = 35.0
                },
                new UserStatistics
                {
                    Id = 8,
                    UserId = FirstUser.Id,
                    DateOfМeasurements = DateTime.Parse("2024-04-01"),
                    Weight = 74.0,
                    Height = 168,
                    ChestCircumference = 105,
                    BackCircumference = 144,
                    LeftArmCircumference = 42.0,
                    RightArmCircumference = 42.3,
                    WaistCircumference = 38,
                    GluteusCircumference = 48,
                    RightLegCircumference = 78,
                    LeftLegCircumference = 78.4,
                    RightCalfCircumference = 34.8,
                    LeftCalfCircumference = 34.9
                }
            };
        }

        private void SeedWorkouts()
        {
            Workouts = new Workout[]
            {
                new Workout
                {
                    Id = 1,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-01"),
                    Duration = 120,
                    Note = "Need more pump in chests",
                },
                new Workout
                {
                    Id = 2,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-03"),
                    Duration = 90,
                    Note = "Focused on cardio"
                },
                new Workout
                {
                    Id = 3,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-05"),
                    Duration = 60
                },
                new Workout
                {
                    Id = 4,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-07"),
                    Duration = 75,
                    Note = "Increased weight in squats"
                },
                new Workout
                {
                    Id = 5,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-09"),
                    Duration = 105
                },
                new Workout
                {
                    Id = 6,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-11"),
                    Duration = 45,
                    Note = "Focused on core exercises"
                },
                new Workout
                {
                    Id = 7,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-13"),
                    Duration = 80
                },
                new Workout
                {
                    Id = 8,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-15"),
                    Duration = 110,
                    Note = "Feeling energetic today"
                },
                new Workout
                {
                    Id = 9,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-17"),
                    Duration = 50
                },
                new Workout
                {
                    Id = 10,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-19"),
                    Duration = 70,
                    Note = "Need to work on flexibility"
                },
                new Workout
                {
                    Id = 11,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-21"),
                    Duration = 100
                },
                new Workout
                {
                    Id = 12,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-23"),
                    Duration = 55
                },
                new Workout
                {
                    Id = 13,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-25"),
                    Duration = 85,
                    Note = "Focused on upper body"
                },
                new Workout
                {
                    Id = 14,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-27"),
                    Duration = 95
                },
                new Workout
                {
                    Id = 15,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-29"),
                    Duration = 40
                },
                new Workout
                {
                    Id = 16,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-03-31"),
                    Duration = 65
                },
                new Workout
                {
                    Id = 17,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-02"),
                    Duration = 120
                },
                new Workout
                {
                    Id = 18,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-04"),
                    Duration = 60,
                    Note = "Need to increase intensity"
                },
                new Workout
                {
                    Id = 19,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-06"),
                    Duration = 75
                },
                new Workout
                {
                    Id = 20,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-08"),
                    Duration = 105
                },
                new Workout
                {
                    Id = 21,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-10"),
                    Duration = 45
                },
                new Workout
                {
                    Id = 22,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-12"),
                    Duration = 80
                },
                new Workout
                {
                    Id = 23,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-14"),
                    Duration = 110
                },
                new Workout
                {
                    Id = 24,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-16"),
                    Duration = 50
                },
                new Workout
                {
                    Id = 25,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-18"),
                    Duration = 70
                },
                new Workout
                {
                    Id = 26,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-06"),
                    Duration = 120
                },
                new Workout
                {
                    Id = 27,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-20"),
                    Duration = 55,
                    Note = "Feeling motivated today"
                },
                new Workout
                {
                    Id = 28,
                    FitnessProgramId = 3,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-22"),
                    Duration = 85
                },
                new Workout
                {
                    Id = 29,
                    FitnessProgramId = 1,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-24"),
                    Duration = 95
                },
                new Workout
                {
                    Id = 30,
                    FitnessProgramId = 2,
                    UserId = FirstUser.Id,
                    Date = DateTime.Parse("2024-04-26"),
                    Duration = 40
                }
            };
        }
    }
}
