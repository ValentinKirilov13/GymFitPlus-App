namespace GymFitPlus.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class ApplicationUserConstants
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 5;

            public const string DateFormat = "yyyy-MM-dd";

            public const int UrlMaxLenght = 200;
            public const int UrlMinLenght = 10;

            public const double GenderTypeEnumMaxValue = 3;
            public const double GenderTypeEnumMinValue = 1;

            public const int PasswordMaxLenght = 100;
            public const int PasswordMinLenght = 6;

            public const int UserPhoneMaxLength = 15;

            public const int UserPhoneMinLength = 7;

            public const int ImageMaxLenght = int.MaxValue;
        }

        public static class ExerciseConstants
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 3;

            public const int DescriptionMaxLenght = 400;
            public const int DescriptionMinLenght = 50;

            public const int VideoUrlMaxLenght = 200;
            public const int VideoUrlMinLenght = 10;
        }

        public static class FitnessProgramConstants
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 5;

            public const double ExercisePropertiesMaxValue = 200;
            public const double ExercisePropertiesMinValue = 0;

        }

        public static class WorkoutConstants
        {
            public const int NoteMaxLenght = 300;
            public const int NoteMinLenght = 5;

            public const int DurationMaxValue = int.MaxValue;
            public const int DurationMinValue = 0;
        }

        public static class StatisticConstants
        {
            public const double StatsMaxValue = 300;
            public const double StatsMinValue = 0;
        }

        public static class RecipeConstants
        {
            public const int NameMaxLenght = 100;
            public const int NameMinLenght = 5;

            public const int DescriptionMaxLenght = 1000;
            public const int DescriptionMinLenght = 5;

            public const int NoteMaxLenght = 300;
            public const int NoteMinLenght = 5;

            public const double MacrosMaxValue = 300;
            public const double MacrosMinValue = 0;

            public const int ImageMaxLenght = int.MaxValue;
        }

        public static class RoleConstants
        {
            public const string AdminRole = "Administrator";
        }

        public static class AreaConstants
        {
            public const string AdminAreaName = "Admin";
        }
    }
}
